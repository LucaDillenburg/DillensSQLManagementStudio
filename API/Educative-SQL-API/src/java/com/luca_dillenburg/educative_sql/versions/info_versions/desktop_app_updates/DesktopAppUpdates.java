/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.versions.info_versions.desktop_app_updates;

import com.luca_dillenburg.educative_sql.versions.info_versions.InfoVersions;
import com.luca_dillenburg.educative_sql.versions.sql_command.SqlCommand;
import java.util.Date;
import java.util.List;
import java.util.Objects;

/**
 *
 * @author luca_dillenburg
 */
public class DesktopAppUpdates extends InfoVersions {
    
    // Commands and commands explanation
    protected boolean sqlExplanationUpdated;
    protected boolean sqlCommandsUpdated;
    
    protected List<SqlCommand> sqlCommands = null;
    
    // Desktop app version
    protected boolean desktopAppVersionUpdated;
    
    // ######################### METHODS ############################

    public DesktopAppUpdates(Date sqlExplanationVersion, Date sqlCommandsVersion, int desktopAppVersion) {
        super(sqlExplanationVersion, sqlCommandsVersion, desktopAppVersion);
    }
    
    public List<SqlCommand> getSqlCommands() {
        return this.sqlCommands;
    }
    public void setSqlCommands(List<SqlCommand> sqlCommands) {
        this.sqlCommands = sqlCommands;
    }
    public boolean isSqlExplanationUpdated() {
        return sqlExplanationUpdated;
    }
    public void setSqlExplanationUpdated(boolean sqlExplanationUpdated) {
        this.sqlExplanationUpdated = sqlExplanationUpdated;
    }
    public boolean isSqlCommandsUpdated() {
        return sqlCommandsUpdated;
    }
    public void setSqlCommandsUpdated(boolean sqlCommandsUpdated) {
        this.sqlCommandsUpdated = sqlCommandsUpdated;
    }
    public boolean isDesktopAppVersionUpdated() {
        return desktopAppVersionUpdated;
    }
    public void setDesktopAppVersionUpdated(boolean desktopAppVersionUpdated) {
        this.desktopAppVersionUpdated = desktopAppVersionUpdated;
    }

    @Override
    public String toString() {
        return "DesktopAppUpdates{" + "sqlExplanationUpdated=" + sqlExplanationUpdated + ", sqlCommandsUpdated=" + sqlCommandsUpdated + ", sqlCommands=" + sqlCommands + ", desktopAppVersionUpdated=" + desktopAppVersionUpdated + '}';
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 31 * hash + (this.sqlExplanationUpdated ? 1 : 0);
        hash = 31 * hash + (this.sqlCommandsUpdated ? 1 : 0);
        hash = 31 * hash + Objects.hashCode(this.sqlCommands);
        hash = 31 * hash + (this.desktopAppVersionUpdated ? 1 : 0);
        return hash;
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final DesktopAppUpdates other = (DesktopAppUpdates) obj;
        if (this.sqlExplanationUpdated != other.sqlExplanationUpdated) {
            return false;
        }
        if (this.sqlCommandsUpdated != other.sqlCommandsUpdated) {
            return false;
        }
        if (this.desktopAppVersionUpdated != other.desktopAppVersionUpdated) {
            return false;
        }
        if (!Objects.equals(this.sqlCommands, other.sqlCommands)) {
            return false;
        }
        return true;
    }

    
}
