namespace MedicalServicesManagement.WebApp.Extensions
{
    public static class OptionsExtensions
    {
        public static void RegisterOptions<TOptions>(this IServiceCollection services, string sectionName)
            where TOptions : class
        {
            services.AddOptions<TOptions>()
                .Configure<IConfiguration>((settings, config) =>
                {
                    config.GetSection(sectionName).Bind(settings);
                });
        }
    }

}
