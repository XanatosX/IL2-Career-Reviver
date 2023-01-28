using IL2CareerToolset.Model;
using IL2CareerToolset.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
