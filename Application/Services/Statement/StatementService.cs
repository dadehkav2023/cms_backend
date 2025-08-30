using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Statement.Request;
using Application.ViewModels.Statement.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Statement;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Statement
{
    public class StatementService : IStatementService
    {
        private readonly IRepository<Domain.Entities.Statement.Statement> _statementRepository;
        private readonly IRepository<Domain.Entities.Statement.StatementCategory> _statementCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;


        public StatementService(IUnitOfWorkStatement unitOfWorkStatement, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _statementRepository = unitOfWorkStatement.GetRepository<Domain.Entities.Statement.Statement>();
            _statementCategoryRepository = unitOfWorkStatement.GetRepository<StatementCategory>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewStatement(
            RequestNewStatementViewModel requestNewStatementViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newStatement =
                    _mapper.Map<Domain.Entities.Statement.Statement>(requestNewStatementViewModel);

                if (requestNewStatementViewModel.ImagePath != null)
                {
                    var uploadedAddress = _fileUploaderService
                        .Upload(new List<IFormFile> {requestNewStatementViewModel.ImagePath})
                        .FirstOrDefault();

                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    newStatement.ImagePath = uploadedAddress;
                }

                var statementCategories = _statementCategoryRepository
                    .DeferredWhere(x => requestNewStatementViewModel.CategoriesId.Contains(x.Id)).ToList();

                newStatement.StatementCategories = statementCategories;
                newStatement.PublishDateTime ??= DateTime.Now;

                await _statementRepository.AddAsync(newStatement);

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

        public async Task<IBusinessLogicResult<bool>> EditStatement(
            RequestEditStatementViewModel requestEditStatementViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statement =
                    _statementRepository
                        .DeferredWhere(n => n.Id == requestEditStatementViewModel.Id)
                        .Include(c => c.StatementCategories)
                        .FirstOrDefaultAsync()
                        .Result;

                if (statement == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.StatementNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldImagePath = statement.ImagePath;

                _mapper.Map(requestEditStatementViewModel, statement);

                #region Upload

                if (requestEditStatementViewModel.ImagePath != null)
                {
                    var uploadedFileAddress = _fileUploaderService
                        .Upload(new List<IFormFile>() {requestEditStatementViewModel.ImagePath})
                        .FirstOrDefault();

                    if (uploadedFileAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    statement.ImagePath = uploadedFileAddress;
                }
                else
                {
                    statement.ImagePath = oldImagePath;
                }

                #endregion

                var statementCategories = _statementCategoryRepository
                    .DeferredWhere(x => requestEditStatementViewModel.CategoriesId.Contains(x.Id)).ToList();

                statement.StatementCategories?.Clear();

                statement.StatementCategories = statementCategories;

                statement.PublishDateTime ??= DateTime.Now;

                await _statementRepository.UpdateAsync(statement, true);

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

        public async Task<IBusinessLogicResult<ResponseGetStatementListViewModel>> GetStatementList(
            RequestGetStatementViewModel requestGetStatementViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statement = _statementRepository.DeferdSelectAll().OrderByDescending(x=>x.PublishDateTime).AsQueryable();
                if (requestGetStatementViewModel.Id is > 0)
                    statement = statement.Where(s => s.Id == requestGetStatementViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetStatementViewModel.Title))
                    statement = statement.Where(s => s.Title.Contains(requestGetStatementViewModel.Title));
                if (requestGetStatementViewModel.IsActive != null)
                    statement = statement.Where(x => x.IsActive == requestGetStatementViewModel.IsActive);
                if (requestGetStatementViewModel.LoadPublishStatements)
                    statement = statement.Where(n => n.PublishDateTime <= DateTime.Now);

                if (statement == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.StatementNotFound));
                    return new BusinessLogicResult<ResponseGetStatementListViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var statementList = statement
                    .ProjectTo<ResponseGetStatementViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetStatementViewModel.Page - 1) * requestGetStatementViewModel.PageSize)
                    .Take(requestGetStatementViewModel.PageSize);

                var result = new ResponseGetStatementListViewModel()
                {
                    Count = statementList.Count(),
                    CurrentPage = requestGetStatementViewModel.Page,
                    TotalCount = statement.Count(),
                    StatementList = statementList.ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetStatementListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetStatementListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteStatement(int statementId, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statement = _statementRepository.FirstOrDefaultItemAsync(s => s.Id == statementId).Result;
                if (statement == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.StatementNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _statementRepository.RemoveAsync(statement, true);

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