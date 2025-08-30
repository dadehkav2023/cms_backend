using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.Services.Statement.Category;
using Application.ViewModels.Statement.Category.Request;
using Application.ViewModels.Statement.Category.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Statement;

namespace Application.Services.Statement.Category
{
public class StatementCategoryService : IStatementCategoryService
    {
        private readonly IRepository<StatementCategory> _statementCategoryRepository;
        private readonly IMapper _mapper;

        public StatementCategoryService(IUnitOfWorkStatement statementCategoryRepository, IMapper mapper)
        {
            _statementCategoryRepository = statementCategoryRepository.GetRepository<StatementCategory>();
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> NewStatementCategory(
            RequestNewStatementCategoryViewModel requestNewStatementCategoryViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newStatementCategory =
                    _mapper.Map<StatementCategory>(requestNewStatementCategoryViewModel);

                await _statementCategoryRepository.AddAsync(newStatementCategory);

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

        public async Task<IBusinessLogicResult<bool>> EditStatementCategory(
            RequestEditStatementCategoryViewModel requestEditStatementCategoryViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statementCategory =
                    _statementCategoryRepository.FirstOrDefaultItemAsync(x => x.Id == requestEditStatementCategoryViewModel.Id)
                        .Result;
                if (statementCategory == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CategoryNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                _mapper.Map(requestEditStatementCategoryViewModel, statementCategory);

                await _statementCategoryRepository.UpdateAsync(statementCategory, true);

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

        public async Task<IBusinessLogicResult<List<ResponseGetStatementCategoryViewModel>>> GetAllStatementCategoryList()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statementCategories = _statementCategoryRepository.DeferdSelectAll();


                var result = statementCategories
                    .ProjectTo<ResponseGetStatementCategoryViewModel>(_mapper.ConfigurationProvider).ToList();

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<ResponseGetStatementCategoryViewModel>>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<ResponseGetStatementCategoryViewModel>>(succeeded: false,
                    result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteStatementCategory(int statementCategoryId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statementCategory = _statementCategoryRepository.FirstOrDefaultItemAsync(s => s.Id == statementCategoryId).Result;
                if (statementCategory == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CategoryNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _statementCategoryRepository.RemoveAsync(statementCategory, true);

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