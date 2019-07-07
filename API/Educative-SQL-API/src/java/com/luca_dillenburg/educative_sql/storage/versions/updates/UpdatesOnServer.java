/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.storage.versions.updates;

import com.luca_dillenburg.educative_sql.storage.versions.InfoVersions;
import com.luca_dillenburg.educative_sql.storage.sql_command.SqlCommand;
import java.util.Calendar;
import java.util.Date;
import java.util.LinkedList;
import java.util.List;
import java.util.Objects;
import lombok.AllArgsConstructor;
import lombok.Data;

/**
 *
 * @author luca_dillenburg
 */
@Data
public class UpdatesOnServer extends InfoVersions {
    
    // Commands
    protected boolean sqlCommandsUpdated;
    //protected boolean sqlExplanationUpdated;
    //its not possible to know if only the explanation of the commands were updated or the commands themselves too
    
    protected List<SqlCommand> sqlCommands = null;
    
    // Desktop app
    protected boolean desktopAppVersionUpdated;

    
    // constructor

    public UpdatesOnServer(Calendar sqlCommandsVersion, int desktopAppVersion, boolean sqlCommandsUpdated, boolean desktopAppVersionUpdated) {
        super(sqlCommandsVersion, desktopAppVersion);
        this.sqlCommandsUpdated = sqlCommandsUpdated;
        this.desktopAppVersionUpdated = desktopAppVersionUpdated;
    }
    
}
