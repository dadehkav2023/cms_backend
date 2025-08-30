using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Utilities;
using Application.ViewModels.Statement.Attachment.Request;
using Application.ViewModels.Statement.Attachment.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Statement;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Statement.Attachment
{
    public class StatementAttachmentService : IStatementAttachmentService
    {
        private readonly IRepository<StatementAttachment> _statementAttachmentRepository;
        private readonly IRepository<Domain.Entities.Statement.Statement> _statementRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public StatementAttachmentService(IUnitOfWorkStatement unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _statementAttachmentRepository = unitOfWork.GetRepository<StatementAttachment>();
            _statementRepository = unitOfWork.GetRepository<Domain.Entities.Statement.Statement>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewStatementAttachment(
            RequestNewStatementAttachmentViewModel requestNewStatementAttachmentViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statement =
                    _statementRepository.Any(x => x.Id == requestNewStatementAttachmentViewModel.StatementId);
                if (!statement)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.StatementNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                {
                    requestNewStatementAttachmentViewModel.AttachmentFile
                }).FirstOrDefault();

                if (uploadAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newStatementAttachment =
                    _mapper.Map<StatementAttachment>(requestNewStatementAttachmentViewModel);
                newStatementAttachment.AttachmentFile = uploadAddress;
                newStatementAttachment.FileType =
                    requestNewStatementAttachmentViewModel.AttachmentFile.GetFileExtension();

                await _statementAttachmentRepository.AddAsync(newStatementAttachment);

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

        public async Task<IBusinessLogicResult<bool>> EditStatementAttachment(
            RequestEditStatementAttachmentViewModel requestEditStatementAttachmentViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statementAttachment =
                    _statementAttachmentRepository
                        .FirstOrDefaultItemAsync(x => x.Id == requestEditStatementAttachmentViewModel.Id).Result;
                if (statementAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldFilePath = statementAttachment.AttachmentFile;
                _mapper.Map(requestEditStatementAttachmentViewModel, statementAttachment);

                if (requestEditStatementAttachmentViewModel.AttachmentFile != null)
                {
                    var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                        {requestEditStatementAttachmentViewModel.AttachmentFile}).FirstOrDefault();
                    if (uploadAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    statementAttachment.AttachmentFile = uploadAddress;
                    statementAttachment.FileType =
                        requestEditStatementAttachmentViewModel.AttachmentFile.GetFileExtension();
                }
                else
                {
                    statementAttachment.AttachmentFile = oldFilePath;
                }

                await _statementAttachmentRepository.UpdateAsync(statementAttachment, true);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                    message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<ResponseGetStatementAttachmentListViewModel>> GetStatementAttachmentList(
            RequestGetStatementAttachmentViewModel requestGetStatementAttachmentViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statementAttachment = _statementAttachmentRepository.DeferredWhere(x =>
                    x.StatementId == requestGetStatementAttachmentViewModel.StatementId);

                if (!string.IsNullOrEmpty(requestGetStatementAttachmentViewModel.Title))
                    statementAttachment = statementAttachment.Where(x =>
                        x.Title.Contains(requestGetStatementAttachmentViewModel.Title));

                var result = new ResponseGetStatementAttachmentListViewModel
                {
                    StatementAttachmentList = statementAttachment
                        .ProjectTo<ResponseGetStatementAttachmentViewModel>(_mapper.ConfigurationProvider).ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetStatementAttachmentListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetStatementAttachmentListViewModel>(succeeded: false,
                    result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteStatementAttachment(int statementAttachmentId, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var statementAttachment = _statementAttachmentRepository.FirstOrDefaultItemAsync(s => s.Id == statementAttachmentId).Result;
                if (statementAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _statementAttachmentRepository.RemoveAsync(statementAttachment, true);

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