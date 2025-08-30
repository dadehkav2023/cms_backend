using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.ContactUs.ContactUs.Request;
using Application.ViewModels.ContactUs.ContactUs.Response;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Services.ContactUs
{
    public class ContactUsService : IContactUsService
    {
        private readonly IRepository<Domain.Entities.ContactUs.ContactUs> _repository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public ContactUsService(IUnitOfWorkContactUs unitOfWorkContactUs, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _repository = unitOfWorkContactUs.GetRepository<Domain.Entities.ContactUs.ContactUs>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<BusinessLogicResult<bool>> SetContactUs(RequestSetContactUsViewModel requestSetContactUsViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var contactUs = _repository.DeferdSelectAll().FirstOrDefault();
                if (contactUs != null)
                {
                    Update(contactUs, requestSetContactUsViewModel);
                }
                else
                {
                    Insert(requestSetContactUsViewModel);
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

        public async Task<BusinessLogicResult<ResponseGetContactUs>> GetContactUs()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var contactUs = _repository.DeferdSelectAll().FirstOrDefault();
                if (contactUs == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SettingIsEmpty));
                    return new BusinessLogicResult<ResponseGetContactUs>(succeeded: false, result: null,
                        messages: messages);
                }

                var result = _mapper.Map<ResponseGetContactUs>(contactUs);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetContactUs>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception ex)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetContactUs>(succeeded: false, result: null,
                    messages: messages, exception: ex);
            }
        }

        
        
        
        private async void Insert(RequestSetContactUsViewModel requestSetContactUsViewModel)
        {
            var newContactUs = _mapper.Map<Domain.Entities.ContactUs.ContactUs>(requestSetContactUsViewModel);

            #region Upload Header Image

            if (requestSetContactUsViewModel.HeaderImage != null)
            {
                var uploadedFileAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() {requestSetContactUsViewModel.HeaderImage})
                    .FirstOrDefault();

                if (uploadedFileAddress == null)
                {
                    throw new Exception("Cannot Upload File");
                }

                newContactUs.HeaderImage = uploadedFileAddress;
            }

            #endregion

            await _repository.AddAsync(newContactUs);
        }

        private async void Update(Domain.Entities.ContactUs.ContactUs currentContactUs,
            RequestSetContactUsViewModel requestSetContactUsViewModel)
        {
            var oldHeaderImagePath = currentContactUs.HeaderImage;
            
            _mapper.Map(requestSetContactUsViewModel,
                currentContactUs);

            #region upload header image

            if (requestSetContactUsViewModel.HeaderImage != null)
            {
                var uploadedFileAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() {requestSetContactUsViewModel.HeaderImage})
                    .FirstOrDefault();

                if (uploadedFileAddress == null)
                {
                    throw new Exception("Cannot Upload File");
                }

                currentContactUs.HeaderImage = uploadedFileAddress;
            }
            else
            {
                currentContactUs.HeaderImage = oldHeaderImagePath;
            }

            #endregion

            await _repository.UpdateAsync(currentContactUs, true);
        }
    }
}