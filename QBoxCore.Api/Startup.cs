using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using QBox.Api.Migrations;
using QBoxCore.Api.Models;
using Polly;
using System;
using Microsoft.EntityFrameworkCore;

namespace QBoxCore.Api
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


            services.AddTransient<DbInitializer>(d => new DbInitializer(new QuizBoxContext()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try { 
            var sqlRetryPolicy = Policy
              .Handle<Exception>()
              .WaitAndRetry(new[]
              {
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
              });

            sqlRetryPolicy.Execute(() =>
            {
                InitializeDatabase(initializer);
            });
            }
            catch( Exception)
            {
            }

        }

        private void InitializeDatabase(DbInitializer initializer)
        {
            using (var context = new QuizBoxContext())
            {
                context.Database.Migrate();
            }

            initializer.Seed();
        }
    }
}
