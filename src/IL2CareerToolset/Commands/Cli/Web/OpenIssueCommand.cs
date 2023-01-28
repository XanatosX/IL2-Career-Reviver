using IL2CareerToolset.Services;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Web;

[Description("Open the link to create issues in the web browser")]
internal class OpenIssueCommand : BaseOpenRepositoryLinkCommand
{
    public OpenIssueCommand(IConfiguration configuration, OSInteractionService oSInteractionService) : base(configuration, oSInteractionService)
    {
    }

    public override string LinkToOpen()
    {
        return configuration.Issue;
    }
}
