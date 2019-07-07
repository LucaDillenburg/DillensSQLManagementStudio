/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.storage.versions;

import com.luca_dillenburg.educative_sql.config.jackson.CalendarDeserializer;
import com.luca_dillenburg.educative_sql.config.jackson.CalendarSerializer;
import java.util.Calendar;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.codehaus.jackson.map.annotate.JsonDeserialize;
import org.codehaus.jackson.map.annotate.JsonSerialize;

/**
 *
 * @author luca_dillenburg
 */
@Data @NoArgsConstructor(access = AccessLevel.PROTECTED) @AllArgsConstructor
public class InfoVersions {

    // Command
    @JsonDeserialize(using = CalendarDeserializer.class)
    @JsonSerialize(using = CalendarSerializer.class)
    protected Calendar sqlCommandsVersion;

    // Desktop app version
    protected int desktopAppVersion;
        
}
