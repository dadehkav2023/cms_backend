using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Services.News.PhotoNews;
using Application.Services.News.TextNews;
using Application.Services.News.VideoNews;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.PhotoNews.Response;
using Application.ViewModels.News.Request;
using Application.ViewModels.News.TextNews.Request;
using Application.ViewModels.News.TextNews.Response;
using Application.ViewModels.News.VideoNews.Request;
using AutoMapper;
using Common.Enum;

namespace Application.Services.News
{
    public class NewsService : INewsService
    {
        private readonly ITextNewsService _textNewsService;
        private readonly IPhotoNewsService _photoNewsService;
        private readonly IVideoNewsService _videoNewsService;
        private readonly IMapper _mapper;

        public NewsService(IMapper mapper, ITextNewsService textNewsService, IPhotoNewsService photoNewsService,
            IVideoNewsService videoNewsService)
        {
            _textNewsService = textNewsService;
            _photoNewsService = photoNewsService;
            _videoNewsService = videoNewsService;
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> NewNews(RequestNewNewsViewModel requestNewNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                if (requestNewNewsViewModel.NewsContentType == NewsContentTypeEnum.Text)
                {
                    var newsViewModel = _mapper.Map<RequestNewTextNewsViewModel>(requestNewNewsViewModel);
                    return _textNewsService.NewNews(newsViewModel).Result;
                }

                if (requestNewNewsViewModel.NewsContentType == NewsContentTypeEnum.Photo)
                {
                    var photoNewsViewModel = _mapper.Map<RequestNewPhotoNewsViewModel>(requestNewNewsViewModel);
                    return _photoNewsService.NewPhotoNews(photoNewsViewModel).Result;
                }

                if (requestNewNewsViewModel.NewsContentType == NewsContentTypeEnum.Video)
                {
                    var videoNewsViewModel = _mapper.Map<RequestNewVideoNewsViewModel>(requestNewNewsViewModel);
                    return _videoNewsService.NewVideoNews(videoNewsViewModel).Result;
                }

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> EditNews(RequestEditNewsViewModel requestEditNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                if (requestEditNewsViewModel.NewsContentType == NewsContentTypeEnum.Text)
                {
                    var newsViewModel = _mapper.Map<RequestEditTextNewsViewModel>(requestEditNewsViewModel);
                    return _textNewsService.EditNews(newsViewModel).Result;
                }

                if (requestEditNewsViewModel.NewsContentType == NewsContentTypeEnum.Photo)
                {
                    var photoNewsViewModel = _mapper.Map<RequestEditPhotoNewsViewModel>(requestEditNewsViewModel);
                    return _photoNewsService.EditPhotoNews(photoNewsViewModel).Result;
                }
                
                if (requestEditNewsViewModel.NewsContentType == NewsContentTypeEnum.Video)
                {
                    var videoNewsViewModel = _mapper.Map<RequestEditVideoNewsViewModel>(requestEditNewsViewModel);
                    return _videoNewsService.EditVideoNews(videoNewsViewModel).Result;
                }

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteNews(RequestDeleteNewsViewModel requestDeleteNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                if (requestDeleteNewsViewModel.NewsContentType == NewsContentTypeEnum.Text)
                    return _textNewsService.DeleteNews(requestDeleteNewsViewModel.NewsId).Result;

                if (requestDeleteNewsViewModel.NewsContentType == NewsContentTypeEnum.Photo)
                    return _photoNewsService.DeletePhotoNews(requestDeleteNewsViewModel.NewsId).Result;

                if (requestDeleteNewsViewModel.NewsContentType == NewsContentTypeEnum.Video)
                    return _videoNewsService.DeleteVideoNews(requestDeleteNewsViewModel.NewsId).Result;


                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }
    }
}