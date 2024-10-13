using AutoMapper;
using TechChallenge.Data.Context;
using TechChallenge.Data.Repositories;
using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Entities.Requests;
using TechChallenge.Domain.Interfaces.Repositories;
using TechChallenge.Domain.Interfaces.Services;
using TechChallenge.Manager.Services;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace TechChallenge.Api.Options.IoC
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjection       
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Prometheus
            services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter();
            });

            // Connection strings
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BdPadraoConnection")));

            // JWT
            var jwtOptions = configuration.GetSection("JwtOptions");

            //Auto Mapper
            var autoMapperConfig = new MapperConfiguration(cfg => { 
                cfg.CreateMap<RegistrarContatoRequest, Contato>().ReverseMap(); 
                cfg.CreateMap<AtualizarContatoRequest, Contato>().ReverseMap(); 
            
            });
            services.AddSingleton(autoMapperConfig.CreateMapper());

            // Repositórios
            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<IDDDRepository, DDDRepository>();

            // Services          
            services.AddScoped<IContatoService, ContatoService>();
            services.AddScoped<IDDDService, DDDService>();
            return services;
        }
    }
}
