using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RedisHelp
{
    public class ConfigurationTool
    {

        public static T GetAppSettings<T>(string key, IConfiguration config) where T : class, new()
        {
            T appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;

            return appconfig;
        }

    }
}
