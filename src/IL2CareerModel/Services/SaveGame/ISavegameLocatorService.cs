using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Services.SaveGame;

public interface ISavegameLocatorService
{
    string DisplayName { get; }

    int Priority { get; }

    string? GetSavegamePath();
}
