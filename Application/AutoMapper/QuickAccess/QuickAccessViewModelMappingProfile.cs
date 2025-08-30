using Application.ViewModels.QuickAccess.Request;
using Application.ViewModels.QuickAccess.Response;
using AutoMapper;

namespace Application.AutoMapper.QuickAccess
{
    public class NewQuickAccessViewModelMappingProfile : Profile
    {
        public NewQuickAccessViewModelMappingProfile()
        {
            CreateMap<RequestNewQuickAccessViewModel, Domain.Entities.QuickAccess.QuickAccess>().ReverseMap();
            CreateMap<RequestEditQuickAccessViewModel, Domain.Entities.QuickAccess.QuickAccess>().ReverseMap();
            CreateMap<ResponseGetQuickAccessViewModel, Domain.Entities.QuickAccess.QuickAccess>().ReverseMap();
        }
    }
}