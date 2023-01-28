using IL2CareerToolset.Services;
using Microsoft.Extensions.Configuration;

namespace IL2CareerToolset.Commands.Cli.Web;
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
