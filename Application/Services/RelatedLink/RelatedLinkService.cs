using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.ViewModels.RelatedLink.Request;
using Application.ViewModels.RelatedLink.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Services.RelatedLink
{
    public class RelatedLinkService : IRelatedLinkService
    {
        private readonly IRepository<Domain.Entities.RelatedLink.RelatedLink> _repository;
        private readonly IMapper _mapper;

        public RelatedLinkService(IUnitOfWorkRelatedLink unitOfWorkRelatedLink, IMapper mapper)
        {
            _repository = unitOfWorkRelatedLink.GetRepository<Domain.Entities.RelatedLink.RelatedLink>();
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> NewRelatedLink(
            RequestNewRelatedLinkViewModel requestNewRelatedLinkViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newRelatedLink =
                    _mapper.Map<Domain.Entities.RelatedLink.RelatedLink>(requestNewRelatedLinkViewModel);
                await _repository.AddAsync(newRelatedLink);

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

        public async Task<IBusinessLogicResult<bool>> EditRelatedLink(
            RequestEditRelatedLinkViewModel requestEditRelatedLinkViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var relatedLink = _repository.FirstOrDefaultItemAsync(q => q.Id == requestEditRelatedLinkViewModel.Id)
                    .Result;
                if (relatedLink == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.RelatedLinkNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                _mapper.Map(requestEditRelatedLinkViewModel, relatedLink);

                await _repository.UpdateAsync(relatedLink, true);

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

        public async Task<IBusinessLogicResult<ResponseGetRelatedLinkListViewModel>> GetRelatedLinkList(
            RequestGetRelatedLinkViewModel requestGetRelatedLinkViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var relatedLinks = _repository.DeferdSelectAll();
                if (requestGetRelatedLinkViewModel.Id is > 0)
                    relatedLinks = relatedLinks.Where(s => s.Id == requestGetRelatedLinkViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetRelatedLinkViewModel.Title))
                    relatedLinks = relatedLinks.Where(s => s.Title.Contains(requestGetRelatedLinkViewModel.Title));
                if (requestGetRelatedLinkViewModel.IsActive != null)
                    relatedLinks = relatedLinks.Where(x => x.IsActive == requestGetRelatedLinkViewModel.IsActive);

                if (relatedLinks == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.RelatedLinkNotFound));
                    return new BusinessLogicResult<ResponseGetRelatedLinkListViewModel>(succeeded: false, result: null,
                        messages: messages);
                }

                var relatedLinkList = relatedLinks
                    .ProjectTo<ResponseGetRelatedLinkViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetRelatedLinkViewModel.Page - 1) * requestGetRelatedLinkViewModel.PageSize)
                    .Take(requestGetRelatedLinkViewModel.PageSize);

                var result = new ResponseGetRelatedLinkListViewModel()
                {
                    Count = relatedLinkList.Count(),
                    CurrentPage = requestGetRelatedLinkViewModel.Page,
                    TotalCount = relatedLinks.Count(),
                    RelatedLinkList = relatedLinkList.ToList()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetRelatedLinkListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetRelatedLinkListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteRelatedLink(int relatedLinkId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var relatedLink = _repository.FirstOrDefaultItemAsync(s => s.Id == relatedLinkId).Result;
                if (relatedLink == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.RelatedLinkNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _repository.RemoveAsync(relatedLink, true);

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