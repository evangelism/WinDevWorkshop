using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using uwpdevholsService.DataObjects;
using uwpdevholsService.Models;
using Owin;

namespace uwpdevholsService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new uwpdevholsInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer(null);

            app.UseMobileAppAuthentication(config);
            app.UseWebApi(config);
        }
    }

    public class uwpdevholsInitializer : CreateDatabaseIfNotExists<uwpdevholsContext>
    {
        protected override void Seed(uwpdevholsContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), UserId = "", Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), UserId = "", Text = "Second item", Complete = false },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

