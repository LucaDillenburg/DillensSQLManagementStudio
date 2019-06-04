/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.versions;

import com.luca_dillenburg.educative_sql.config.Config;
import com.luca_dillenburg.educative_sql.file_operations.FileOperations;
import com.luca_dillenburg.educative_sql.versions.info_versions.InfoVersions;
import com.luca_dillenburg.educative_sql.versions.info_versions.desktop_app_info.DesktopAppInfo;
import com.luca_dillenburg.educative_sql.versions.info_versions.desktop_app_updates.DesktopAppUpdates;
import com.luca_dillenburg.educative_sql.versions.sql_command.SqlCommand;
import com.luca_dillenburg.educative_sql.versions.sql_command.sql_command_stage.SqlCommandStage;
import com.luca_dillenburg.educative_sql.xml.XmlOperations;
import java.io.File;
import java.io.IOException;
import java.util.Date;
import java.util.LinkedList;
import java.util.List;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

/**
 *
 * @author luca_dillenburg
 */
public class Versions {
   
    public static DesktopAppUpdates checkVersions(DesktopAppInfo desktopApp) {
        final DesktopAppUpdates necessaryUpdates = Versions.getCurrVersionsWithoutText();
        
        // boolean values
        final boolean sqlCommandsAreUpdated = Versions.isUpdated(necessaryUpdates.getSqlCommandsVersion(), desktopApp.getSqlCommandsVersion());
        final boolean sqlExplanationIsUpdated = Versions.isUpdated(necessaryUpdates.getSqlExplanationVersion(), desktopApp.getSqlExplanationVersion());
        final boolean desktopAppIsUpdated = necessaryUpdates.getDesktopAppVersion() == desktopApp.getDesktopAppVersion();
        
        // set boolean values
        necessaryUpdates.setSqlCommandsUpdated(sqlCommandsAreUpdated);
        necessaryUpdates.setSqlExplanationUpdated(sqlExplanationIsUpdated);
        necessaryUpdates.setDesktopAppVersionUpdated(desktopAppIsUpdated);
        
        // set texts (only if necessary)
        if (!necessaryUpdates.isSqlCommandsUpdated() || !necessaryUpdates.isSqlExplanationUpdated())
            necessaryUpdates.setSqlCommands(Versions.getLocalDatabaseInserts());
        
        return necessaryUpdates;
    }
    
    
    // VERIFY VERSION
    protected static DesktopAppUpdates getCurrVersionsWithoutText() {
        final Date sqlExplanationVersion = FileOperations.modifiedDate(Config.pathSqlExplanation);
        final Date sqlCommandsVersion = FileOperations.modifiedDate(Config.pathSqlCommands);
        final int desktopAppVersion = Config.currentDesktopAppVersion;
        
        return new DesktopAppUpdates(sqlExplanationVersion, sqlCommandsVersion, desktopAppVersion);
    }
    
    protected static boolean isUpdated(Date currentVersion, Date desktopAppVersion) {
        return currentVersion.equals(desktopAppVersion);
    }
    
    
    // INSERTS: 0=commands, 1=explanation
    protected static List<SqlCommand> getLocalDatabaseInserts() {
        
        try {
            // put the xml file in a object
            final DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            factory.setValidating(true);
            factory.setIgnoringElementContentWhitespace(true);
            
            final File sqlExplanationFile = new File(Config.pathSqlExplanation);
            
            final Document document = factory.newDocumentBuilder().parse(sqlExplanationFile);
            // document.getDocumentElement().normalize(); //doesnt do that because the server has to know the different lines
            
            // iterate the object from the xml and create SqlCommands and SqlCommandStage
            // and concatenates its inserts (given by those objects) in the variables above
            NodeList commandList = document.getElementsByTagName("command");
            List<SqlCommand> sqlCommands = new LinkedList<SqlCommand>();
            for (int iCommand = 0; iCommand < commandList.getLength(); iCommand++) { //EVERY COMMAND
                Node commandNode = commandList.item(iCommand);
                if (commandNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element commandElement = (Element) commandNode;

                    // get sql command attributes
                    final String commandName = commandElement.getAttribute("name");
                    final boolean justUsedOnHelp = XmlOperations.getOptionalAttribute(commandElement, "just_used_on_help", "true") == "true";

                    // create SqlCommand and add insert to the String
                    final SqlCommand sqlCommand = new SqlCommand(commandName, justUsedOnHelp);
                    sqlCommands.add(sqlCommand);

                    NodeList stageList = commandElement.getElementsByTagName("stage");
                    for (int iStage = 0; iStage < stageList.getLength(); iStage++) { // EVERY STAGE
                        Node stageNode = stageList.item(iStage);

                        if (stageNode.getNodeType() == Node.ELEMENT_NODE) {
                            Element stageElement = (Element) stageNode;

                            // get sql command stage attributes
                            final String title = XmlOperations.getAllLinesElement(stageElement, "title");
                            final String explanation = XmlOperations.getAllLinesElement(stageElement, "explanation", false);
                            final String example = XmlOperations.getAllLinesElement(stageElement, "example");

                            // create SqlCommandStage and add insert to the String
                            final SqlCommandStage sqlCommandStage = new SqlCommandStage(title, explanation, example);
                            sqlCommand.addSqlCommandStage(sqlCommandStage);
                        }
                    }
                }
            }
            
            return sqlCommands;
            
        }catch(SAXException e) {
            System.err.println("XML SQL Explanation file is not correct!");
            return null;
        }catch(IOException | ParserConfigurationException e) {
            return null; //it will never come here
        }
    }
}
