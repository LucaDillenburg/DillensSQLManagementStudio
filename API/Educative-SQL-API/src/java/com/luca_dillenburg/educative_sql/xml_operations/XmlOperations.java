/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.xml_operations;

import com.luca_dillenburg.educative_sql.storage.sql_command.SqlCommand;
import java.util.LinkedList;
import java.util.List;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

/**
 *
 * @author luca_dillenburg
 */
public class XmlOperations {

    public static String getOptionalAttribute(Element element, String attributeName, String defaultValue) {
        if (!element.hasAttribute(attributeName)) //default
            return defaultValue;
        else
            return element.getAttribute(attributeName);
    }
    
    public static String getAllLinesElement(Element element, String attributeName) {
        return XmlOperations.getAllLinesElement(element, attributeName, true);
    }
    
    public static String getAllLinesElement(Element element, String attributeName, boolean considerFirstAndLastLines) {
        String lines = "";
        
        NodeList nodeList = element.getElementsByTagName(attributeName);
        for (int i = 0; i < nodeList.getLength(); i++) {
            lines += nodeList.item(i).getTextContent();
        }
        
        return lines;
    }
    
}
