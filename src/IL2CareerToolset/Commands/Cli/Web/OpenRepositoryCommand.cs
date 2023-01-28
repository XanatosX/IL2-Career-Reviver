using IL2CareerToolset.Services;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Web;

[Description("Open the link to the public repository of the project")]
internal class OpenRepositoryCommand : BaseOpenRepositoryLinkCommand
{
    public OpenRepositoryCommand(IConfiguration configuration, OSInteractionService oSInteractionService) : base(configuration, oSInteractionService)
    {
    }

    public override string LinkToOpen()
    {
        return configuration.Link;
    }
}
