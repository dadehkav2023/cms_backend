using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Notification.Notification.Request;
using Application.ViewModels.Notification.Notification.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Domain.Entities.Notification.Notification> _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;


        public NotificationService(IUnitOfWorkNotification unitOfWorkNotification, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _notificationRepository = unitOfWorkNotification.GetRepository<Domain.Entities.Notification.Notification>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewNotification(
            RequestNewNotificationViewModel requestNewNotificationViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newNotification =
                    _mapper.Map<Domain.Entities.Notification.Notification>(requestNewNotificationViewModel);

                if (requestNewNotificationViewModel.ImagePath != null)
                {
                    var uploadedAddress = _fileUploaderService
                        .Upload(new List<IFormFile>{ requestNewNotificationViewModel.ImagePath})
                        .FirstOrDefault();
                    
                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    newNotification.ImagePath = uploadedAddress;
                }

                await _notificationRepository.AddAsync(newNotification);

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

        public async Task<IBusinessLogicResult<bool>> EditNotification(
            RequestEditNotificationViewModel requestEditNotificationViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var notification =
                    _notificationRepository.FirstOrDefaultItemAsync(n => n.Id == requestEditNotificationViewModel.Id)
                        .Result;

                if (notification == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NotificationNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldImagePath = notification.ImagePath;

                _mapper.Map(requestEditNotificationViewModel, notification);

                #region Upload

                if (requestEditNotificationViewModel.ImagePath != null)
                {
                    var uploadedFileAddress = _fileUploaderService
                        .Upload(new List<IFormFile>() { requestEditNotificationViewModel.ImagePath })
                        .FirstOrDefault();

                    if (uploadedFileAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    notification.ImagePath = uploadedFileAddress;
                }
                else
                {
                    notification.ImagePath = oldImagePath;
                }

                #endregion

                await _notificationRepository.UpdateAsync(notification, true);

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

        public async Task<IBusinessLogicResult<ResponseGetNotificationListViewModel>> GetNotificationList(
            RequestGetNotificationViewModel requestGetNotificationViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var notification = _notificationRepository.DeferdSelectAll();
                if (requestGetNotificationViewModel.Id is > 0)
                    notification = notification.Where(s => s.Id == requestGetNotificationViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetNotificationViewModel.Title))
                    notification = notification.Where(s => s.Title.Contains(requestGetNotificationViewModel.Title));
                if (requestGetNotificationViewModel.IsActive != null)
                    notification = notification.Where(x => x.IsActive == requestGetNotificationViewModel.IsActive);

                if (notification == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NotificationNotFound));
                    return new BusinessLogicResult<ResponseGetNotificationListViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var notificationList = notification
                    .ProjectTo<ResponseGetNotificationViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetNotificationViewModel.Page - 1) * requestGetNotificationViewModel.PageSize)
                    .Take(requestGetNotificationViewModel.PageSize);

                var result = new ResponseGetNotificationListViewModel()
                {
                    Count = notificationList.Count(),
                    CurrentPage = requestGetNotificationViewModel.Page,
                    TotalCount = notification.Count(),
                    NotificationList = notificationList.ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetNotificationListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetNotificationListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }

        }

        public async Task<IBusinessLogicResult<bool>> DeleteNotification(int notificationId, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var notification = _notificationRepository.FirstOrDefaultItemAsync(s => s.Id == notificationId).Result;
                if (notification == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.NotificationNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _notificationRepository.RemoveAsync(notification, true);

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