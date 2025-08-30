using Application.ViewModels.Rules.Attachment.Request;
using Application.ViewModels.Rules.Attachment.Response;
using Application.ViewModels.Rules.Request;
using Application.ViewModels.Rules.Response;
using AutoMapper;
using Domain.Entities.Rules;

namespace Application.AutoMapper.Rules
{
    public class RulesViewModelMappingProfile : Profile
    {
        public RulesViewModelMappingProfile()
        {
            CreateMap<ResponseGetRulesViewModel, Domain.Entities.Rules.Rules>().ReverseMap();
            CreateMap<RequestSetRulesViewModel, Domain.Entities.Rules.Rules>().ReverseMap();
            
            //Attachment
            CreateMap<RequestNewRulesAttachmentViewModel, RulesAttachment>().ReverseMap();
            CreateMap<RequestEditRulesAttachmentViewModel, RulesAttachment>().ReverseMap();
            CreateMap<ResponseGetRulesAttachmentViewModel, RulesAttachment>().ReverseMap();
        }
    }
}