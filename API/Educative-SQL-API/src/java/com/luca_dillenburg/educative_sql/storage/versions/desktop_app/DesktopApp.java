/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.storage.versions.desktop_app;

import com.luca_dillenburg.educative_sql.storage.versions.InfoVersions;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Objects;
import lombok.AccessLevel;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 *
 * @author luca_dillenburg
 */
@Data @NoArgsConstructor(access = AccessLevel.PROTECTED/*to convert Json string in this object*/)
public class DesktopApp extends InfoVersions {
    
    //to collect some data about the users:
    protected String macAdress;
    protected int timesUsed;

}
