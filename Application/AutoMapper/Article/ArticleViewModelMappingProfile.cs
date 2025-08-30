using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Attachment.Response;
using Application.ViewModels.Article.Request;
using Application.ViewModels.Article.Response;
using AutoMapper;
using Domain.Entities.Article;

namespace Application.AutoMapper.Article
{
    public class ArticleViewModelMappingProfile : Profile
    {
        public ArticleViewModelMappingProfile()
        {
            CreateMap<RequestNewArticleViewModel, Domain.Entities.Article.Article>().ReverseMap();
            CreateMap<RequestEditArticleViewModel, Domain.Entities.Article.Article>().ReverseMap();
            CreateMap<ResponseGetArticleViewModel, Domain.Entities.Article.Article>().ReverseMap();
            
            // //Attachment
            CreateMap<RequestNewArticleAttachmentViewModel, ArticleAttachment>().ReverseMap();
            CreateMap<RequestEditArticleAttachmentViewModel, ArticleAttachment>().ReverseMap();
            CreateMap<ResponseGetArticleAttachmentViewModel, ArticleAttachment>().ReverseMap();
        }
    }
}