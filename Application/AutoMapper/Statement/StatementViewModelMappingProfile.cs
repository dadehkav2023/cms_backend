using Application.ViewModels.Statement.Attachment.Request;
using Application.ViewModels.Statement.Attachment.Response;
using Application.ViewModels.Statement.Category.Request;
using Application.ViewModels.Statement.Category.Response;
using Application.ViewModels.Statement.Request;
using Application.ViewModels.Statement.Response;
using AutoMapper;
using Domain.Entities.Statement;

namespace Application.AutoMapper.Statement
{
    public class StatementViewModelMappingProfile : Profile
    {
        public StatementViewModelMappingProfile()
        {
            CreateMap<RequestNewStatementViewModel, Domain.Entities.Statement.Statement>().ReverseMap();
            CreateMap<RequestEditStatementViewModel, Domain.Entities.Statement.Statement>().ReverseMap();
            CreateMap<ResponseGetStatementViewModel, Domain.Entities.Statement.Statement>().ReverseMap();
            
            //Attachment
            CreateMap<RequestNewStatementAttachmentViewModel, StatementAttachment>().ReverseMap();
            CreateMap<RequestEditStatementAttachmentViewModel, StatementAttachment>().ReverseMap();
            CreateMap<ResponseGetStatementAttachmentViewModel, StatementAttachment>().ReverseMap();
            
            
            //Category
            CreateMap<RequestNewStatementCategoryViewModel, StatementCategory>().ReverseMap();
            CreateMap<RequestEditStatementCategoryViewModel, StatementCategory>().ReverseMap();
            CreateMap<ResponseGetStatementCategoryViewModel, StatementCategory>().ReverseMap();
        }
    }
}