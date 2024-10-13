using NLog.Extensions.Logging;

namespace TechChallenge.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggerSetupExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ResolveLog(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog("NLog.config");
            });

            return services;
        }
    }
}
