/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.versions.sql_command;

import com.luca_dillenburg.educative_sql.versions.sql_command.sql_command_stage.SqlCommandStage;
import java.util.LinkedList;
import java.util.List;
import java.util.Objects;

/**
 *
 * @author luca_dillenburg
 */
public class SqlCommand {
    
    protected String name;
    protected boolean justUsedOnHelp;
    protected List<SqlCommandStage> sqlCommandStages = new LinkedList<SqlCommandStage>();

    public SqlCommand(String name, boolean justUsedOnHelp) {
        this.name = name;
        this.justUsedOnHelp = justUsedOnHelp;
    }
    
    public void addSqlCommandStage(SqlCommandStage commandStage)
    {
        this.sqlCommandStages.add(commandStage);
    }

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public boolean isJustUsedOnHelp() {
        return justUsedOnHelp;
    }
    public void setJustUsedOnHelp(boolean justUsedOnHelp) {
        this.justUsedOnHelp = justUsedOnHelp;
    }

    @Override
    public String toString() {
        return "SqlCommand{" + "name=" + name + ", justUsedOnHelp=" + justUsedOnHelp + '}';
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 41 * hash + Objects.hashCode(this.name);
        hash = 41 * hash + (this.justUsedOnHelp ? 1 : 0);
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
        final SqlCommand other = (SqlCommand) obj;
        if (this.justUsedOnHelp != other.justUsedOnHelp) {
            return false;
        }
        if (!Objects.equals(this.name, other.name)) {
            return false;
        }
        return true;
    }
      
}
