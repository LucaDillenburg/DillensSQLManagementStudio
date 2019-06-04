/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.versions.sql_command.sql_command_stage;

import com.luca_dillenburg.educative_sql.versions.sql_command.SqlCommand;
import java.util.Objects;

/**
 *
 * @author luca_dillenburg
 */
public class SqlCommandStage {
    
    protected String title;
    protected String explanation;
    protected String example;

    public SqlCommandStage(String title, String explanation, String example) {
        this.title = title;
        this.explanation = explanation;
        this.example = example;
    }

    public String getTitle() {
        return title;
    }
    public void setTitle(String title) {
        this.title = title;
    }
    public String getExplanation() {
        return explanation;
    }
    public void setExplanation(String explanation) {
        this.explanation = explanation;
    }
    public String getExample() {
        return example;
    }
    public void setExample(String example) {
        this.example = example;
    }

    @Override
    public String toString() {
        return "SqlCommandStage{" + "title=" + title + ", explanation=" + explanation + ", example=" + example + '}';
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 97 * hash + Objects.hashCode(this.title);
        hash = 97 * hash + Objects.hashCode(this.explanation);
        hash = 97 * hash + Objects.hashCode(this.example);
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
        final SqlCommandStage other = (SqlCommandStage) obj;
        if (!Objects.equals(this.title, other.title)) {
            return false;
        }
        if (!Objects.equals(this.explanation, other.explanation)) {
            return false;
        }
        if (!Objects.equals(this.example, other.example)) {
            return false;
        }
        return true;
    }
    
}
