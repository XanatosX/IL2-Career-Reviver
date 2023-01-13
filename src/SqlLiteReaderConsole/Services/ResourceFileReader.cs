using System.Reflection;

namespace IL2CarrerReviverConsole.Services;
internal class ResourceFileReader
{
    private const string PREFIX = "IL2CarrerReviverConsole.Resources.";
    public string GetResourceContent(string name)
    {
        string fullPath = PREFIX + name;
        string returnValue = string.Empty;
        var assembly = Assembly.GetExecutingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream(fullPath))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                returnValue = reader.ReadToEnd();
            }
        }
        return returnValue;
    }
}
