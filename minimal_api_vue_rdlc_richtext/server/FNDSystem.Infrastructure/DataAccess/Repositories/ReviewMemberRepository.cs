using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class ReviewMemberRepository : GenericRepository<ReviewMember>, IReviewMemberRepository
{
    public ReviewMemberRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    #region ピアレビューメンバマスタの一覧取得
    /// <summary>
    /// ピアレビューメンバマスタの一覧取得
    /// </summary>
    /// <param name="findDto">検索条件</param>
    /// <returns>ピアレビューメンバマスタ 一覧</returns>
    public IEnumerable<ReviewMemberResponseDto>? GetReviewMemberList(FindReviewMemberRequestDto findDto)
    {
        if (findDto.PlantName == null) findDto.PlantName = string.Empty;
        var resultList = this.slaveDBContext?.ReviewMemberResponses?
                             .FromSql($@"Call Sp_Get_Initial ({findDto.PlantName});")
                             .ToList();
        return resultList;
    }
    #endregion

    #region plantNameでReviewMemberEditorの一覧を取得する
    /// <summary>
    /// plantNameでReviewMemberEditorの一覧を取得する
    /// </summary>
    /// <param name="plantName"></param>
    /// <returns></returns>
    public IEnumerable<ReviewMemberEditorResponseDto>? GetReviewMemberEditor(string plantName)
    {
        var resultList = this.slaveDBContext?.ReviewMemberEditorResponseDto?
                                                .FromSql($@"call Sp_Get_Review_Member_Editor({plantName})")
                                                .ToList();
        return resultList;

    }
    #endregion
}