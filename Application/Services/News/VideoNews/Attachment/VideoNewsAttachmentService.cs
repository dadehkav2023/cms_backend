using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Attachment.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.News.VideoNews;
using Microsoft.AspNetCore.Http;

namespace Application.Services.News.VideoNews.Attachment
{
    public class VideoNewsAttachmentService : IVideoNewsAttachmentService
    {
        private readonly IRepository<VideoNewsAttachment> _newsAttachmentRepository;
        private readonly IRepository<Domain.Entities.News.VideoNews.VideoNews> _newsRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public VideoNewsAttachmentService(IUnitOfWorkNews unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _newsAttachmentRepository = unitOfWork.GetRepository<VideoNewsAttachment>();
            _newsRepository = unitOfWork.GetRepository<Domain.Entities.News.VideoNews.VideoNews>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewNewsAttachment(
            RequestNewVideoNewsAttachmentViewModel requestNewVideoNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var news = _newsRepository.Any(x => x.Id == requestNewVideoNewsAttachmentViewModel.VideoNewsId);

                if (!news)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.NewsNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }
                
                var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                {
                    requestNewVideoNewsAttachmentViewModel.VideoPath
                }).FirstOrDefault();

                if (uploadAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newNewsAttachment =
                    _mapper.Map<VideoNewsAttachment>(requestNewVideoNewsAttachmentViewModel);
                newNewsAttachment.VideoPath = uploadAddress;

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
            RequestEditVideoNewsAttachmentViewModel requestEditVideoNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsAttachment =
                    _newsAttachmentRepository
                        .FirstOrDefaultItemAsync(x => x.Id == requestEditVideoNewsAttachmentViewModel.Id).Result;
                if (newsAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldFilePath = newsAttachment.VideoPath;
                _mapper.Map(requestEditVideoNewsAttachmentViewModel, newsAttachment);

                if (requestEditVideoNewsAttachmentViewModel.VideoPath != null)
                {
                    var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                        {requestEditVideoNewsAttachmentViewModel.VideoPath}).FirstOrDefault();
                    if (uploadAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    newsAttachment.VideoPath = uploadAddress;
                }
                else
                {
                    newsAttachment.VideoPath = oldFilePath;
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

        public async Task<IBusinessLogicResult<ResponseGetVideoNewsAttachmentListViewModel>> GetNewsAttachmentList(
            RequestGetVideoNewsAttachmentViewModel requestGetVideoNewsAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsAttachment = _newsAttachmentRepository.DeferredWhere(x =>
                    x.VideoNewsId == requestGetVideoNewsAttachmentViewModel.VideoNewsId);

                if (!string.IsNullOrEmpty(requestGetVideoNewsAttachmentViewModel.Title))
                    newsAttachment = newsAttachment.Where(x =>
                        x.Title.Contains(requestGetVideoNewsAttachmentViewModel.Title));

                var result = new ResponseGetVideoNewsAttachmentListViewModel
                {
                    NewsAttachmentList = newsAttachment
                        .ProjectTo<ResponseGetVideoNewsAttachmentViewModel>(_mapper.ConfigurationProvider).ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetVideoNewsAttachmentListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetVideoNewsAttachmentListViewModel>(succeeded: false,
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