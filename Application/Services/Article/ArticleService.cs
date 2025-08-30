using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Article.Request;
using Application.ViewModels.Article.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Domain.Entities.Article.Article> _articleRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;


        public ArticleService(IUnitOfWorkArticle unitOfWorkArticle, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _articleRepository = unitOfWorkArticle.GetRepository<Domain.Entities.Article.Article>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewArticle(
            RequestNewArticleViewModel requestNewArticleViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newArticle =
                    _mapper.Map<Domain.Entities.Article.Article>(requestNewArticleViewModel);


                #region upload

                if (requestNewArticleViewModel.ImagePath != null)
                {
                    var uploadedAddress = _fileUploaderService
                        .Upload(new List<IFormFile> {requestNewArticleViewModel.ImagePath})
                        .FirstOrDefault();
                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    newArticle.ImagePath = uploadedAddress;
                }

                #endregion

                await _articleRepository.AddAsync(newArticle);

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

        public async Task<IBusinessLogicResult<bool>> EditArticle(
            RequestEditArticleViewModel requestEditArticleViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var article =
                    _articleRepository.FirstOrDefaultItemAsync(n => n.Id == requestEditArticleViewModel.Id)
                        .Result;

                if (article == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.ArticleNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                #region upload

                var imagePath = article.ImagePath;
                if (requestEditArticleViewModel.ImagePath != null)
                {
                    var uploadedAddress = _fileUploaderService
                        .Upload(new List<IFormFile> {requestEditArticleViewModel.ImagePath})
                        .FirstOrDefault();
                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    imagePath = uploadedAddress;
                }

                #endregion

                _mapper.Map(requestEditArticleViewModel, article);
                article.ImagePath = imagePath;

                await _articleRepository.UpdateAsync(article, true);

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

        public async Task<IBusinessLogicResult<ResponseGetArticleListViewModel>> GetArticleList(
            RequestGetArticleViewModel requestGetArticleViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var article = _articleRepository.DeferdSelectAll();
                if (requestGetArticleViewModel.Id is > 0)
                    article = article.Where(s => s.Id == requestGetArticleViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetArticleViewModel.Title))
                    article = article.Where(s => s.Title.Contains(requestGetArticleViewModel.Title));
                if (requestGetArticleViewModel.IsActive != null)
                    article = article.Where(x => x.IsActive == requestGetArticleViewModel.IsActive);
                if (requestGetArticleViewModel.PublishYear != null)
                    article = article.Where(x => x.PublishYear == requestGetArticleViewModel.PublishYear);

                if (article == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.ArticleNotFound));
                    return new BusinessLogicResult<ResponseGetArticleListViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var articleList = article
                    .ProjectTo<ResponseGetArticleViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetArticleViewModel.Page - 1) * requestGetArticleViewModel.PageSize)
                    .Take(requestGetArticleViewModel.PageSize);

                var result = new ResponseGetArticleListViewModel()
                {
                    Count = articleList.Count(),
                    CurrentPage = requestGetArticleViewModel.Page,
                    TotalCount = article.Count(),
                    ArticleList = articleList.ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetArticleListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetArticleListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteArticle(int articleId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var article = _articleRepository.FirstOrDefaultItemAsync(s => s.Id == articleId).Result;
                if (article == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.ArticleNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _articleRepository.RemoveAsync(article, true);

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