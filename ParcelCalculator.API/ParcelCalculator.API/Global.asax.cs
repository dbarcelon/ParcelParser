﻿using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace ParcelCalculator.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer windsorContainer;
        protected void Application_Start()
        {
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            //    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters
            //    .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, windsorContainer));
            WebApiConfig.Register(GlobalConfiguration.Configuration, windsorContainer);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, ParcelCalculator-Authentication-Header");
                HttpContext.Current.Response.AddHeader("Access-Control-Expose-Headers", "ParcelCalculator-Authentication-Header");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }


        protected void Application_End()
        {
            windsorContainer.Dispose();
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            windsorContainer = new WindsorContainer();
            windsorContainer.Install(FromAssembly.This());
            windsorContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(windsorContainer.Kernel, true));
            var dependencyResolver = new ParcelCalculator.API.CastleWindsor.WindsorDependencyResolver(windsorContainer);
            configuration.DependencyResolver = dependencyResolver;
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            //return true;
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.IndexOf("/api") > 0;
        }

    }
}
