using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.Interfaces.Public.Upload;
using Application.Services.Public.Upload;
using Application.ViewModels.CMS.Setting.Request;
using Application.ViewModels.CMS.Setting.Response;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.ExternalApi.FileService;
using Microsoft.AspNetCore.Http;

namespace Application.Services.CMS.Setting
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Domain.Entities.CMS.Setting.Setting> settingRepository;
        private readonly IMapper mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public SettingService(IUnitOfWorkApplication unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            settingRepository = unitOfWork.GetRepository<Domain.Entities.CMS.Setting.Setting>();
            this.mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<ResponseGetSettingViewModel>> GetSetting(int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var setting = settingRepository.DeferdSelectAll().FirstOrDefault();
                if (setting == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SettingIsEmpty));
                    return new BusinessLogicResult<ResponseGetSettingViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var result = mapper.Map<ResponseGetSettingViewModel>(setting);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetSettingViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception ex)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetSettingViewModel>(succeeded: false, result: null,
                    messages: messages, exception: ex);
            }
        }

        public async Task<IBusinessLogicResult<bool>> SetSetting(RequestSetSettingViewModel requestSetSettingViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                IBusinessLogicResult<bool> result = null;

                var setting = settingRepository.DeferdSelectAll().FirstOrDefault();
                if (setting != null)
                {
                    result = Update(setting, requestSetSettingViewModel).Result;
                }
                else
                {
                    result = Insert(requestSetSettingViewModel).Result;
                }

                if (result != null) return result;

                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
            }
            catch (Exception ex)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: ex);
            }
        }

        private async Task<IBusinessLogicResult<bool>> Insert(RequestSetSettingViewModel requestSetSettingViewModel)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                var mappedSetting = mapper.Map<Domain.Entities.CMS.Setting.Setting>(requestSetSettingViewModel);

                #region Upload Logo

                var uploadedFileAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() {requestSetSettingViewModel.LogoImageAddress})
                    .FirstOrDefault();

                if (uploadedFileAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                mappedSetting.LogoImageAddress = uploadedFileAddress;

                #endregion

                var result =  settingRepository.AddAsync(mappedSetting).Result;

                if (result != null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                    return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
                }

                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
            }
            catch (Exception ex)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: ex);
            }
        }

        private async Task<IBusinessLogicResult<bool>> Update(Domain.Entities.CMS.Setting.Setting currentSetting,
            RequestSetSettingViewModel requestSetSettingViewModel)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                var oldLogoImagePath = currentSetting.LogoImageAddress;
                mapper.Map(requestSetSettingViewModel,
                    currentSetting);

                #region upload logo

                if (requestSetSettingViewModel.LogoImageAddress != null)
                {
                    var uploadedFileAddress = _fileUploaderService
                        .Upload(new List<IFormFile>() {requestSetSettingViewModel.LogoImageAddress})
                        .FirstOrDefault();

                    if (uploadedFileAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    currentSetting.LogoImageAddress = uploadedFileAddress;
                }
                else
                {
                    currentSetting.LogoImageAddress = oldLogoImagePath;
                }

                #endregion

                var result =  settingRepository.UpdateAsync(currentSetting, true).Result;
                if (result != null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                    return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
                }

                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
            }
            catch (Exception ex)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: ex);
            }
        }
    }
}