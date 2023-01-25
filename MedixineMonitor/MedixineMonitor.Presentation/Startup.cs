using MedixineMonitor.Application;
using MedixineMonitor.Application.Common.Abstraction;
using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Infrastructure;
using MedixineMonitor.Infrastructure.Caching;
using Microsoft.Extensions.Caching.Memory;
using FluentValidation.AspNetCore;

namespace MedixineMonitor.Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddMemoryCache();
            services.AddFluentValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddHttpClient();

            //We can have configuration and set default expire time for some items by key
            Dictionary<string, TimeSpan> configuration = new();
            services.AddSingleton<ICacheStore>(x => new MemoryCacheStore(x.GetService<IMemoryCache>()!, configuration));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            //var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
                        options.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .WithMethods("GET", "PUT", "DELETE")
                        .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.MapControllers();

            app.MapHub<BaseAlertHub>("/AlertNotifications");

            app.Run();
        }
    }
}
