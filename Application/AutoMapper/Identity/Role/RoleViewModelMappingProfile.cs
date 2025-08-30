using Application.ViewModels.Accounting.Role.Response;
using Application.ViewModels.Accounting.User.Response;
using AutoMapper;

namespace Application.AutoMapper.Identity.Role
{
    public class RoleViewModelMappingProfile : Profile
    {
        public RoleViewModelMappingProfile()
        {
            CreateMap<ResponseRoleViewModel, Domain.Entities.Identity.Role.Role>().ReverseMap();
        }
    }
}