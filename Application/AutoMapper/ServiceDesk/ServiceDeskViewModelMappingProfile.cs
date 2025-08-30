using Application.ViewModels.ServiceDesk.Request;
using Application.ViewModels.ServiceDesk.Response;
using AutoMapper;

namespace Application.AutoMapper.ServiceDesk
{
    public class ServiceDeskViewModelMappingProfile : Profile
    {
        public ServiceDeskViewModelMappingProfile()
        {
            CreateMap<RequestNewServiceDeskViewModel, Domain.Entities.ServiceDesk.ServiceDesk>().ReverseMap();
            CreateMap<RequestEditServiceDeskViewModel, Domain.Entities.ServiceDesk.ServiceDesk>().ReverseMap();
            CreateMap<ResponseGetServiceDeskViewModel, Domain.Entities.ServiceDesk.ServiceDesk>().ReverseMap();
        }
    }
}