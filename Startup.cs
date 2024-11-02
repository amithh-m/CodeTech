
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using BussinessLayer.BussinessLogic;
using BussinessLayer.BussinessLogic.Interface;


namespace CodeTechTech
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
            services.AddDistributedMemoryCache();

            services.AddMvc();




            services.AddDbContext<ApplicationDbContext>(
               options =>
               {
                   options.UseSqlServer(Configuration.GetConnectionString("DbConnectSql"));

               }
               );

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);

                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddTransient<Login_in, LoginLogics>();
            services.AddTransient<Customer_in, CustomerLogics>();

            services.AddCors(options =>
            {  // add cors
                options.AddDefaultPolicy(builder =>
                {

                    builder.WithOrigins("https://localhost:7089").AllowAnyHeader().AllowAnyMethod();

                });
            });


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
                    c.RoutePrefix = string.Empty;  // Set to serve Swagger UI at the root URL
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();  // Enable CORS as configured
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
