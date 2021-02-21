using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using S6A0702.Business;
using S6A0702.Business.Implementation;
using S6A0702.Moldels.Context;
using S6A0702.Repository;
using S6A0702.Repository.Implementation;
using System.Linq;

namespace S5A0504
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
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                ;
            }));
            services.AddControllers();
            services.AddApiVersioning();

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
            }).AddXmlSerializerFormatters();

            services.AddDbContext<WebApi001Context>(options =>
            {
                var configuration = Configuration["ConnectionStrings:WebApi001"];
                options.UseMySql(configuration);
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "S6A0702 - 1.0", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "S6A0702 - 2.0", Version = "v2" });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
            });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IBookBusiness, BookBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "S6A0702 v2");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "S6A0702 v1");
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
