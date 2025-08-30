using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Utilities;
using Application.ViewModels.Rules.Attachment.Request;
using Application.ViewModels.Rules.Attachment.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Rules;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Rules.Attachment
{
    public class RulesAttachmentService : IRulesAttachmentService
    {
        private readonly IRepository<RulesAttachment> _rulesAttachmentRepository;
        private readonly IRepository<Domain.Entities.Rules.Rules> _rulesRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public RulesAttachmentService(IUnitOfWorkRules unitOfWork, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _rulesAttachmentRepository = unitOfWork.GetRepository<RulesAttachment>();
            _rulesRepository = unitOfWork.GetRepository<Domain.Entities.Rules.Rules>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> NewRulesAttachment(
            RequestNewRulesAttachmentViewModel requestNewRulesAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var rules =
                    _rulesRepository.Any(x => x.Id == requestNewRulesAttachmentViewModel.RulesId);
                if (!rules)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info,
                        message: MessageId.RulesNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                {
                    requestNewRulesAttachmentViewModel.AttachmentFile
                }).FirstOrDefault();

                if (uploadAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var newRulesAttachment =
                    _mapper.Map<RulesAttachment>(requestNewRulesAttachmentViewModel);
                newRulesAttachment.FilePath = uploadAddress;
                newRulesAttachment.FileType =
                    requestNewRulesAttachmentViewModel.AttachmentFile.GetFileExtension();

                await _rulesAttachmentRepository.AddAsync(newRulesAttachment);

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

        public async Task<IBusinessLogicResult<bool>> EditRulesAttachment(
            RequestEditRulesAttachmentViewModel requestEditRulesAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var rulesAttachment =
                    _rulesAttachmentRepository
                        .FirstOrDefaultItemAsync(x => x.Id == requestEditRulesAttachmentViewModel.Id).Result;
                if (rulesAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldFilePath = rulesAttachment.FilePath;
                _mapper.Map(requestEditRulesAttachmentViewModel, rulesAttachment);

                if (requestEditRulesAttachmentViewModel.AttachmentFile != null)
                {
                    var uploadAddress = _fileUploaderService.Upload(new List<IFormFile>
                        {requestEditRulesAttachmentViewModel.AttachmentFile}).FirstOrDefault();
                    if (uploadAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    rulesAttachment.FilePath = uploadAddress;
                    rulesAttachment.FileType =
                        requestEditRulesAttachmentViewModel.AttachmentFile.GetFileExtension();
                }
                else
                {
                    rulesAttachment.FilePath = oldFilePath;
                }

                await _rulesAttachmentRepository.UpdateAsync(rulesAttachment, true);

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

        public async Task<IBusinessLogicResult<ResponseGetRulesAttachmentListViewModel>>
            GetRulesAttachmentList(
                RequestGetRulesAttachmentViewModel requestGetRulesAttachmentViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var rulesAttachment = _rulesAttachmentRepository.DeferredWhere(x =>
                    x.RulesId == requestGetRulesAttachmentViewModel.RulesId);

                if (!string.IsNullOrEmpty(requestGetRulesAttachmentViewModel.Title))
                    rulesAttachment = rulesAttachment.Where(x =>
                        x.Title.Contains(requestGetRulesAttachmentViewModel.Title));

                var result = new ResponseGetRulesAttachmentListViewModel
                {
                    RulesAttachmentList = rulesAttachment
                        .ProjectTo<ResponseGetRulesAttachmentViewModel>(_mapper.ConfigurationProvider).ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetRulesAttachmentListViewModel>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetRulesAttachmentListViewModel>(succeeded: false,
                    result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteRulesAttachment(int rulesAttachmentId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var rulesAttachment = _rulesAttachmentRepository.FirstOrDefaultItemAsync(s => s.Id == rulesAttachmentId).Result;
                if (rulesAttachment == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.AttachmentNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _rulesAttachmentRepository.RemoveAsync(rulesAttachment, true);

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