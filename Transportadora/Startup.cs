using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Transportadora.Data;
using Transportadora.Repositories;
using Transportadora.Services;
using Transportadora.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Transportadora
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TransporteContext");
            services.AddDbContext<TransporteContext>(options =>
                options.UseNpgsql(connectionString));
         
            // Agregar servicios necesarios
            services.AddScoped<ICargadorService, CargadorService>();
            services.AddScoped<ICargadorRepository, CargadorRepository>();
            services.AddScoped<IAutobusRepository, AutobusRepository>();
            services.AddScoped<IAutobusService, AutobusService>();
                

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
