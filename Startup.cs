using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Data;
using HuskyBot.Interfaces;
using HuskyBot.Interfaces.IServices;
using HuskyBot.Services;
using Microsoft.EntityFrameworkCore;

namespace HuskyBot
{
    public class Startup
    {
        public IConfiguration _config {get;}
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConnections( );

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpClient();

            services.AddSingleton<IDiscordBotService, DiscordBotService>();

            services.AddSingleton<IUserLevelService, UserLevelService>();
            services.AddScoped<IUserMessageService, UserMessageService>();
            services.AddSingleton<ITwitchService, TwitchService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DataContext>(options => {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                string connStr;

                // Depending on if in development or production, use either MSSQL-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = _config.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // Use connection string provided at runtime by Azure.
                    connStr = Environment.GetEnvironmentVariable("DATABASE_URL");
                }
                options.UseSqlServer(connStr);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            } else {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            // Configure the HTTP request pipeline.
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}