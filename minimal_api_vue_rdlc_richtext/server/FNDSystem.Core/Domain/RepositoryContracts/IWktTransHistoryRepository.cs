using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IWktTransHistoryRepository : IGenericRepository<WktTransHistory>
{
    void InsertByScope(int pDisplayOrder
                            , string pItemName
                            , string pItemNameJp
                            , string pItemNameEn
                            , string? pPlantName
                            , string? pKinds
                            , string? pField
                            , string? pPartId
                            , int? pSerialNum
                            , int? pRevisions
                            , string? pEnglishEdition);
    List<WktTransHistoryExtendDto>? GetWktTransHistoryByParentAndChildTableName(string pParentTableName
                                                                                , string pChildTableName
                                                                                , string pItemName
                                                                                , string pItemNameJp
                                                                                , string pItemNameEn
                                                                                , string? pPlantName
                                                                                , string? pKinds
                                                                                , string? pField
                                                                                , string? pPartId
                                                                                , int? pSerialNum
                                                                                , int? pRevisions
                                                                                , string? pEnglishEdition);
}