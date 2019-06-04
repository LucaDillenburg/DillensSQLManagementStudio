/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.luca_dillenburg.educative_sql;

import com.luca_dillenburg.educative_sql.versions.Versions;
import com.luca_dillenburg.educative_sql.versions.info_versions.desktop_app_info.DesktopAppInfo;
import com.luca_dillenburg.educative_sql.versions.info_versions.desktop_app_updates.DesktopAppUpdates;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Produces;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.PathParam;
import javax.ws.rs.core.MediaType;

/**
 * REST Web Service
 *
 * @author luca_dillenburg
 */
@Path("educative-sql")
public class EducativeSQLService {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of EducativeSQLService
     */
    public EducativeSQLService() {
    }

    
    @PUT
    @Path("/verify_versions")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public DesktopAppUpdates verificarVersao(DesktopAppInfo desktopApp)
    {
        return Versions.checkVersions(desktopApp);
    }
    
    
    /**
     * Retrieves representation of an instance of com.luca_dillenburg.educative_sql.EducativeSQLService
     * @return an instance of java.lang.String
     */
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public String getJson() {
        //TODO return proper representation object
        throw new UnsupportedOperationException();
    }

    /**
     * PUT method for updating or creating an instance of EducativeSQLService
     * @param content representation for the resource
     */
    @PUT
    @Consumes(MediaType.APPLICATION_JSON)
    public void putJson(String content) {
    }
}
