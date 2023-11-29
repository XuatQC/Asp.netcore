using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Dto.OBS;

namespace FNDSystem.Core.ServicesContracts;

public interface IObsService
{
    #region obs

    Obs? GetObsByNum(string num);
    IEnumerable<NumberControl>? GetObsRevisionList(FindObsRequestDto findObsDto);
    IEnumerable<Obs>? GetObsListByMultiCondition(FindObsRequestDto findObsDto);
    IEnumerable<Obs>? GetObsListForReqTran2(FindObsRequestDto obsDto);
    int GetMaxNo(FindObsRequestDto obsDto);
    IEnumerable<NumObsDto>? GetNum(string plantName, string fields);
    Obs Create(ObsDto obsDto);
    Obs? UpdateByNum(string num, ObsExtend obsDto);
    bool Delete(DeleteObsDto deleteObsDto);
    bool DeleteDataOfWktTables();
    Obs? AddNewObsAtScreenList(FindObsRequestDto obsExtendDto);
    string ObsSaveProcess(string userInitial, ObsSaveDto obsSaveDto,string diskPath);
    bool ObsWorkTableConversion(string num, string transSource);
    #endregion

    #region obs_extend

    IEnumerable<DisObsReponseDto>? GetObsExtendList(FindObsRequestDto obsExtendDto);
    IEnumerable<VwObsControl>? GetOnlyObsExtendList(FindObsRequestDto obsExtendDto);
    IEnumerable<DisObsReponseDto>? GetObsExtendListWhenClearClick(FindObsRequestDto obsExtendDto);
    int GetCountObsExtendList(FindObsRequestDto obsExtendDto);
    ObsExtend? GetOnlyObsExtend(string num);

    #endregion

    #region obs_extend_refer

    string GetObsExtendReferNum(FindObsRequestDto findObsExtenReferDto);
    IEnumerable<ObsExtendReferDto>? GetObsExtendReferListByNum(FindObsExtendReferRequestDto findObsExtendReferDto);

    #endregion
}
