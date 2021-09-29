using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thesaurus.Api.Business;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Business.Model.Interface;
using Thesaurus.Api.Business.Model.Mapper;
using Thesaurus.Api.Repository.Base;
using Thesaurus.Api.Repository.Interface;

namespace Thesaurus.API
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
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "An api to find synonyms of a word",
                    Title="Thesaurus API",
                    Contact=new OpenApiContact
                    {
                        Name="Pulkit"
                    }
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:17634")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var uri = Configuration.GetSection("MongoDbSettings").GetSection("MongoUri").Value;
                return new MongoClient(uri);
            });
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            });

            services.AddScoped<IWordLogic, WordLogic>();
            services.AddScoped<IMapper<Words, Word>, WordMapper>();
            services.AddScoped<IMapper<Word, Words>, ThesaurusRepoMapper>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c=>c.SerializeAsV2=true);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thesaurus API");
            });

            app.UseRouting();
            app.UseCors("MyAllowSpecificOrigins");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
