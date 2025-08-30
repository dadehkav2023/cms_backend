using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.ViewModels.QuickAccess.Request;
using Application.ViewModels.QuickAccess.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Services.QuickAccess
{
    public class QuickAccessService : IQuickAccessService
    {
        private readonly IRepository<Domain.Entities.QuickAccess.QuickAccess> _repository;
        private readonly IMapper _mapper;

        public QuickAccessService(IUnitOfWorkQuickAccess unitOfWorkQuickAccess, IMapper mapper)
        {
            _repository = unitOfWorkQuickAccess.GetRepository<Domain.Entities.QuickAccess.QuickAccess>();
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> NewQuickAccess(
            RequestNewQuickAccessViewModel requestNewQuickAccessViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newQuickAccess =
                    _mapper.Map<Domain.Entities.QuickAccess.QuickAccess>(requestNewQuickAccessViewModel);
                await _repository.AddAsync(newQuickAccess);

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

        public async Task<IBusinessLogicResult<bool>> EditQuickAccess(
            RequestEditQuickAccessViewModel requestEditQuickAccessViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var quickAccess = _repository.FirstOrDefaultItemAsync(q => q.Id == requestEditQuickAccessViewModel.Id)
                    .Result;
                if (quickAccess == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.QuickAccessNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                _mapper.Map(requestEditQuickAccessViewModel, quickAccess);

                await _repository.UpdateAsync(quickAccess, true);

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

        public async Task<IBusinessLogicResult<ResponseGetQuickAccessListViewModel>> GetQuickAccessList(
            RequestGetQuickAccessViewModel requestGetQuickAccessViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var quickAccess = _repository.DeferdSelectAll();
                if (requestGetQuickAccessViewModel.Id is > 0)
                    quickAccess = quickAccess.Where(s => s.Id == requestGetQuickAccessViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetQuickAccessViewModel.Title))
                    quickAccess = quickAccess.Where(s => s.Title.Contains(requestGetQuickAccessViewModel.Title));
                if (requestGetQuickAccessViewModel.IsActive != null)
                    quickAccess = quickAccess.Where(x => x.IsActive == requestGetQuickAccessViewModel.IsActive);

                if (quickAccess == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.QuickAccessNotFound));
                    return new BusinessLogicResult<ResponseGetQuickAccessListViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var quickAccessList = quickAccess
                    .ProjectTo<ResponseGetQuickAccessViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetQuickAccessViewModel.Page - 1) * requestGetQuickAccessViewModel.PageSize)
                    .Take(requestGetQuickAccessViewModel.PageSize);

                var result = new ResponseGetQuickAccessListViewModel()
                {
                    Count = quickAccessList.Count(),
                    CurrentPage = requestGetQuickAccessViewModel.Page,
                    TotalCount = quickAccess.Count(),
                    QuickAccessList = quickAccessList.ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetQuickAccessListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetQuickAccessListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
            
        }

        public async Task<IBusinessLogicResult<bool>> DeleteQuickAccess(int quickAccessId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var quickAccess = _repository.FirstOrDefaultItemAsync(s => s.Id == quickAccessId).Result;
                if (quickAccess == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.QuickAccessNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _repository.RemoveAsync(quickAccess, true);

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