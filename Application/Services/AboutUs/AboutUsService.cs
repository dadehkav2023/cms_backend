using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.AboutUs.Request;
using Application.ViewModels.AboutUs.Response;
using Application.ViewModels.CMS.Setting.Request;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Services.AboutUs
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IRepository<Domain.Entities.AboutUs.AboutUs> _repository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public AboutUsService(IUnitOfWorkAboutUs unitOfWorkAboutUs, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _repository = unitOfWorkAboutUs.GetRepository<Domain.Entities.AboutUs.AboutUs>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<BusinessLogicResult<bool>> SetAboutUs(RequestSetAboutUsViewModel requestSetAboutUsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var aboutUs = _repository.DeferdSelectAll().FirstOrDefault();
                if (aboutUs != null)
                {
                    Update(aboutUs, requestSetAboutUsViewModel);
                }
                else
                {
                    Insert(requestSetAboutUsViewModel);
                }

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception ex)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: ex);
            }
            
        }

        public async Task<BusinessLogicResult<ResponseGetAboutUs>> GetAboutUs()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var aboutUs = _repository.DeferdSelectAll().FirstOrDefault();
                if (aboutUs == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SettingIsEmpty));
                    return new BusinessLogicResult<ResponseGetAboutUs>(succeeded: false, result: null,
                        messages: messages);
                }

                var result = _mapper.Map<ResponseGetAboutUs>(aboutUs);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetAboutUs>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception ex)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetAboutUs>(succeeded: false, result: null,
                    messages: messages, exception: ex);
            }
        }

        
        
        
        private async void Insert(RequestSetAboutUsViewModel requestSetAboutUsViewModel)
        {
            var newAboutUs = _mapper.Map<Domain.Entities.AboutUs.AboutUs>(requestSetAboutUsViewModel);

            #region Upload Header Image

            if (requestSetAboutUsViewModel.HeaderImage != null)
            {
                var uploadedFileAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() {requestSetAboutUsViewModel.HeaderImage})
                    .FirstOrDefault();

                if (uploadedFileAddress == null)
                {
                    throw new Exception("Cannot Upload File");
                }

                newAboutUs.HeaderImage = uploadedFileAddress;
            }

            #endregion

            await _repository.AddAsync(newAboutUs);
        }

        private async void Update(Domain.Entities.AboutUs.AboutUs currentAboutUs,
            RequestSetAboutUsViewModel requestSetAboutUsViewModel)
        {
            var oldHeaderImagePath = currentAboutUs.HeaderImage;
            
            _mapper.Map(requestSetAboutUsViewModel,
                currentAboutUs);

            #region upload header image

            if (requestSetAboutUsViewModel.HeaderImage != null)
            {
                var uploadedFileAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() {requestSetAboutUsViewModel.HeaderImage})
                    .FirstOrDefault();

                if (uploadedFileAddress == null)
                {
                    throw new Exception("Cannot Upload File");
                }

                currentAboutUs.HeaderImage = uploadedFileAddress;
            }
            else
            {
                currentAboutUs.HeaderImage = oldHeaderImagePath;
            }

            #endregion

            await _repository.UpdateAsync(currentAboutUs, true);
        }
    }
}