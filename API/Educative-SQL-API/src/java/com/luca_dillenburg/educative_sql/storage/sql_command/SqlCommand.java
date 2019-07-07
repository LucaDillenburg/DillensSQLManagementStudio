/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.storage.sql_command;

import com.luca_dillenburg.educative_sql.storage.sql_command.stage.SqlCommandStage;
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
public class SqlCommand {

    protected String name;
    protected boolean isQuery;
    protected boolean hasEscope;
    protected boolean justUsedOnHelp;
    protected List<SqlCommandStage> sqlCommandStages = new LinkedList<SqlCommandStage>();
    
    // constructors

    public SqlCommand(String name, boolean isQuery, boolean hasEscope, boolean justUsedOnHelp) {
        this.name = name;
        this.isQuery = isQuery;
        this.hasEscope = hasEscope;
        this.justUsedOnHelp = justUsedOnHelp;
    }

    
    // methods
    public void addSqlCommandStage(SqlCommandStage commandStage) {
        this.sqlCommandStages.add(commandStage);
    }
      
}
