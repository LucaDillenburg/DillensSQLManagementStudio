/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.versions.info_versions;

import java.util.Date;
import java.util.Objects;

/**
 *
 * @author luca_dillenburg
 */
public class InfoVersions {

    // Commands
    protected Date sqlExplanationVersion;

    // Command explanations
    protected Date sqlCommandsVersion;

    // Desktop app version
    protected int desktopAppVersion;
    
    // ###################### METHODS #######################

    public InfoVersions()
    { }
    
    public InfoVersions(Date sqlExplanationVersion, Date sqlCommandsVersion, int desktopAppVersion) {
        this.sqlExplanationVersion = sqlExplanationVersion;
        this.sqlCommandsVersion = sqlCommandsVersion;
        this.desktopAppVersion = desktopAppVersion;
    }
    public int getDesktopAppVersion() {
        return desktopAppVersion;
    }
    public void setDesktopAppVersion(int desktopAppVersion) {
        this.desktopAppVersion = desktopAppVersion;
    }
    public Date getSqlExplanationVersion() {
        return sqlExplanationVersion;
    }
    public void setSqlExplanationVersion(Date sqlExplanationVersion) {
        this.sqlExplanationVersion = sqlExplanationVersion;
    }
    public Date getSqlCommandsVersion() {
        return sqlCommandsVersion;
    }
    public void setSqlCommandsVersion(Date sqlCommandsVersion) {
        this.sqlCommandsVersion = sqlCommandsVersion;
    }

    @Override
    public String toString() {
        return "InfoVersions{" + "sqlExplanationVersion=" + sqlExplanationVersion + ", sqlCommandsVersion=" + sqlCommandsVersion + ", desktopAppVersion=" + desktopAppVersion + '}';
    }

    @Override
    public int hashCode() {
        int hash = 3;
        hash = 37 * hash + Objects.hashCode(this.sqlExplanationVersion);
        hash = 37 * hash + Objects.hashCode(this.sqlCommandsVersion);
        hash = 37 * hash + this.desktopAppVersion;
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
        final InfoVersions other = (InfoVersions) obj;
        if (this.desktopAppVersion != other.desktopAppVersion) {
            return false;
        }
        if (!Objects.equals(this.sqlExplanationVersion, other.sqlExplanationVersion)) {
            return false;
        }
        if (!Objects.equals(this.sqlCommandsVersion, other.sqlCommandsVersion)) {
            return false;
        }
        return true;
    }

    
}
