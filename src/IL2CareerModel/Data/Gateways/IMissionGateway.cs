﻿using IL2CareerModel.Data.Gateways;
using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverModel.Data.Gateways;

/// <summary>
/// The gateway to change the mission table on the database
/// </summary>
public interface IMissionGateway : IReadGateway<Mission>, IWriteGateway<Mission>
{
}
