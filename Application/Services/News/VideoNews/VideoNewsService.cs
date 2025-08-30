using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Utilities;
using Application.ViewModels.News.VideoNews.Request;
using Application.ViewModels.News.VideoNews.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.News.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.News.VideoNews
{
    public class VideoNewsService : IVideoNewsService
    {
        private readonly IRepository<Domain.Entities.News.VideoNews.VideoNews> _videoNewsRepository;
        private readonly IRepository<NewsCategory> _newsCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public VideoNewsService(IUnitOfWorkNews unitOfWorkNews, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _videoNewsRepository = unitOfWorkNews.GetRepository<Domain.Entities.News.VideoNews.VideoNews>();
            _newsCategoryRepository = unitOfWorkNews.GetRepository<NewsCategory>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewVideoNews(
            RequestNewVideoNewsViewModel requestNewVideoNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newVideoNews = _mapper.Map<Domain.Entities.News.VideoNews.VideoNews>(requestNewVideoNewsViewModel);

                #region Upload

                var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile> {requestNewVideoNewsViewModel.ImagePath})
                    .FirstOrDefault();
                if (uploadedAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                newVideoNews.ImagePath = uploadedAddress;

                #endregion

                var newsCategories = _newsCategoryRepository
                    .DeferredWhere(x => requestNewVideoNewsViewModel.CategoriesId.Contains(x.Id)).ToList();

                newVideoNews.NewsCategories = newsCategories;
                newVideoNews.PublishedDateTime = !string.IsNullOrEmpty(requestNewVideoNewsViewModel.PublishedDateTime)
                    ? requestNewVideoNewsViewModel.PublishedDateTime.ConvertJalaliToMiladi()
                    : DateTime.Now;

                await _videoNewsRepository.AddAsync(newVideoNews);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> EditVideoNews(
            RequestEditVideoNewsViewModel requestEditVideoNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news =
                    _videoNewsRepository.DeferredWhere(n => n.Id == requestEditVideoNewsViewModel.Id)
                        .Include(c => c.NewsCategories).FirstOrDefault();
                if (news == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldImagePath = news.ImagePath;
                _mapper.Map(requestEditVideoNewsViewModel, news);

                #region Upload

                if (requestEditVideoNewsViewModel.ImagePath != null)
                {
                    var uploadedFileAddress = _fileUploaderService
                        .Upload(new List<IFormFile> {requestEditVideoNewsViewModel.ImagePath})
                        .FirstOrDefault();

                    if (uploadedFileAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    news.ImagePath = uploadedFileAddress;
                }
                else
                {
                    news.ImagePath = oldImagePath;
                }

                #endregion

                var newsCategories = _newsCategoryRepository
                    .DeferredWhere(x => requestEditVideoNewsViewModel.CategoriesId.Contains(x.Id)).ToList();

                news.NewsCategories = newsCategories;
                news.PublishedDateTime = !string.IsNullOrEmpty(requestEditVideoNewsViewModel.PublishedDateTime)
                    ? requestEditVideoNewsViewModel.PublishedDateTime.ToString().ConvertJalaliToMiladi()
                    : DateTime.Now;

                await _videoNewsRepository.UpdateAsync(news, true);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<ResponseGetVideoNewsListViewModel>> GetVideoNews(
            RequestGetVideoNewsViewModel requestGetVideoNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news = _videoNewsRepository.DeferdSelectAll().OrderBy(x => x.PublishedDateTime).AsQueryable();
                if (!string.IsNullOrEmpty(requestGetVideoNewsViewModel.Title))
                    news = news.Where(x => x.Title.Contains(requestGetVideoNewsViewModel.Title));
                if (!string.IsNullOrEmpty(requestGetVideoNewsViewModel.Summary))
                    news = news.Where(x => x.Summary.Contains(requestGetVideoNewsViewModel.Summary));
                if (requestGetVideoNewsViewModel.LoadPublishedNews)
                    news = news.Where(n => n.PublishedDateTime <= DateTime.Now);
                if (requestGetVideoNewsViewModel.Id is > 0)
                    news = news.Where(n => n.Id == requestGetVideoNewsViewModel.Id);
                if (requestGetVideoNewsViewModel.IsActive != null)
                    news = news.Where(n => n.IsActive == requestGetVideoNewsViewModel.IsActive);
                if (!string.IsNullOrEmpty(requestGetVideoNewsViewModel.StartDateTime))
                    news = news.Where(s =>
                        s.PublishedDateTime >= requestGetVideoNewsViewModel.StartDateTime.ConvertJalaliToMiladi());
                if (!string.IsNullOrEmpty(requestGetVideoNewsViewModel.EndDateTime))
                    news = news.Where(s =>
                        s.PublishedDateTime <= requestGetVideoNewsViewModel.EndDateTime.ConvertJalaliToMiladi());
                if (requestGetVideoNewsViewModel.ShowInMainPage != null)
                    news = news.Where(n => n.ShowInMainPage == requestGetVideoNewsViewModel.ShowInMainPage);
                
                if(requestGetVideoNewsViewModel.Priority != null)
                {
                    news = news.Where(x=>x.Priority == requestGetVideoNewsViewModel.Priority).OrderByDescending(x => x.Priority);
                }
                if (requestGetVideoNewsViewModel.CategoryIds != null)
                {
                    //todo remove include
                    news = news.Include(x => x.NewsCategories).Where(c =>
                        c.NewsCategories.Any(i => requestGetVideoNewsViewModel.CategoryIds.Contains(i.Id)));
                }
                
                var newsList = news
                    .ProjectTo<ResponseGetVideoNewsViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetVideoNewsViewModel.Page - 1) * requestGetVideoNewsViewModel.PageSize)
                    .Take(requestGetVideoNewsViewModel.PageSize);

                var result = new ResponseGetVideoNewsListViewModel
                {
                    Count = newsList.Count(),
                    CurrentPage = requestGetVideoNewsViewModel.Page,
                    TotalCount = news.Count(),
                    NewsList = newsList.ToList()
                };


                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetVideoNewsListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetVideoNewsListViewModel>(succeeded: false, result: null,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteVideoNews(int newsId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news =
                    _videoNewsRepository.FirstOrDefaultItemAsync(n => n.Id == newsId)
                        .Result;
                if (news == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                await _videoNewsRepository.RemoveAsync(news, true);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
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