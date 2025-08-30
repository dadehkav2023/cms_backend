using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.Services.NoticesService;
using Application.ViewModels.Accounting;
using Application.ViewModels.Accounting.Request;
using AutoMapper;
using Common.Enum;
using Domain.Entities.Identity.Accounting;
using Domain.Entities.Identity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Accounting
{
    public class RegisterService : IRegisterService
    {
        private readonly ISmsSenderService _smsSenderService;
        private readonly IRepository<ValidationCode> _repositoryValidationCode;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterService(ISmsSenderService smsSenderService, IUnitOfWorkApplication _unitOfWorkApplication,
            UserManager<User> userManager, IMapper mapper)
        {
            _smsSenderService = smsSenderService;
            _repositoryValidationCode = _unitOfWorkApplication.GetRepository<ValidationCode>();
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> SendValidationCode(string mobileNumber)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var generatedCode = new Random().Next(100000, 999999).ToString();
                if (_smsSenderService.SendSms(mobileNumber, SmsMessageEnum.Simple,
                    generatedCode))
                {
                    await _repositoryValidationCode.AddAsync(new ValidationCode
                    {
                        MobileNumber = mobileNumber,
                        VerificationCode = generatedCode,
                        ExpirationDateTime = DateTime.Now.AddMinutes(5)
                    });

                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                    return new BusinessLogicResult<bool>(succeeded: true, result: true,
                        messages: messages);
                }

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: true, result: true,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> CheckValidationCode(
            RequestCheckValidationCodeViewModel requestCheckValidationCode)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var validationCode = _repositoryValidationCode
                    .DeferredWhere(x => x.MobileNumber == requestCheckValidationCode.MobileNumber)
                    .Where(d => DateTime.Now <= d.ExpirationDateTime)
                    .Where(c => c.VerificationCode == requestCheckValidationCode.ValidationCode)
                    .OrderBy(x => x.Id)
                    .LastOrDefaultAsync().Result;

                if (validationCode == null || (validationCode.IsConfirmed))
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.ValidationCodeIsWrong));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                validationCode.IsConfirmed = true;
                await _repositoryValidationCode.UpdateAsync(validationCode, true);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                    message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> Register(RequestRegisterViewModel requestRegisterViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                // var validationCode = _repositoryValidationCode
                //     .DeferredWhere(x => x.MobileNumber == requestRegisterViewModel.PhoneNumber)
                //     .OrderBy(x => x.Id)
                //     .LastOrDefaultAsync().Result;
                // if (validationCode is not {IsConfirmed: true})
                // {
                //     messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                //         message: MessageId.MobileNumberWasNotConfirmed));
                //     return new BusinessLogicResult<bool>(succeeded: false, result: false,
                //         messages: messages);
                // }
                
                var user = _mapper.Map<User>(requestRegisterViewModel);
                user.PhoneNumberConfirmed = true;
                var createUser = await _userManager.CreateAsync(user);

                if (createUser.Succeeded)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.Success));
                    return new BusinessLogicResult<bool>(succeeded: true, result: true,
                        messages: messages);
                }

                var errors = createUser.Errors.Select(x => x.Description);

                messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                    message: MessageId.ErrorOccured));
                return new BusinessLogicResult<bool>(succeeded: false, result: false,
                    messages: messages, errorFileds: errors);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false,
                    messages: messages, exception: exception);
            }
        }
    }
}