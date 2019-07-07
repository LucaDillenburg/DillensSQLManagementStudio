/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.config;

import java.text.SimpleDateFormat;

/**
 *
 * @author luca_dillenburg
 */
public class Config {
    
    // CONSTANTS
    
    //STRUCTURAL
    public static final int currentDesktopAppVersion = 1;
    
    //PATH
    public static final String PATH_BEFORE_PROJECT_FOLDER = "/home/luca_dillenburg/Documents/Projetos/Educative-SQL-Management-Studio/API/"; //TODO: do in a way I dont have to change this variable every time the project changes the folder
    public static final String PATH_SQL_COMMAND_XML = Config.PATH_BEFORE_PROJECT_FOLDER + "Sql-Commands/SqlCommands.xml";
    
    //XML
    //defaults values
    public static boolean DEFAULT_COMMAND_IS_QUERY          = false;
    public static boolean DEFAULT_COMMAND_HAS_ESCOPE        = false;
    public static boolean DEFAULT_COMMAND_JUST_USED_ON_HELP = false;
    //strings
    public static String COMMAND_NAME_STR               = "name";
    public static String COMMAND_IS_QUERY_STR           = "is_query";
    public static String COMMAND_HAS_ESCOPE_STR         = "has_escope";
    public static String COMMAND_JUST_USED_ON_HELP_STR  = "just_used_on_help";
    public static String STAGE_TITLE_STR                = "title";
    
    //ENCODING
    public static String ENCODING_TYPE = "UTF-8";
    
    //JSON
    public static SimpleDateFormat JSON_DATE_FORMAT = new SimpleDateFormat("MM-dd-yyyy mm:HH:ss");

}
