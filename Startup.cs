using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using webapi.Services;
using Microsoft.Extensions.Configuration;
namespace webapi {
    public class Startup {
        public static IConfiguration Configuration{set; get;}

        public Startup(IConfiguration configuration){
            Configuration=configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<BookDbContext> (options =>
                options.UseNpgsql ("Host=localhost;Database=BookApiProject;Username=postgres;Password=root"));
            services.AddMvc (option => option.EnableEndpointRouting = false);
            services.AddScoped<ICountryRepository, CountryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, BookDbContext context) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            // app.UseRouting ();

            // app.UseEndpoints (endpoints => {
            //     endpoints.MapGet ("/", async context => {
            //         await context.Response.WriteAsync ("Hello World!");
            //     });
            // });

            //Seeding Data For The First Time
            //context.SeedDataContext();
            app.UseMvc();
        }
    }
}