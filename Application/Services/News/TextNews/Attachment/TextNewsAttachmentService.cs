using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Services.News.TextNews.Attachment;
using Application.Utilities;
using Application.ViewModels.News.TextNews.Attachment.Request;
using Application.ViewModels.News.TextNews.Attachment.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.News;
using Microsoft.AspNetCore.Http;

namespace Application.Services.News.Attachment
{
    public class TextNewsAttachmentService : ITextNewsAttachmentService
    {
        private readonly IRepository<NewsAttachment> _newsAttachmentRepository;
        private readonly IRepository<Domain.Entities.News.News> _newsRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public TextNewsAttachmentService(IUnitOfWorkNews unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _newsAttachmentRepository = unitOfWork.GetRepository<NewsAttachment>();
            _newsRepository = unitOfWork.GetRepository<Domain.Entities.News.News>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewNewsAttachment(
            RequestTextNewNewsAttachmentViewModel requestTextNewNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news = _newsRepository.Any(x => x.Id == requestTextNewNewsAttachmentViewModel.NewsId);

                if (!news)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                {
                    requestTextNewNewsAttachmentViewModel.AttachmentFile
                }).FirstOrDefault();

                if (uploadAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newNewsAttachment =
                    _mapper.Map<NewsAttachment>(requestTextNewNewsAttachmentViewModel);
                newNewsAttachment.AttachmentFile = uploadAddress;
                newNewsAttachment.FileType =
                    requestTextNewNewsAttachmentViewModel.AttachmentFile.GetFileExtension();

                await _newsAttachmentRepository.AddAsync(newNewsAttachment);
                
                
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

        public async Task<IBusinessLogicResult<bool>> EditNewsAttachment(
            RequestEditTextNewsAttachmentViewModel requestEditTextNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsAttachment =
                    _newsAttachmentRepository
                        .FirstOrDefaultItemAsync(x => x.Id == requestEditTextNewsAttachmentViewModel.Id).Result;
                if (newsAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldFilePath = newsAttachment.AttachmentFile;
                _mapper.Map(requestEditTextNewsAttachmentViewModel, newsAttachment);

                if (requestEditTextNewsAttachmentViewModel.AttachmentFile != null)
                {
                    var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                        {requestEditTextNewsAttachmentViewModel.AttachmentFile}).FirstOrDefault();
                    if (uploadAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    newsAttachment.AttachmentFile = uploadAddress;
                    newsAttachment.FileType =
                        requestEditTextNewsAttachmentViewModel.AttachmentFile.GetFileExtension();
                }
                else
                {
                    newsAttachment.AttachmentFile = oldFilePath;
                }

                await _newsAttachmentRepository.UpdateAsync(newsAttachment, true);

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

        public async Task<IBusinessLogicResult<ResponseGetTextNewsAttachmentListViewModel>> GetNewsAttachmentList(
            RequestTextGetNewsAttachmentViewModel requestTextGetNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsAttachment = _newsAttachmentRepository.DeferredWhere(x =>
                    x.NewsId == requestTextGetNewsAttachmentViewModel.NewsId);

                if (!string.IsNullOrEmpty(requestTextGetNewsAttachmentViewModel.Title))
                    newsAttachment = newsAttachment.Where(x =>
                        x.Title.Contains(requestTextGetNewsAttachmentViewModel.Title));

                var result = new ResponseGetTextNewsAttachmentListViewModel
                {
                    NewsAttachmentList = newsAttachment
                        .ProjectTo<ResponseGetTextNewsAttachmentViewModel>(_mapper.ConfigurationProvider).ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetTextNewsAttachmentListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetTextNewsAttachmentListViewModel>(succeeded: false,
                    result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteNewsAttachment(int newsAttachmentId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsAttachment = _newsAttachmentRepository.FirstOrDefaultItemAsync(s => s.Id == newsAttachmentId).Result;
                if (newsAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _newsAttachmentRepository.RemoveAsync(newsAttachment, true);

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