using Application.ViewModels.News.Category.Request;
using Application.ViewModels.News.Category.Response;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.PhotoNews.Attachment.Response;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.PhotoNews.Response;
using Application.ViewModels.News.Request;
using Application.ViewModels.News.TextNews.Attachment.Request;
using Application.ViewModels.News.TextNews.Attachment.Response;
using Application.ViewModels.News.TextNews.Request;
using Application.ViewModels.News.TextNews.Response;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Attachment.Response;
using Application.ViewModels.News.VideoNews.Request;
using Application.ViewModels.News.VideoNews.Response;
using AutoMapper;
using Domain.Entities.News;
using Domain.Entities.News.Category;
using Domain.Entities.News.PhotoNews;
using Domain.Entities.News.VideoNews;

namespace Application.AutoMapper.News
{
    public class NewsViewModelMappingProfile : Profile
    {
        public NewsViewModelMappingProfile()
        {
            #region TextNews

            CreateMap<RequestNewTextNewsViewModel, Domain.Entities.News.News>().ReverseMap();
            CreateMap<RequestEditTextNewsViewModel, Domain.Entities.News.News>().ReverseMap();
            CreateMap<ResponseGetTextNewsViewModel, Domain.Entities.News.News>().ReverseMap();

            #region Attachment
            CreateMap<RequestTextNewNewsAttachmentViewModel, NewsAttachment>().ReverseMap();
            CreateMap<RequestEditTextNewsAttachmentViewModel, NewsAttachment>().ReverseMap();
            CreateMap<ResponseGetTextNewsAttachmentViewModel, NewsAttachment>().ReverseMap();
            #endregion
            
            #endregion

            #region Category
            CreateMap<RequestNewNewsCategoryViewModel, NewsCategory>().ReverseMap();
            CreateMap<RequestEditNewsCategoryViewModel, NewsCategory>().ReverseMap();
            CreateMap<ResponseGetNewsCategoryViewModel, NewsCategory>().ReverseMap();
            #endregion

            #region PhotoNews

            CreateMap<RequestNewPhotoNewsViewModel, PhotoNews>().ReverseMap();
            CreateMap<RequestEditPhotoNewsViewModel, PhotoNews>().ReverseMap();
            CreateMap<ResponseGetPhotoNewsViewModel, PhotoNews>().ReverseMap();

            #region Attachment
            CreateMap<RequestNewPhotoNewsAttachmentViewModel, PhotoNewsAttachment>().ReverseMap();
            CreateMap<RequestEditPhotoNewsAttachmentViewModel, PhotoNewsAttachment>().ReverseMap();
            CreateMap<ResponseGetPhotoNewsAttachmentViewModel, PhotoNewsAttachment>().ReverseMap();
            #endregion
            
            #endregion
            
            #region VideoNews

                CreateMap<RequestNewVideoNewsViewModel, VideoNews>().ReverseMap();
                CreateMap<RequestEditVideoNewsViewModel, VideoNews>().ReverseMap();
                CreateMap<ResponseGetVideoNewsViewModel, VideoNews>().ReverseMap();

                #region Attachment
                CreateMap<RequestNewVideoNewsAttachmentViewModel, VideoNewsAttachment>().ReverseMap();
                CreateMap<RequestEditVideoNewsAttachmentViewModel, VideoNewsAttachment>().ReverseMap();
                CreateMap<ResponseGetVideoNewsAttachmentViewModel, VideoNewsAttachment>().ReverseMap();
                #endregion
            
            #endregion

            CreateMap<RequestNewNewsViewModel, RequestNewPhotoNewsViewModel>().ReverseMap();
            CreateMap<RequestNewNewsViewModel, RequestNewTextNewsViewModel>().ReverseMap();
            CreateMap<RequestNewNewsViewModel, RequestNewVideoNewsViewModel>().ReverseMap();
            
            CreateMap<RequestEditNewsViewModel, RequestEditTextNewsViewModel>().ReverseMap();
            CreateMap<RequestEditNewsViewModel, RequestEditPhotoNewsViewModel>().ReverseMap();  
            CreateMap<RequestEditNewsViewModel, RequestEditVideoNewsViewModel>().ReverseMap();  
        }
    }
}