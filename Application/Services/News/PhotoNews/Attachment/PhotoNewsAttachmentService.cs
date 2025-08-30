using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.PhotoNews.Attachment.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.News.PhotoNews;
using Microsoft.AspNetCore.Http;

namespace Application.Services.News.PhotoNews.Attachment
{
    public class PhotoNewsAttachmentService : PhotoNews.Attachment.IPhotoNewsAttachmentService
    {
        private readonly IRepository<PhotoNewsAttachment> _newsAttachmentRepository;
        private readonly IRepository<Domain.Entities.News.PhotoNews.PhotoNews> _newsRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public PhotoNewsAttachmentService(IUnitOfWorkNews unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _newsAttachmentRepository = unitOfWork.GetRepository<PhotoNewsAttachment>();
            _newsRepository = unitOfWork.GetRepository<Domain.Entities.News.PhotoNews.PhotoNews>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewNewsAttachment(
            RequestNewPhotoNewsAttachmentViewModel requestNewPhotoNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news = _newsRepository.Any(x => x.Id == requestNewPhotoNewsAttachmentViewModel.PhotoNewsId);

                if (!news)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                {
                    requestNewPhotoNewsAttachmentViewModel.ImagePath
                }).FirstOrDefault();

                if (uploadAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newNewsAttachment =
                    _mapper.Map<PhotoNewsAttachment>(requestNewPhotoNewsAttachmentViewModel);
                newNewsAttachment.ImagePath = uploadAddress;

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
            RequestEditPhotoNewsAttachmentViewModel requestEditPhotoNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsAttachment =
                    _newsAttachmentRepository
                        .FirstOrDefaultItemAsync(x => x.Id == requestEditPhotoNewsAttachmentViewModel.Id).Result;
                if (newsAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldFilePath = newsAttachment.ImagePath;
                _mapper.Map(requestEditPhotoNewsAttachmentViewModel, newsAttachment);

                if (requestEditPhotoNewsAttachmentViewModel.ImagePath != null)
                {
                    var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                        {requestEditPhotoNewsAttachmentViewModel.ImagePath}).FirstOrDefault();
                    if (uploadAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    newsAttachment.ImagePath = uploadAddress;
                }
                else
                {
                    newsAttachment.ImagePath = oldFilePath;
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

        public async Task<IBusinessLogicResult<ResponseGetPhotoNewsAttachmentListViewModel>> GetNewsAttachmentList(
            RequestGetPhotoNewsAttachmentViewModel requestGetPhotoNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsAttachment = _newsAttachmentRepository.DeferredWhere(x =>
                    x.PhotoNewsId == requestGetPhotoNewsAttachmentViewModel.PhotoNewsId);

                if (!string.IsNullOrEmpty(requestGetPhotoNewsAttachmentViewModel.Title))
                    newsAttachment = newsAttachment.Where(x =>
                        x.Title.Contains(requestGetPhotoNewsAttachmentViewModel.Title));

                var result = new ResponseGetPhotoNewsAttachmentListViewModel
                {
                    NewsAttachmentList = newsAttachment
                        .ProjectTo<ResponseGetPhotoNewsAttachmentViewModel>(_mapper.ConfigurationProvider).ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetPhotoNewsAttachmentListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetPhotoNewsAttachmentListViewModel>(succeeded: false,
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