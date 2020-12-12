using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System.IO;
using URLShortner.Domain;
using URLShortner.Models;
using URLShortner.Persistence;

namespace URLShortner
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

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "URL Shortner", Version = "v1" });
            //});
            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo
                  {
                      Version = "v1",
                      Title = "URl Shortner",
                      Description = "Shortens the URLS for ease of use",
                      //TermsOfService = "",
                      Contact = new OpenApiContact { Name = "Asif Bhat", Email = "bhatasif17@gmail.com" },
                      License = new OpenApiLicense { Name = "MIT License " }
                  });

                  // Set the comments path for the Swagger JSON and UI.
                  var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                  var xmlPath = Path.Combine(basePath, "URLShortner.xml");
                  c.IncludeXmlComments(xmlPath);
              });


            services.AddScoped<IURLShortnerService, URLShortnerService>();
            services.AddScoped<IURLShortnerRepository, URLShortnerRepository>();
            services.AddScoped<URLShortnerContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                // app.UseSwaggerUI(
                //   c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "URLShortner v1"));
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "URLShortner v1");
                    c.RoutePrefix = "swagger/ui";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
