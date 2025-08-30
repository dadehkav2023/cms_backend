using Application.ViewModels.Accounting.User.Request;
using Application.ViewModels.Accounting.User.Response;
using AutoMapper;

namespace Application.AutoMapper.Identity.User
{
    public class UserViewModelMappingProfile : Profile
    {
        public UserViewModelMappingProfile()
        {
            CreateMap<ResponseGetUserViewModel, Domain.Entities.Identity.User.User>().ReverseMap();
            CreateMap<UserPotentialInViewModel, Domain.Entities.Identity.User.UserPotential>().ReverseMap();
        }
    }
}