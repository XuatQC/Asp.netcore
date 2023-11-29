using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.API.Configuration;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<Account, CreateAccountRequestDto>();
        // From - To
        CreateMap<CreateAccountRequestDto, Account>();
        CreateMap<Account, AccountDto>();
        CreateMap<Account, LoginResponseDto>();
        CreateMap<Translate, WktTranslate>();
        CreateMap<User,LoginResponseDto>();
        CreateMap<ObsRequestDto, Obs>();
        CreateMap<ObsExtend, ObsResponseDto> ();
        CreateMap<ObsFactRequestDto, ObsFact>();
        CreateMap<ObsFact, ObsFactResponseDto > ();
        CreateMap<ObsConclusionRequestDto, ObsConclusion>();
        CreateMap<ObsConclusion, ObsConclusionResponseDto> ();
        CreateMap<ObsExtend, Obs>();
        CreateMap<ObsAttachDto, ObsAttach>();
        CreateMap<ObsAttachRequestDto, ObsAttach>();
        CreateMap<WktObsFactDto, WktObsFact>();
        CreateMap<WktObsFactDeleteDto, WktObsFact>();
        CreateMap<UpdateObsDto, Obs>();
        CreateMap<WktObsFactDto, WktObsFact>();
        CreateMap<WktObsConclusionDto, WktObsConclusion>();
        CreateMap<ObsFactRequestDto, ObsFact>();
        CreateMap<ObsConclusionRequestDto, ObsConclusion>();
        // CreateMap<PersonViewModel, Person>();
    }
}