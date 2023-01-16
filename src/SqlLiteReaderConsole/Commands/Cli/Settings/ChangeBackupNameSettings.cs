using Spectre.Console.Cli;

namespace IL2CarrerReviverConsole.Commands.Cli.Settings;
internal class ChangeBackupNameSettings : CommandSettings
{
    [CommandArgument(0, "<BACKUP_GUID>")]
    public string? Guid { get; set; }

    public string? Name { get; set; }
}
