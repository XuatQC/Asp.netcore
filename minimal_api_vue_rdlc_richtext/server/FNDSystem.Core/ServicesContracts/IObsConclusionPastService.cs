using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IObsConclusionPastService
{
    IEnumerable<ObsConclusionPast> GetObsConclusionPastListByNum(string num);
}