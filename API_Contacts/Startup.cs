using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Contacts.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using API_Contacts.Models;

namespace API_Contacts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            /// <summary>
            ///   the data provider can be changed here
            /// </summary>
            services.AddDbContext<DBContactsContext>(options =>
            options
            .UseSqlite("Filename = DBContactsSQLite.db")
            .UseLazyLoadingProxies());

            //SQL server data provider
            //.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddTransient<IRepository<Contact>, InMemoryRepository<Contact>>();
            services.AddTransient<IRepository<Skill>, InMemoryRepository<Skill>>();
            services.AddTransient<IRepository<ContactSkill>, InMemoryRepository<ContactSkill>>();
            services.AddControllers();

            /// <summary>
            ///   register the swagger generator, defining swagger document
            /// </summary>
            //register the swagger generator, defining 1 or more swagger documents
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "My API", 
                    Description = "An API to manage contact details and associated skills",  
                    Contact = new OpenApiContact
                    {
                        Name = "Github repo",
                        Url =new Uri( "https://github.com/michaelnikhil/API_Contacts")
                    },
                    Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DBContactsContext dBContactsContext)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My Contact API V1");
                //c.RoutePrefix = string.Empty;
            }
                ) ;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            dBContactsContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
