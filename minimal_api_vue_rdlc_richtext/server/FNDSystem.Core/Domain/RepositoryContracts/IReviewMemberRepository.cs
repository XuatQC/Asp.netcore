using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IReviewMemberRepository : IGenericRepository<ReviewMember>
{
    IEnumerable<ReviewMemberResponseDto>? GetReviewMemberList(FindReviewMemberRequestDto findDto);
    IEnumerable<ReviewMemberEditorResponseDto>? GetReviewMemberEditor(string plantName);

}