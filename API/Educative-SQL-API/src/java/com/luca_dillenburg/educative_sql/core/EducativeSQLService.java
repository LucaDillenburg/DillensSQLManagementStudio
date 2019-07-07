/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql.core;

import com.luca_dillenburg.educative_sql.config.Config;
import com.luca_dillenburg.educative_sql.storage.versions.desktop_app.DesktopApp;
import com.luca_dillenburg.educative_sql.storage.versions.updates.UpdatesOnServer;
import java.io.IOException;
import java.net.URLDecoder;
import java.util.Calendar;
import java.util.logging.Logger;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Produces;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.PathParam;
import javax.ws.rs.WebApplicationException;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import org.codehaus.jackson.map.ObjectMapper;
import org.xml.sax.SAXException;

/**
 * REST Web Service
 *
 * @author luca_dillenburg
 */
@Path("educative-sql")
public class EducativeSQLService {

    @Context
    private UriInfo context;

    
    @GET
    @Path("/verify_versions/{jsonEncodedDesktopApp}")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public UpdatesOnServer verifyVersion(@PathParam("jsonEncodedDesktopApp") String jsonEncodedDesktopApp)
    {
        //get desktop app json and convert to object
        final DesktopApp desktopApp;
        try {
            final String jsonDesktopApp = URLDecoder.decode(jsonEncodedDesktopApp, Config.ENCODING_TYPE);
            desktopApp = new ObjectMapper().readValue(jsonDesktopApp, DesktopApp.class);
        } catch (IOException e) {
            throw new WebApplicationException(
                    Response.status(Response.Status.EXPECTATION_FAILED)
                        .entity("Invalid Json object for DesktopApp class! Error: " + e)
                        .build());
        }
        
        //do the verifications needed
        try {
            return Versions.checkVersions(desktopApp);
        } catch (SAXException | IOException | IllegalStateException e) {
            throw new WebApplicationException(
                    Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                        .entity("XML SQL Explanation file is not correct! Error: " + e)
                        .build());
        }
    }
    
}
