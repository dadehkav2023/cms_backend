using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Interfaces.Public.Upload;
using Application.ViewModels.ServiceDesk.Request;
using Application.ViewModels.ServiceDesk.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services.ServiceDesk
{
    public class ServiceDeskService : IServiceDeskService
    {
        private readonly IRepository<Domain.Entities.ServiceDesk.ServiceDesk> _serviceDeskRepository;
        private readonly IUploader _uploader;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        private readonly string _serviceDeskSectionKey = "File:ServiceDesk:ServiceDeskImages";

        public ServiceDeskService(IUnitOfWorkServiceDesk unitOfWorkServiceDesk, IUploader uploader,
            IWebHostEnvironment hostEnvironment, IConfiguration configuration, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _serviceDeskRepository = unitOfWorkServiceDesk.GetRepository<Domain.Entities.ServiceDesk.ServiceDesk>();
            _uploader = uploader;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewServiceDesk(
            RequestNewServiceDeskViewModel requestNewServiceDeskViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                #region Upload
                var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() { requestNewServiceDeskViewModel.ImageFile })
                    .FirstOrDefault();

                if (uploadedAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                #endregion

                var newServiceDesk =
                    _mapper.Map<Domain.Entities.ServiceDesk.ServiceDesk>(requestNewServiceDeskViewModel);
                newServiceDesk.ImagePath = uploadedAddress;

                await _serviceDeskRepository.AddAsync(newServiceDesk);

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

        public async Task<IBusinessLogicResult<bool>> EditServiceDesk(
            RequestEditServiceDeskViewModel requestEditServiceDeskViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var serviceDesk =
                    _serviceDeskRepository.FirstOrDefaultItemAsync(x => x.Id == requestEditServiceDeskViewModel.Id)
                        .Result;
                if (serviceDesk == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.ServiceDeskNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldImagePath = serviceDesk.ImagePath;

                _mapper.Map(requestEditServiceDeskViewModel, serviceDesk);

                #region Upload

                if (requestEditServiceDeskViewModel.ImageFile != null)
                {
                    var uploadedAddress = _fileUploaderService
                        .Upload(new List<IFormFile>() { requestEditServiceDeskViewModel.ImageFile })
                        .FirstOrDefault();

                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    serviceDesk.ImagePath = uploadedAddress;
                }
                else
                {
                    serviceDesk.ImagePath = oldImagePath;
                }

                #endregion

                await _serviceDeskRepository.UpdateAsync(serviceDesk, true);

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

        public async Task<IBusinessLogicResult<ResponseGetServiceDeskListViewModel>> GetServiceDesk(
            RequestGetServiceDeskListViewModel requestGetServiceDeskListViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var serviceDesk = _serviceDeskRepository.DeferdSelectAll();
                if (requestGetServiceDeskListViewModel.Id is > 0)
                    serviceDesk = serviceDesk.Where(s => s.Id == requestGetServiceDeskListViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetServiceDeskListViewModel.Title))
                    serviceDesk = serviceDesk.Where(s => s.Title.Contains(requestGetServiceDeskListViewModel.Title));
                if (requestGetServiceDeskListViewModel.IsActive != null)
                    serviceDesk = serviceDesk.Where(x => x.IsActive == requestGetServiceDeskListViewModel.IsActive);

                if (serviceDesk == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.ServiceDeskNotFound));
                    return new BusinessLogicResult<ResponseGetServiceDeskListViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var serviceDeskList = serviceDesk
                    .ProjectTo<ResponseGetServiceDeskViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetServiceDeskListViewModel.Page - 1) * requestGetServiceDeskListViewModel.PageSize)
                    .Take(requestGetServiceDeskListViewModel.PageSize);

                var result = new ResponseGetServiceDeskListViewModel
                {
                    Count = serviceDeskList.Count(),
                    CurrentPage = requestGetServiceDeskListViewModel.Page,
                    TotalCount = serviceDesk.Count(),
                    ServiceDeskList = serviceDeskList.ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetServiceDeskListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetServiceDeskListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> RemoveServiceDesk(int serviceDeskId, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var serviceDesk = _serviceDeskRepository.FirstOrDefaultItemAsync(s => s.Id == serviceDeskId).Result;
                if (serviceDesk == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.ServiceDeskNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _serviceDeskRepository.RemoveAsync(serviceDesk, true);

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