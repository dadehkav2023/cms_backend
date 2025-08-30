using Application.ViewModels.RelatedLink.Request;
using Application.ViewModels.RelatedLink.Response;
using AutoMapper;

namespace Application.AutoMapper.RelatedLink
{
    public class NewRelatedLinkViewModelMappingProfile : Profile
    {
        public NewRelatedLinkViewModelMappingProfile()
        {
            CreateMap<RequestNewRelatedLinkViewModel, Domain.Entities.RelatedLink.RelatedLink>().ReverseMap();
            CreateMap<RequestEditRelatedLinkViewModel, Domain.Entities.RelatedLink.RelatedLink>().ReverseMap();
            CreateMap<ResponseGetRelatedLinkViewModel, Domain.Entities.RelatedLink.RelatedLink>().ReverseMap();
        }
    }
}