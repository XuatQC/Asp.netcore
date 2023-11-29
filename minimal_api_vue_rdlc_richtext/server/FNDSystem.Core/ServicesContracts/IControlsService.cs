using FNDSystem.Core.Dto;
using System.Collections.Generic;

namespace FNDSystem.Core.ServicesContracts;

public interface IControlsService
{
    DateTime? GetLastSyncDate();
}

