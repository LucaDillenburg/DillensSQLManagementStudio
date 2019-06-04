/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.file_operations;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Date;

/**
 *
 * @author luca_dillenburg
 */
public class FileOperations {
    
    public static String END_LINE = "\n\r";
    
    public static Date modifiedDate(String path) {
        final long lastModified = new File(path).lastModified();
        return new Date(lastModified);
    }
    
    public static String strFromFile(String path) throws IOException {
        return new String(Files.readAllBytes(Paths.get(path)));
    }
    
}
