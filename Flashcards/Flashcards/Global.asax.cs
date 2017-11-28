using Common.Processses;
using Flashcards.Code;
using Flashcards.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Flashcards
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            addRolesIfNotExist();
        }

        private void addRolesIfNotExist()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                createRoleIfNotExist(roleManager, Groups.Administrator);
                createRoleIfNotExist(roleManager, Groups.User);
            }
        }

        private static void createRoleIfNotExist(RoleManager<IdentityRole> roleManager, string groupName)
        {
            if (roleManager.RoleExists(groupName) == false)
                roleManager.Create(new IdentityRole { Name = groupName });
        }

        public void test()
        {
        }

        protected void Application_End()
        {
        }
    }
}
