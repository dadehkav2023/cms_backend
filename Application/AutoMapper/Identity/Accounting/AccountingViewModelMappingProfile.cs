using Application.ViewModels.Accounting.Request;
using AutoMapper;

namespace Application.AutoMapper.Identity.Accounting
{
    public class AccountingViewModelMappingProfile : Profile
    {
        public AccountingViewModelMappingProfile()
        {
            CreateMap<RequestRegisterViewModel, Domain.Entities.Identity.User.User>().ReverseMap();
        }
    }
}