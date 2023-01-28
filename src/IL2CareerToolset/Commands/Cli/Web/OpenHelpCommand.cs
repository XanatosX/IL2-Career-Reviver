using IL2CareerToolset.Services;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Web;

[Description("Open the online link for this application")]
internal class OpenHelpCommand : BaseOpenRepositoryLinkCommand
{
    public OpenHelpCommand(IConfiguration configuration, OSInteractionService oSInteractionService) : base(configuration, oSInteractionService)
    {
    }

    public override string LinkToOpen()
    {
        return configuration.Help;
    }
}
