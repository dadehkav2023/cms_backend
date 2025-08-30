using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Utilities;
using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Attachment.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Article;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Article.Article
{
    public class ArticleAttachmentService : IArticleAttachmentService
    {
        private readonly IRepository<ArticleAttachment> _articleAttachmentRepository;
        private readonly IRepository<Domain.Entities.Article.Article> _articleRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public ArticleAttachmentService(IUnitOfWorkArticle unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _articleAttachmentRepository = unitOfWork.GetRepository<ArticleAttachment>();
            _articleRepository = unitOfWork.GetRepository<Domain.Entities.Article.Article>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewArticleAttachment(
            RequestNewArticleAttachmentViewModel requestNewArticleAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var article =
                    _articleRepository.Any(x => x.Id == requestNewArticleAttachmentViewModel.ArticleId);
                if (!article)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.ArticleNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                {
                    requestNewArticleAttachmentViewModel.AttachmentFile
                }).FirstOrDefault();

                if (uploadAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newArticleAttachment =
                    _mapper.Map<ArticleAttachment>(requestNewArticleAttachmentViewModel);
                newArticleAttachment.AttachmentFile = uploadAddress;
                newArticleAttachment.FileType =
                    requestNewArticleAttachmentViewModel.AttachmentFile.GetFileExtension();

                await _articleAttachmentRepository.AddAsync(newArticleAttachment);

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

        public async Task<IBusinessLogicResult<bool>> EditArticleAttachment(
            RequestEditArticleAttachmentViewModel requestEditArticleAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var articleAttachment =
                    _articleAttachmentRepository
                        .FirstOrDefaultItemAsync(x => x.Id == requestEditArticleAttachmentViewModel.Id).Result;
                if (articleAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldFilePath = articleAttachment.AttachmentFile;
                _mapper.Map(requestEditArticleAttachmentViewModel, articleAttachment);

                if (requestEditArticleAttachmentViewModel.AttachmentFile != null)
                {
                    var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                        {requestEditArticleAttachmentViewModel.AttachmentFile}).FirstOrDefault();
                    if (uploadAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    articleAttachment.AttachmentFile = uploadAddress;
                    articleAttachment.FileType =
                        requestEditArticleAttachmentViewModel.AttachmentFile.GetFileExtension();
                }
                else
                {
                    articleAttachment.AttachmentFile = oldFilePath;
                }

                await _articleAttachmentRepository.UpdateAsync(articleAttachment, true);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                    message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<ResponseGetArticleAttachmentListViewModel>> GetArticleAttachmentList(
            RequestGetArticleAttachmentViewModel requestGetArticleAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var articleAttachment = _articleAttachmentRepository.DeferredWhere(x =>
                    x.ArticleId == requestGetArticleAttachmentViewModel.ArticleId);

                if (!string.IsNullOrEmpty(requestGetArticleAttachmentViewModel.Title))
                    articleAttachment = articleAttachment.Where(x =>
                        x.Title.Contains(requestGetArticleAttachmentViewModel.Title));

                var result = new ResponseGetArticleAttachmentListViewModel
                {
                    ArticleAttachmentList = articleAttachment
                        .ProjectTo<ResponseGetArticleAttachmentViewModel>(_mapper.ConfigurationProvider).ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetArticleAttachmentListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetArticleAttachmentListViewModel>(succeeded: false,
                    result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteArticleAttachment(int articleAttachmentId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var articleAttachment = _articleAttachmentRepository.FirstOrDefaultItemAsync(s => s.Id == articleAttachmentId).Result;
                if (articleAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _articleAttachmentRepository.RemoveAsync(articleAttachment, true);

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