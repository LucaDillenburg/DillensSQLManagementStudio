/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.storage.sql_command.stage;

import com.luca_dillenburg.educative_sql.storage.sql_command.SqlCommand;
import java.util.Objects;
import lombok.AllArgsConstructor;
import lombok.Data;

/**
 *
 * @author luca_dillenburg
 */
@Data @AllArgsConstructor
public class SqlCommandStage {
    
    protected String title;
    protected String explanation;
    protected String example;
    
}
