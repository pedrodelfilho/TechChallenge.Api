using AutoMapper;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using TechChallenge.Data.Repositories;
using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Entities.Requests;
using TechChallenge.Domain.Interfaces.Repositories;
using TechChallenge.Domain.Interfaces.Services;
using TechChallenge.Manager.Services;

namespace TechChallenge.Api.Options.IoC
{
    /// <summary>
    /// Classe para registro de dependências.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Método para registrar os serviços na injeção de dependência.
        /// </summary>
        /// <param name="services">Coleção de serviços.</param>
        /// <param name="configuration">Configuração da aplicação.</param>
        /// <returns>Coleção de serviços atualizada.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Prometheus e OpenTelemetry
            services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("techchallenge-api"))
                    .AddPrometheusExporter();
            });

            // Auto Mapper
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
