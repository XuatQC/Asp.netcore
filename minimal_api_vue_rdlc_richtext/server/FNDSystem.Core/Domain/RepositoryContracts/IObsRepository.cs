using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Dto.OBS;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IObsRepository : IGenericRepository<Obs>
{
    #region obs

    IEnumerable<NumberControl>? GetObsRevisionList(FindObsRequestDto findObsDto);
    IEnumerable<Obs>? GetObsListByMultiCodition(FindObsRequestDto findObsDto);
    IEnumerable<NumObsDto>? GetNum(string plantName, string fields);
    bool Delete(DeleteObsDto deleteObsDto);
    bool DeleteDataOfWktTables();
    int GetMaxNo(FindObsRequestDto obsDto);
    Obs? AddNewObsAtScreenList(FindObsRequestDto obsExtendDto);
    #endregion

    #region obs extend

    IEnumerable<DisObsReponseDto>? GetObsExtendList(FindObsRequestDto obsExtendDto);
    IEnumerable<DisObsReponseDto>? GetObsExtendListWhenClearClick(FindObsRequestDto obsExtendDto);
    #endregion

    #region obs extend refer
    string GetReqNumOfObsExtendRefer(FindObsRequestDto findObsDto);
    string GetReqNumOfObsExtendReferByNum(FindObsExtendReferRequestDto findObsExtendReferDto);
    IEnumerable<ObsExtendReferDto>? GetObsExtendReferListByNum(FindObsExtendReferRequestDto findObsExtendReferDto);
    List<ObsExtendReferDto>? GetTblObsExtendRefer();
    #endregion

    #region Request translate 2
    IEnumerable<Obs>? GetObsListForReqTran2(FindObsRequestDto obsDto);

    #endregion
    IEnumerable<ObsExtend>? GetObsExtends(string num);
}