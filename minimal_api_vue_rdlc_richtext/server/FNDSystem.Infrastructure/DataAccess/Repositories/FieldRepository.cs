using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class FieldRepository : GenericRepository<Field>, IFieldRepository
{
    public FieldRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    #region 分野一覧取得
    /// <summary>
    /// 分野一覧取得
    /// </summary>
    /// <param name="findFieldDto">条件検索</param>
    /// <returns>分野マスタリスト</returns>
    public IEnumerable<FieldResponseDto>? GetFieldList(FindFieldRequestDto findFieldDto)
    {
        ObjectUtils.NullToDefault<FindFieldRequestDto>(findFieldDto);
        var resultList = this.slaveDBContext?.FieldResponses?
                             .FromSql($@"Call Sp_Get_Field ({findFieldDto.PlantName});")
                             .ToList();
        return resultList;
    }
    #endregion

    #region 分野11件取得
    /// <summary>
    /// 分野11件取得
    /// </summary>
    /// <param name="findFieldDto">条件検索</param>
    /// <returns> 分野11</returns>
    public IEnumerable<Field>? Get_11Records_Field(FindFieldRequestDto findFieldDto)
    {
        ObjectUtils.NullToDefault<FindFieldRequestDto>(findFieldDto);
        var resultList = this.slaveDBContext?.Fields?
                             .FromSql($@"Call Sp_Get_11Records_Field ({findFieldDto.PlantName},{findFieldDto.DmDiv});")
                             .ToList();
        return resultList;
    }
    #endregion
}