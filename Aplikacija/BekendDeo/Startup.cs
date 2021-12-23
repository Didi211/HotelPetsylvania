using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BekendDeo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BekendDeo.APILogika;
using BekendDeo.Helpers;
using BekendDeo.AuthentificationService;

namespace BekendDeo
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
            services.AddTransient<IDataProvider, DataProvider>();
            services.AddTransient<IDataProviderAdmin,DataProviderAdmin>();
            services.AddTransient<IDataProviderMusterija,DataProviderMusterija>();
            services.AddTransient<IDataProviderRadnik,DataProviderRadnik>();
            
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddControllers().AddMvcOptions(x => x.Filters.Add(new AuthorizeAttribute()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BekendDeo", Version = "v1" });
            });
            services.AddCors(p =>
            {
                p.AddPolicy("CORS", builder =>
                 {
                     builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowAnyOrigin();

                 });
            });
            services.AddDbContext<HotelContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("HotelPetsylvaniaCS"));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BekendDeo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
