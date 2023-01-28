using IL2CareerToolset.Model;
using IL2CareerToolset.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console.Cli;

namespace IL2CareerToolset.Commands.Cli.Web;
internal abstract class BaseOpenRepositoryLinkCommand : Command
{
    protected readonly RepositoryConfiguration configuration;
    protected readonly OSInteractionService oSInteractionService;

    protected BaseOpenRepositoryLinkCommand(IConfiguration configuration, OSInteractionService oSInteractionService)
    {
        this.configuration = configuration.GetRequiredSection("Repository").Get<RepositoryConfiguration>() ?? new RepositoryConfiguration
        {
            Issue = string.Empty,
            Link = string.Empty,
            Help = string.Empty
        };
        this.oSInteractionService = oSInteractionService;
    }

    public override int Execute(CommandContext context)
    {
        oSInteractionService.OpenLinkInBrowser(LinkToOpen());
        return 0;
    }

    public abstract string LinkToOpen();
}
