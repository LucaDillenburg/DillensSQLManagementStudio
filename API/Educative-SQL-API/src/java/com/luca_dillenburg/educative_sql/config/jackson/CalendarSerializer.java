/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.config.jackson;

import com.luca_dillenburg.educative_sql.config.Config;
import java.io.IOException;
import java.util.Calendar;
import org.codehaus.jackson.JsonGenerator;
import org.codehaus.jackson.JsonProcessingException;
import org.codehaus.jackson.map.JsonSerializer;
import org.codehaus.jackson.map.SerializerProvider;

/**
 *
 * @author luca_dillenburg
 */
public class CalendarSerializer extends JsonSerializer<Calendar> {

    @Override
    public void serialize(Calendar calendar, JsonGenerator jgen, SerializerProvider provider) throws IOException, JsonProcessingException {
        final String formatedDate = Config.JSON_DATE_FORMAT.format(calendar.getTime());
        jgen.writeString(formatedDate);
    }

}