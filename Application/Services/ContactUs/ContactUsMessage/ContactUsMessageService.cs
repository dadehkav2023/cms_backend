using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Request;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Response;
using Application.ViewModels.QuickAccess.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Services.ContactUs.ContactUsMessage
{
    public class ContactUsMessageService : IContactUsMessageService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.ContactUs.ContactUsMessage> _repository;

        public ContactUsMessageService(IMapper mapper, IUnitOfWorkContactUs unitOfWorkContactUs)
        {
            _mapper = mapper;
            _repository = unitOfWorkContactUs.GetRepository<Domain.Entities.ContactUs.ContactUsMessage>();
        }

        public async Task<BusinessLogicResult<bool>> NewContactUsMessage(
            RequestNewContactUsMessageViewModel requestNewContactUsMessageViewModel, int? userId = null)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                if (userId == null && string.IsNullOrEmpty(requestNewContactUsMessageViewModel.FirstNameAndLastName))
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.FirstNameAndLastNameIsRequired));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newMessage =
                    _mapper.Map<Domain.Entities.ContactUs.ContactUsMessage>(requestNewContactUsMessageViewModel);
                if (userId != null)
                {
                    newMessage.UserId = userId;
                }

                await _repository.AddAsync(newMessage);

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

        public async Task<BusinessLogicResult<ResponseGetContactUsMessageListViewModel>> GetContactUsMessageList(
            RequestGetContactUsMessageViewModel requestGetContactUsMessageViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var contactUsMessages = _repository.DeferdSelectAll();

                if (requestGetContactUsMessageViewModel.UserId != null)
                    contactUsMessages =
                        contactUsMessages.Where(c => c.UserId == requestGetContactUsMessageViewModel.UserId);

                if (!string.IsNullOrEmpty(requestGetContactUsMessageViewModel.Email))
                    contactUsMessages =
                        contactUsMessages.Where(x => x.Email == requestGetContactUsMessageViewModel.Email);

                if (!string.IsNullOrEmpty(requestGetContactUsMessageViewModel.TextMessage))
                    contactUsMessages =
                        contactUsMessages.Where(x =>
                            x.TextMessage.Contains(requestGetContactUsMessageViewModel.TextMessage));

                if (!string.IsNullOrEmpty(requestGetContactUsMessageViewModel.FirstNameAndLastName))
                    contactUsMessages =
                        contactUsMessages.Where(x =>
                            x.FirstNameAndLastName.Contains(requestGetContactUsMessageViewModel.FirstNameAndLastName));

                var contactUsMessageList = contactUsMessages
                    .ProjectTo<ResponseGetContactUsMessageViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetContactUsMessageViewModel.Page - 1) * requestGetContactUsMessageViewModel.PageSize)
                    .Take(requestGetContactUsMessageViewModel.PageSize);

                var result = new ResponseGetContactUsMessageListViewModel
                {
                    Count = contactUsMessageList.Count(),
                    CurrentPage = requestGetContactUsMessageViewModel.Page,
                    TotalCount = contactUsMessages.Count(),
                    ContactUseMessageList = contactUsMessageList.ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetContactUsMessageListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetContactUsMessageListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }
    }
}