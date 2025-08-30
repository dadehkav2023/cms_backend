using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Utilities;
using Application.ViewModels.News.TextNews.Request;
using Application.ViewModels.News.TextNews.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.News.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.News.TextNews
{
    public class TextNewsService : ITextNewsService
    {
        private readonly IRepository<Domain.Entities.News.News> _newsRepository;
        private readonly IRepository<NewsCategory> _newsCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;


        public TextNewsService(IUnitOfWorkNews unitOfWorkNews, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _newsRepository = unitOfWorkNews.GetRepository<Domain.Entities.News.News>();
            _newsCategoryRepository = unitOfWorkNews.GetRepository<NewsCategory>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewNews(RequestNewTextNewsViewModel requestNewTextNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newNews =
                    _mapper.Map<Domain.Entities.News.News>(requestNewTextNewsViewModel);

                newNews.PublishedDateTime = !string.IsNullOrEmpty(requestNewTextNewsViewModel.PublishedDateTime)
                    ? requestNewTextNewsViewModel.PublishedDateTime.ToString().ConvertJalaliToMiladi()
                    : DateTime.Now;


                var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile> {requestNewTextNewsViewModel.ImagePath})
                    .FirstOrDefault();

                if (uploadedAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                newNews.ImagePath = uploadedAddress;

                var newsCategories = _newsCategoryRepository
                    .DeferredWhere(x => requestNewTextNewsViewModel.CategoriesId.Contains(x.Id)).ToList();

                newNews.NewsCategories = newsCategories;

                await _newsRepository.AddAsync(newNews);

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

        public async Task<IBusinessLogicResult<bool>> EditNews(
            RequestEditTextNewsViewModel requestEditTextNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news =
                    _newsRepository.DeferredWhere(n => n.Id == requestEditTextNewsViewModel.Id)
                        .Include(category => category.NewsCategories).FirstOrDefault();
                if (news == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                news.PublishedDateTime = !string.IsNullOrEmpty(requestEditTextNewsViewModel.PublishedDateTime)
                    ? requestEditTextNewsViewModel.PublishedDateTime.ConvertJalaliToMiladi()
                    : DateTime.Now;

                var oldImagePath = news.ImagePath;
                _mapper.Map(requestEditTextNewsViewModel, news);

                #region Upload

                if (requestEditTextNewsViewModel.ImagePath != null)
                {
                    var uploadedFileAddress = _fileUploaderService
                        .Upload(new List<IFormFile>() {requestEditTextNewsViewModel.ImagePath})
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
                    .DeferredWhere(x => requestEditTextNewsViewModel.CategoriesId.Contains(x.Id)).ToList();

                news.NewsCategories = newsCategories;

                await _newsRepository.UpdateAsync(news, true);

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

        public async Task<IBusinessLogicResult<ResponseGetTextNewsListViewModel>> GetNews(
            RequestGetTextNewsViewModel requestGetTextNewsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news = _newsRepository.DeferdSelectAll().OrderBy(x => x.PublishedDateTime).AsQueryable();
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.Title))
                    news = news.Where(x => x.Title.Contains(requestGetTextNewsViewModel.Title));
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.HeadTitle))
                    news = news.Where(x => x.HeadTitle.Contains(requestGetTextNewsViewModel.HeadTitle));
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.SubTitle))
                    news = news.Where(x => x.SubTitle.Contains(requestGetTextNewsViewModel.SubTitle));
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.SummaryTitle))
                    news = news.Where(x => x.SummaryTitle.Contains(requestGetTextNewsViewModel.SummaryTitle));
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.Lead))
                    news = news.Where(x => x.Lead.Contains(requestGetTextNewsViewModel.Lead));
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.Content))
                    news = news.Where(x => x.Content.Contains(requestGetTextNewsViewModel.Content));
                if (requestGetTextNewsViewModel.IsActive != null)
                    news = news.Where(x => x.IsActive == requestGetTextNewsViewModel.IsActive);
                if (requestGetTextNewsViewModel.NewsType != null)
                    news = news.Where(x => x.NewsType == requestGetTextNewsViewModel.NewsType);
                if (requestGetTextNewsViewModel.NewsPriority != null)
                    news = news.Where(x => x.NewsPriority == requestGetTextNewsViewModel.NewsPriority);
                if (requestGetTextNewsViewModel.LoadPublishedDate)
                    news = news.Where(n => n.PublishedDateTime <= DateTime.Now);
                if (requestGetTextNewsViewModel.Id is > 0)
                    news = news.Where(n => n.Id == requestGetTextNewsViewModel.Id);
                if (requestGetTextNewsViewModel.ShowInMainPage != null)
                    news = news.Where(n => n.ShowInMainPage == requestGetTextNewsViewModel.ShowInMainPage);
                
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.StartDateTime))
                    news = news.Where(s =>
                        s.PublishedDateTime >= requestGetTextNewsViewModel.StartDateTime.ConvertJalaliToMiladi());
                if (!string.IsNullOrEmpty(requestGetTextNewsViewModel.EndDateTime))
                    news = news.Where(s =>
                        s.PublishedDateTime <= requestGetTextNewsViewModel.EndDateTime.ConvertJalaliToMiladi());
                if(requestGetTextNewsViewModel.Priority != null)
                {
                    news = news.Where(x => x.Priority == requestGetTextNewsViewModel.Priority).OrderByDescending(x => x.Priority);  
                }
                if (requestGetTextNewsViewModel.CategoryIds != null)
                {
                    //todo remove include
                    news = news.Include(x => x.NewsCategories).Where(c =>
                        c.NewsCategories.Any(i => requestGetTextNewsViewModel.CategoryIds.Contains(i.Id)));
                }

                var newsList = news
                    .ProjectTo<ResponseGetTextNewsViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetTextNewsViewModel.Page - 1) * requestGetTextNewsViewModel.PageSize)
                    .Take(requestGetTextNewsViewModel.PageSize);

                var result = new ResponseGetTextNewsListViewModel
                {
                    Count = newsList.Count(),
                    CurrentPage = requestGetTextNewsViewModel.Page,
                    TotalCount = news.Count(),
                    NewsList = newsList.ToList()
                };


                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetTextNewsListViewModel>(succeeded: true, result: null,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetTextNewsListViewModel>(succeeded: false, result: null,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteNews(int newsId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news =
                    _newsRepository.FirstOrDefaultItemAsync(n => n.Id == newsId)
                        .Result;
                if (news == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                await _newsRepository.RemoveAsync(news, true);

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