using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Utilities;
using Application.ViewModels.Notification.Notification.Attachment.Request;
using Application.ViewModels.Notification.Notification.Attachment.Response;
using Application.ViewModels.Notification.Notification.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Enum;
using Domain.Entities.Notification;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Notification.Attachment
{
    public class NotificationAttachmentService : INotificationAttachmentService
    {
        private readonly IRepository<NotificationAttachment> _notificationAttachmentRepository;
        private readonly IRepository<Domain.Entities.Notification.Notification> _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public NotificationAttachmentService(IUnitOfWorkNotification unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _notificationAttachmentRepository = unitOfWork.GetRepository<NotificationAttachment>();
            _notificationRepository = unitOfWork.GetRepository<Domain.Entities.Notification.Notification>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewNotificationAttachment(
            RequestNewNotificationAttachmentViewModel requestNewNotificationAttachmentViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var notification =
                    _notificationRepository.Any(x => x.Id == requestNewNotificationAttachmentViewModel.NotificationId);
                if (!notification)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.NotificationNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                {
                    requestNewNotificationAttachmentViewModel.AttachmentFile
                }).FirstOrDefault();

                if (uploadAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newNotificationAttachment =
                    _mapper.Map<NotificationAttachment>(requestNewNotificationAttachmentViewModel);
                newNotificationAttachment.AttachmentFile = uploadAddress;
                newNotificationAttachment.FileType =
                    requestNewNotificationAttachmentViewModel.AttachmentFile.GetFileExtension();

                await _notificationAttachmentRepository.AddAsync(newNotificationAttachment);

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

        public async Task<IBusinessLogicResult<bool>> EditNotificationAttachment(
            RequestEditNotificationAttachmentViewModel requestEditNotificationAttachmentViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var notificationAttachment =
                    _notificationAttachmentRepository
                        .FirstOrDefaultItemAsync(x => x.Id == requestEditNotificationAttachmentViewModel.Id).Result;
                if (notificationAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldFilePath = notificationAttachment.AttachmentFile;
                _mapper.Map(requestEditNotificationAttachmentViewModel, notificationAttachment);

                if (requestEditNotificationAttachmentViewModel.AttachmentFile != null)
                {
                    var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                        {requestEditNotificationAttachmentViewModel.AttachmentFile}).FirstOrDefault();
                    if (uploadAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    notificationAttachment.AttachmentFile = uploadAddress;
                    notificationAttachment.FileType =
                        requestEditNotificationAttachmentViewModel.AttachmentFile.GetFileExtension();
                }
                else
                {
                    notificationAttachment.AttachmentFile = oldFilePath;
                }

                await _notificationAttachmentRepository.UpdateAsync(notificationAttachment, true);

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

        public async Task<IBusinessLogicResult<ResponseGetNotificationAttachmentListViewModel>>
            GetNotificationAttachmentList(
                RequestGetNotificationAttachmentViewModel requestGetNotificationAttachmentViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var notificationAttachment = _notificationAttachmentRepository.DeferredWhere(x =>
                    x.NotificationId == requestGetNotificationAttachmentViewModel.NotificationId);

                if (!string.IsNullOrEmpty(requestGetNotificationAttachmentViewModel.Title))
                    notificationAttachment = notificationAttachment.Where(x =>
                        x.Title.Contains(requestGetNotificationAttachmentViewModel.Title));

                var result = new ResponseGetNotificationAttachmentListViewModel
                {
                    NotificationAttachmentList = notificationAttachment
                        .ProjectTo<ResponseGetNotificationAttachmentViewModel>(_mapper.ConfigurationProvider).ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetNotificationAttachmentListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetNotificationAttachmentListViewModel>(succeeded: false,
                    result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteNotificationAttachment(int notificationAttachmentId, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var notificationAttachment = _notificationAttachmentRepository.FirstOrDefaultItemAsync(s => s.Id == notificationAttachmentId).Result;
                if (notificationAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _notificationAttachmentRepository.RemoveAsync(notificationAttachment, true);

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