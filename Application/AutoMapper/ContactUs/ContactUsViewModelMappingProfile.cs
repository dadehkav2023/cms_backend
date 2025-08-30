using Application.ViewModels.AboutUs.Request;
using Application.ViewModels.AboutUs.Response;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Request;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Response;
using Application.ViewModels.ContactUs.ContactUs.Request;
using Application.ViewModels.ContactUs.ContactUs.Response;
using AutoMapper;
using Domain.Entities.ContactUs;

namespace Application.AutoMapper.ContactUs
{
    public class ContactUsViewModelMappingProfile : Profile
    {
        public ContactUsViewModelMappingProfile()
        {
            CreateMap<RequestSetContactUsViewModel, Domain.Entities.ContactUs.ContactUs>().ReverseMap();
            CreateMap<ResponseGetContactUs, Domain.Entities.ContactUs.ContactUs>().ReverseMap();
            
            
            CreateMap<RequestNewContactUsMessageViewModel, ContactUsMessage>().ReverseMap();
            CreateMap<ResponseGetContactUsMessageViewModel, ContactUsMessage>().ReverseMap();
        }
        
    }

}