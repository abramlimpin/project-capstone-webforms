<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<script runat="server">

    void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute(
            "AdviserRoute",
            "adviser/{name}",
            "~/adviser.aspx", true, new RouteValueDictionary() { { "name", 0 } },
                new RouteValueDictionary() { { "name", "[a-z0-9]+" } }
        );
    }

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        RegisterRoutes(RouteTable.Routes);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        Exception exc = Server.GetLastError();
        Helper.Log("Error", Server.HtmlEncode(exc.Message + " - " +
            exc.InnerException.Message));
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
    }
       
</script>
