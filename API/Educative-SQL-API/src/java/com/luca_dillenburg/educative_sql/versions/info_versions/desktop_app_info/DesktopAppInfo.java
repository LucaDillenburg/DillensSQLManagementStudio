/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.versions.info_versions.desktop_app_info;

import com.luca_dillenburg.educative_sql.versions.info_versions.InfoVersions;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Objects;

/**
 *
 * @author luca_dillenburg
 */
public class DesktopAppInfo extends InfoVersions {
    
    protected String macAdress;

    // ######################## METHODS ########################
    
    public DesktopAppInfo()
    { }
    public String getMacAdress() {
        return macAdress;
    }
    public void setMacAdress(String macAdress) {
        this.macAdress = macAdress;
    }
    
    protected static SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
    public void setSqlExplanationVersion(String sqlExplanationVersion) throws ParseException {
        
        System.out.println("Luca Date: " + sqlExplanationVersion);
        
        this.sqlExplanationVersion = dateFormat.parse(sqlExplanationVersion);
    }
    public void setSqlCommandsVersion(String sqlCommandsVersion) throws ParseException {
        this.sqlCommandsVersion = dateFormat.parse(sqlCommandsVersion);
    }

    @Override
    public String toString() {
        return "DesktopAppInfo{" + "macAdress=" + macAdress + ", sqlExplanationVersion=" + sqlExplanationVersion + ", sqlCommandsVersion=" + sqlCommandsVersion + ", desktopAppVersion=" + desktopAppVersion + "}";
    }
    @Override
    public int hashCode() {
        int hash = super.hashCode();
        hash = 23 * hash + Objects.hashCode(this.macAdress);
        return hash;
    }
    @Override
    public boolean equals(Object obj) {
        if (!super.equals(obj)) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final DesktopAppInfo other = (DesktopAppInfo) obj;
        if (!Objects.equals(this.macAdress, other.macAdress)) {
            return false;
        }
        return true;
    }
}
