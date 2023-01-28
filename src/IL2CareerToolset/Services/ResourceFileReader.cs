using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.Reflection;

namespace IL2CareerToolset.Services;
internal class ResourceFileReader
{
    private const string PREFIX = "IL2CareerToolset.Resources.";
    private readonly IMemoryCache cache;
    private readonly ILogger<ResourceFileReader> logger;

    public ResourceFileReader(IMemoryCache cache, ILogger<ResourceFileReader> logger)
    {
        this.cache = cache;
        this.logger = logger;
    }

    public string GetResourceContent(string name)
    {
        return cache.GetOrCreate<string>(name, entry =>
        {
            logger.LogDebug($"Get text from resources. Requested file : {name}");
            string fullPath = PREFIX + name;
            string returnValue = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fullPath) ?? Stream.Null)
            {
                if (stream != Stream.Null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        returnValue = reader.ReadToEnd();
                    }
                }
            }

            return returnValue;
        }) ?? string.Empty;

    }
}
