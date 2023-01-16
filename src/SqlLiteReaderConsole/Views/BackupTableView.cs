using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace IL2CarrerReviverConsole.Views;
internal class BackupTableView : IView<IEnumerable<DatabaseBackup>>
{
    private readonly IFileChecksumService fileChecksumService;

    public BackupTableView(IFileChecksumService fileChecksumService)
    {
        this.fileChecksumService = fileChecksumService;
    }

    public IRenderable GetView(IEnumerable<DatabaseBackup> entity)
    {
        var table = new Table();
        table.AddColumn("GUID");
        table.AddColumn("Name");
        table.AddColumn("Creation Date");
        table.AddColumn("File Found");
        table.AddColumn("Compromised");
        foreach (var backup in entity)
        {
            string checksum = fileChecksumService.GetChecksum(backup.BackupPath);
            bool isCompromised = checksum != backup.Checksum;
            bool fileMissing = !File.Exists(backup.BackupPath);
            var style = new Style(isCompromised || fileMissing ? Color.Red : Color.Green);
            IEnumerable<IRenderable> renderables = new List<IRenderable>
            {
                new Text(backup.Guid.ToString(), style),
                new Text(backup.DisplayName, style),
                new Text(backup.CreationDate.ToString(), style),
                new Text((!fileMissing).ToString(), style),
                new Text(isCompromised.ToString(), style),
            };
            table.AddRow(renderables);
        }
        return table;
    }
}
