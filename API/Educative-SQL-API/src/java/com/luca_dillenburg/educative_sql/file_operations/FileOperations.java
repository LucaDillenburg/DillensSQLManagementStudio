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
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

/**
 *
 * @author luca_dillenburg
 */
public class FileOperations {
    
    public static Calendar modifiedDate(String path) throws IllegalStateException {
        final File file = new File(path);
        if (!file.exists())
            throw new IllegalStateException(file.getAbsolutePath() + " file doesnt exist!");
        
        final long lastModifiedInMilliseconds = new File(path).lastModified();
        final Calendar lastModifiedDate = new GregorianCalendar();
        lastModifiedDate.setTimeInMillis(lastModifiedInMilliseconds);
        return lastModifiedDate;
    }
    
    public static String strFromFile(String path) throws IOException {
        return new String(Files.readAllBytes(Paths.get(path)));
    }
    
}
