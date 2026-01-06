using Microsoft.Extensions.Configuration;

namespace ConfigurationBuilder;

public class ConfigSection(IConfigurationRoot configurationRoot)
{
    public T GetConfigSection<T>() => configurationRoot.GetSection(typeof(T).Name).Get<T>();

    public T GetConfigSection<T>(string sectionName) => configurationRoot.GetSection(sectionName).Get<T>();
}