using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IReviewMemberService
{
    IEnumerable<ReviewMemberResponseDto>? GetReviewMemberList(FindReviewMemberRequestDto findDto);
    ReviewMember? GetReviewMember(FindReviewMemberRequestDto findDto);
    IEnumerable<ReviewMemberEditorResponseDto>? GetReviewMemberEditorList(string plantName);
    
}