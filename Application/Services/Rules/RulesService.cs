using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Rules.Request;
using Application.ViewModels.Rules.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Services.Rules
{
    public class RulesService : IRulesService
    {
        private readonly IRepository<Domain.Entities.Rules.Rules> _rulesRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;


        public RulesService(IUnitOfWorkRules unitOfWorkRules, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _rulesRepository = unitOfWorkRules.GetRepository<Domain.Entities.Rules.Rules>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<IBusinessLogicResult<bool>> SetRule(RequestSetRulesViewModel requestSetRulesViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var rules = _rulesRepository.DeferdSelectAll().FirstOrDefault();
                if (rules == null)
                {
                    var newRules =
                        _mapper.Map<Domain.Entities.Rules.Rules>(requestSetRulesViewModel);
                    await _rulesRepository.AddAsync(newRules);
                }
                else
                {
                    _mapper.Map(requestSetRulesViewModel, rules);
                    await _rulesRepository.UpdateAsync(rules, true);
                }
                
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

        public async Task<IBusinessLogicResult<ResponseGetRulesViewModel>> GetRule()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var rules = _rulesRepository.DeferdSelectAll();
               
                if (rules == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.RulesNotFound));
                    return new BusinessLogicResult<ResponseGetRulesViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var result = rules
                    .ProjectTo<ResponseGetRulesViewModel>(_mapper.ConfigurationProvider).FirstOrDefault();


                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetRulesViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetRulesViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }

        }

    }
}