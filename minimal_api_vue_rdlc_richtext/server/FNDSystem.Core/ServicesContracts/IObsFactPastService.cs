using FNDSystem.Core.Domain.Entities;

namespace FNDSystem.Core.ServicesContracts;

public interface IObsFactPastService
{
    IEnumerable<ObsFactPast> GetObsFactPastListByNum(string num);
    //ObsFactPast Create(ObsFactPastDto obsFactDto);
    //ObsFactPast UpdateById(int id, ObsFactPastDto obsFactDto);
    //bool UpdateByNum(string num, ObsFactPastDto obsFactDto);
}