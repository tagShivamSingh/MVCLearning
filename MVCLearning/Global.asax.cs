﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MVCLearning.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCLearning
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Web.Optimization.BundleTable.EnableOptimizations = true;
            AutomapperWebProfile.Run();

            Dynamic 365(.net)
CrmServiceClient(




SalesForce(.net)
SforceService
PartnerService.SforceService
SforceService

office 365(.net)
SharePointOnlineCredentials
        }
        
    }
}
