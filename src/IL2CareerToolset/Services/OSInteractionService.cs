using System.Diagnostics;

namespace IL2CareerToolset.Services;
internal class OSInteractionService
{
    public void OpenLinkInBrowser(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            return;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = link,
            UseShellExecute = true,
        };
        Process.Start(startInfo);
    }
}
