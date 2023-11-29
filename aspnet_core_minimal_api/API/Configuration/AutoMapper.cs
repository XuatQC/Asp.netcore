
using Core.Models;
using AutoMapper;
using Core.Entities;

namespace Configuration;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<Account, CreateAccountRequestDto>();
        // From - To
        CreateMap<CreateAccountRequestDto, Account>();
        CreateMap<Account, AccountDto>();
        CreateMap<Account, LoginResponseDto>();

        // CreateMap<PersonViewModel, Person>();
    }
}