using System;
using System.Text.Json;
using SebGlow.DataAccess;
using SebGlow.GitHub;
using SebGlow.GitHub.config;
using SebGlow.Service;
using SebGlow.Service.Mapping;
using SebGlow.Service.Model;
using SebGlow.Service.Provider;
using SebGlowGitApi.config;
using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SebGlowGitApi
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
            var connectionStrings = Configuration.GetSection("ConnectionStrings");
            var gitHubSettings = Configuration.GetSection("GitHub");

            services.Configure<GitHubConfig>(gitHubSettings);
            services.Configure<ConnectionStrings>(connectionStrings);

            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IGitHubStatisticsService, GitHubStatisticsService>();

            services.AddHttpClient<IRepositoryClient, GitHubClient>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.Converters.Add(new LettersJsonConverter()))
                ;

            if (Environment.GetEnvironmentVariable("ADD_DB_CONTEXT") == "SqlServer")
            {
                services.AddDbContext<SebGlowDbContext>(options =>
                {
                    options.UseSqlServer(
                        connectionStrings.Get<ConnectionStrings>().SebGlowDb);
                });
            }
            else
            {
                services.AddDbContext<SebGlowDbContext>(options => options.UseInMemoryDatabase(databaseName: "SebGlowDb"));
            }

            services.AddAutoMapper(typeof(StatisticsMappingProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Api for GitHub repos statistics",
                    Description = "Retrieves statistics of user's repositories from GitHub api endpoint"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SebGlowDbContext dataContext)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (!dataContext.Database.IsInMemory())
                dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
