using Application.ViewModels.AboutUs.Request;
using Application.ViewModels.AboutUs.Response;
using AutoMapper;

namespace Application.AutoMapper.AboutUs
{
    public class AboutUsViewModelMappingProfile : Profile
    {
        public AboutUsViewModelMappingProfile()
        {
            CreateMap<RequestSetAboutUsViewModel, Domain.Entities.AboutUs.AboutUs>().ReverseMap();
            CreateMap<ResponseGetAboutUs, Domain.Entities.AboutUs.AboutUs>().ReverseMap();
        }
        
    }

}