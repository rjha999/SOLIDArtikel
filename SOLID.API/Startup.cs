using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SOLID.DAL.DBContext;
using SOLID.IRepository;
using SOLID.Repository;

namespace SOLID.API
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

            services.AddEntityFrameworkSqlServer();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            });

            services.AddDbContextPool<ProductContext>((serviceProvider, optionBuilder) =>
            {

                optionBuilder.UseSqlServer(Configuration.GetConnectionString("StoreConnection")
                    , optionsBuilder => optionsBuilder.MigrationsAssembly("SOLID.API"));
                optionBuilder.UseInternalServiceProvider(serviceProvider);
            });

            //Resolve dependency Injection, so that it can be injected in Controller or other places
            
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IArtikelRepository, ArtikelRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IColorCodeRepository, ColorCodeRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
