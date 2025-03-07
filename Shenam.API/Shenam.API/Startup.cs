//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shenam.API.Brokers.Storages;
using System;

namespace Shenam.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var apiinfo = new OpenApiInfo
            {
                Title = "Shenam.API",
                Version = "v1"
            };
            services.AddDbContext<StorageBroker>();
            services.AddControllers();
            services.AddTransient<IStorageBroker, StorageBroker>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name:"v1", 
                    info:apiinfo);
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment envirovment)
        {
            if (envirovment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "Shenam.API v1");
                });                
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>            
                endpoints.MapControllers());
        }
    }
}
