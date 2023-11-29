using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IObsAttachService
{
    IEnumerable<ObsAttach>? GetObsAttachListByImageCode(string num);
    ObsAttach? GetObsAttachByImageCode(string num);
    bool? InsertObsAttachs(List<ObsAttachRequestDto> obsAttachs);

}