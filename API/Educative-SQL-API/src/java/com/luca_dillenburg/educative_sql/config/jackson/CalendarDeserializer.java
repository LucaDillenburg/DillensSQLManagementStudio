/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.config.jackson;

import com.luca_dillenburg.educative_sql.config.Config;
import java.io.IOException;
import java.text.ParseException;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.codehaus.jackson.JsonNode;
import org.codehaus.jackson.JsonParser;
import org.codehaus.jackson.JsonProcessingException;
import org.codehaus.jackson.map.DeserializationContext;
import org.codehaus.jackson.map.JsonDeserializer;

/**
 *
 * @author luca_dillenburg
 */
public class CalendarDeserializer extends JsonDeserializer<Calendar> {
    
    @Override
    public Calendar deserialize(JsonParser jp, DeserializationContext dc) throws IOException, JsonProcessingException {
        try {
            
            // get string
            JsonNode node = jp.getCodec().readTree(jp);
            final String formatedDate = node.asText();
            
            //convert
            final Calendar calendar = new GregorianCalendar();
            calendar.setTime(Config.JSON_DATE_FORMAT.parse(formatedDate));
            return calendar;
            
        } catch (ParseException ex) {
            Logger.getLogger(CalendarDeserializer.class.getName()).log(Level.SEVERE, "Client error: wrong date format! ", ex);
            return null;
        }
    }

}
