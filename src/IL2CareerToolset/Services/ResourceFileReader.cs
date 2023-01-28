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

    public Stream GetResourceStream(string name)
    {
        logger.LogDebug($"Get stream from resources. Requested file : {name}");
        string fullPath = PREFIX + name;
        return Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath) ?? Stream.Null;
    }

    public string GetResourceContent(string name)
    {
        return cache.GetOrCreate<string>(name, entry =>
        {
            string returnValue = string.Empty;
            using (Stream stream = GetResourceStream(name))
            {
                if (stream != Stream.Null)
                {
                    logger.LogDebug($"Get text from resources. Requested file : {name}");
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
