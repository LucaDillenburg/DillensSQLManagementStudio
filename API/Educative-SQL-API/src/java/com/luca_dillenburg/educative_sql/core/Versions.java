/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.core;

import com.luca_dillenburg.educative_sql.config.Config;
import com.luca_dillenburg.educative_sql.file_operations.FileOperations;
import com.luca_dillenburg.educative_sql.storage.versions.desktop_app.DesktopApp;
import com.luca_dillenburg.educative_sql.storage.versions.updates.UpdatesOnServer;
import com.luca_dillenburg.educative_sql.storage.sql_command.SqlCommand;
import com.luca_dillenburg.educative_sql.storage.sql_command.stage.SqlCommandStage;
import com.luca_dillenburg.educative_sql.xml_operations.XmlOperations;
import java.io.File;
import java.io.IOException;
import java.util.Calendar;
import java.util.LinkedList;
import java.util.List;
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
   
    /**
     * Checks the version of the desktop app and returns a DesktopAppUpdates with only the necessary informations
     * ...
     * @throws SAXException if the XML file is not right
     * @throws IOException if the XML file doesn't exist or is not reachable
     */
    public static UpdatesOnServer checkVersions(DesktopApp desktopApp) throws SAXException, IOException, IllegalStateException {
        final UpdatesOnServer necessaryUpdates = Versions.getCurrVersionsWithoutText(desktopApp);
        
        // set texts (only if necessary)
        if (!necessaryUpdates.isSqlCommandsUpdated())
            necessaryUpdates.setSqlCommands(Versions.getLocalDatabaseInserts());
        
        return necessaryUpdates;
    }
    
    
    // VERIFY VERSION
    protected static UpdatesOnServer getCurrVersionsWithoutText(DesktopApp desktopApp) throws IllegalStateException {
        
        // versions
        final Calendar sqlCommandsVersion = FileOperations.modifiedDate(Config.PATH_SQL_COMMAND_XML);
        final int desktopAppVersion = Config.currentDesktopAppVersion;
        
        // boolean values
        final boolean sqlCommandsUpdated = desktopApp.getSqlCommandsVersion().compareTo(sqlCommandsVersion) >= 0;
        final boolean desktopAppIsUpdated = desktopAppVersion == desktopApp.getDesktopAppVersion();
        
        return new UpdatesOnServer(sqlCommandsVersion, desktopAppVersion, sqlCommandsUpdated, desktopAppIsUpdated);
        
    }
    
    
    /**
     * Access the XML file and return a list with the SqlCommands of it
     * ...
     * @throws SAXException if the XML file is not right
     * @throws IOException if the XML file doesn't exist or is not reachable
     */
    protected static List<SqlCommand> getLocalDatabaseInserts() throws SAXException, IOException {
        
        try {
            // put the xml file in a object
            final DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            factory.setValidating(true);
            factory.setIgnoringElementContentWhitespace(true);

            final File sqlExplanationFile = new File(Config.PATH_SQL_COMMAND_XML);

            final Document document = factory.newDocumentBuilder().parse(sqlExplanationFile);
            // document.getDocumentElement().normalize(); //doesnt do that because the server has to know the different lines

            // iterate the object from the xml and create SqlCommands and SqlCommandStage
            // and concatenates its inserts (given by those objects) in the variables above
            NodeList commandList = document.getElementsByTagName("command");
            List<SqlCommand> sqlCommands = new LinkedList<>();
            for (int iCommand = 0; iCommand < commandList.getLength(); iCommand++) { //EVERY COMMAND
                Node commandNode = commandList.item(iCommand);
                if (commandNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element commandElement = (Element) commandNode;

                    // get sql command attributes
                    final String commandName = commandElement.getAttribute(Config.COMMAND_NAME_STR);

                    final boolean isQuery = "true".equals(XmlOperations.getOptionalAttribute(commandElement,
                            Config.COMMAND_IS_QUERY_STR, Boolean.toString(Config.DEFAULT_COMMAND_IS_QUERY)));

                    final boolean hasEscope = "true".equals(XmlOperations.getOptionalAttribute(commandElement,
                            Config.COMMAND_HAS_ESCOPE_STR, Boolean.toString(Config.DEFAULT_COMMAND_HAS_ESCOPE)));

                    final boolean justUsedOnHelp = "true".equals(XmlOperations.getOptionalAttribute(commandElement,
                            Config.COMMAND_JUST_USED_ON_HELP_STR, Boolean.toString(Config.DEFAULT_COMMAND_JUST_USED_ON_HELP)));

                    // create SqlCommand and add insert to the String
                    final SqlCommand sqlCommand = new SqlCommand(commandName, isQuery, hasEscope, justUsedOnHelp);
                    sqlCommands.add(sqlCommand);

                    NodeList stageList = commandElement.getElementsByTagName("stage");
                    for (int iStage = 0; iStage < stageList.getLength(); iStage++) { // EVERY STAGE
                        Node stageNode = stageList.item(iStage);

                        if (stageNode.getNodeType() == Node.ELEMENT_NODE) {
                            Element stageElement = (Element) stageNode;

                            // get sql command stage attributes
                            final String title = commandElement.getAttribute(Config.STAGE_TITLE_STR);
                            final String explanation = Versions.formatXmlElement(
                                    XmlOperations.getAllLinesElement(stageElement, "explanation", false));
                            final String example = Versions.formatXmlElement(
                                    XmlOperations.getAllLinesElement(stageElement, "example", false));

                            // create SqlCommandStage and add insert to the String
                            final SqlCommandStage sqlCommandStage = new SqlCommandStage(title, explanation, example);
                            sqlCommand.addSqlCommandStage(sqlCommandStage);
                        }
                    }
                }
            }
            
            return sqlCommands;
            
        } catch (ParserConfigurationException e) {
            return null; //it will never get here
        }
    }
    
    protected static String formatXmlElement(String elementStr)
    {
        return elementStr.substring(3, elementStr.length() - 3);
        // takes off the \nTAB of the begining and the \nTAB of the end
    }
}
