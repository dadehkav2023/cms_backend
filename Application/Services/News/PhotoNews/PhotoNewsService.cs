using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Utilities;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.PhotoNews.Response;
using Application.ViewModels.News.TextNews.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.News.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.News.PhotoNews
{
    public class PhotoNewsService : IPhotoNewsService
    {
        private readonly IRepository<Domain.Entities.News.PhotoNews.PhotoNews> _photoNewsRepository;
        private readonly IRepository<NewsCategory> _newsCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public PhotoNewsService(IUnitOfWorkNews unitOfWorkNews, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _photoNewsRepository = unitOfWorkNews.GetRepository<Domain.Entities.News.PhotoNews.PhotoNews>();
            _newsCategoryRepository = unitOfWorkNews.GetRepository<NewsCategory>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewPhotoNews(
            RequestNewPhotoNewsViewModel requestNewPhotoNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newPhotoNews = _mapper.Map<Domain.Entities.News.PhotoNews.PhotoNews>(requestNewPhotoNewsViewModel);

                #region Upload

                var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile> {requestNewPhotoNewsViewModel.ImagePath})
                    .FirstOrDefault();
                if (uploadedAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                newPhotoNews.ImagePath = uploadedAddress;

                #endregion

                var newsCategories = _newsCategoryRepository
                    .DeferredWhere(x => requestNewPhotoNewsViewModel.CategoriesId.Contains(x.Id)).ToList();

                newPhotoNews.NewsCategories = newsCategories;
                newPhotoNews.PublishedDateTime = !string.IsNullOrEmpty(requestNewPhotoNewsViewModel.PublishedDateTime)
                    ? requestNewPhotoNewsViewModel.PublishedDateTime.ConvertJalaliToMiladi()
                    : DateTime.Now;

                await _photoNewsRepository.AddAsync(newPhotoNews);

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

        public async Task<IBusinessLogicResult<bool>> EditPhotoNews(
            RequestEditPhotoNewsViewModel requestEditPhotoNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news =
                    _photoNewsRepository.DeferredWhere(n => n.Id == requestEditPhotoNewsViewModel.Id)
                        .Include(c => c.NewsCategories).FirstOrDefault();
                if (news == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldImagePath = news.ImagePath;
                _mapper.Map(requestEditPhotoNewsViewModel, news);

                #region Upload

                if (requestEditPhotoNewsViewModel.ImagePath != null)
                {
                    var uploadedFileAddress = _fileUploaderService
                        .Upload(new List<IFormFile> {requestEditPhotoNewsViewModel.ImagePath})
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
                    .DeferredWhere(x => requestEditPhotoNewsViewModel.CategoriesId.Contains(x.Id)).ToList();

                news.NewsCategories = newsCategories;
                news.PublishedDateTime = !string.IsNullOrEmpty(requestEditPhotoNewsViewModel.PublishedDateTime)
                    ? requestEditPhotoNewsViewModel.PublishedDateTime.ConvertJalaliToMiladi()
                    : DateTime.Now;

                await _photoNewsRepository.UpdateAsync(news, true);

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

        public async Task<IBusinessLogicResult<ResponseGetPhotoNewsListViewModel>> GetPhotoNews(
            RequestGetPhotoNewsViewModel requestGetPhotoNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news = _photoNewsRepository.DeferdSelectAll().OrderBy(x=>x.PublishedDateTime).AsQueryable();

                if (!string.IsNullOrEmpty(requestGetPhotoNewsViewModel.Title))
                    news = news.Where(x => x.Title.Contains(requestGetPhotoNewsViewModel.Title));
                if (!string.IsNullOrEmpty(requestGetPhotoNewsViewModel.Summary))
                    news = news.Where(x => x.Summary.Contains(requestGetPhotoNewsViewModel.Summary));
                if (requestGetPhotoNewsViewModel.LoadPublishedNews)
                    news = news.Where(n => n.PublishedDateTime <= DateTime.Now);
                if (requestGetPhotoNewsViewModel.Id is > 0)
                    news = news.Where(n => n.Id == requestGetPhotoNewsViewModel.Id);
                if (requestGetPhotoNewsViewModel.IsActive != null)
                    news = news.Where(n => n.IsActive == requestGetPhotoNewsViewModel.IsActive);
                if (requestGetPhotoNewsViewModel.ShowInMainPage != null)
                    news = news.Where(n => n.ShowInMainPage == requestGetPhotoNewsViewModel.ShowInMainPage);
                
                if (requestGetPhotoNewsViewModel.CategoryIds != null)
                {
                    //todo remove include
                    news = news.Include(x => x.NewsCategories).Where(c =>
                        c.NewsCategories.Any(i => requestGetPhotoNewsViewModel.CategoryIds.Contains(i.Id)));
                }
                if(requestGetPhotoNewsViewModel.Priority != null)
                {
                    news = news.Where(n => n.Priority == requestGetPhotoNewsViewModel.Priority).OrderByDescending(x => x.Priority);
                }
                if (!string.IsNullOrEmpty(requestGetPhotoNewsViewModel.StartDateTime))
                    news = news.Where(s =>
                        s.PublishedDateTime >= requestGetPhotoNewsViewModel.StartDateTime.ConvertJalaliToMiladi());
                if (!string.IsNullOrEmpty(requestGetPhotoNewsViewModel.EndDateTime))
                    news = news.Where(s =>
                        s.PublishedDateTime <= requestGetPhotoNewsViewModel.EndDateTime.ConvertJalaliToMiladi());
                
                var newsList = news
                    .ProjectTo<ResponseGetPhotoNewsViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetPhotoNewsViewModel.Page - 1) * requestGetPhotoNewsViewModel.PageSize)
                    .Take(requestGetPhotoNewsViewModel.PageSize);

                var result = new ResponseGetPhotoNewsListViewModel
                {
                    Count = newsList.Count(),
                    CurrentPage = requestGetPhotoNewsViewModel.Page,
                    TotalCount = news.Count(),
                    NewsList = newsList.ToList()
                };


                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetPhotoNewsListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetPhotoNewsListViewModel>(succeeded: false, result: null,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeletePhotoNews(int newsId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news =
                    _photoNewsRepository.FirstOrDefaultItemAsync(n => n.Id == newsId)
                        .Result;
                if (news == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                await _photoNewsRepository.RemoveAsync(news, true);

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