using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.ViewModels.News.Category.Request;
using Application.ViewModels.News.Category.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.News;
using Domain.Entities.News.Category;

namespace Application.Services.News.Category
{
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly IRepository<NewsCategory> _newsCategoryRepository;
        private readonly IMapper _mapper;

        public NewsCategoryService(IUnitOfWorkNews newsCategoryRepository, IMapper mapper)
        {
            _newsCategoryRepository = newsCategoryRepository.GetRepository<NewsCategory>();
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> NewNewsCategory(
            RequestNewNewsCategoryViewModel requestNewNewsCategoryViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newNewsCategory =
                    _mapper.Map<NewsCategory>(requestNewNewsCategoryViewModel);

                await _newsCategoryRepository.AddAsync(newNewsCategory);

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

        public async Task<IBusinessLogicResult<bool>> EditNewsCategory(
            RequestEditNewsCategoryViewModel requestEditNewsCategoryViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsCategory =
                    _newsCategoryRepository.FirstOrDefaultItemAsync(x => x.Id == requestEditNewsCategoryViewModel.Id)
                        .Result;
                if (newsCategory == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CategoryNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                _mapper.Map(requestEditNewsCategoryViewModel, newsCategory);

                await _newsCategoryRepository.UpdateAsync(newsCategory, true);

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

        public async Task<IBusinessLogicResult<List<ResponseGetNewsCategoryViewModel>>> GetAllNewsCategoryList()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsCategories = _newsCategoryRepository.DeferdSelectAll();


                var result = newsCategories
                    .ProjectTo<ResponseGetNewsCategoryViewModel>(_mapper.ConfigurationProvider).ToList();

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<ResponseGetNewsCategoryViewModel>>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<ResponseGetNewsCategoryViewModel>>(succeeded: false,
                    result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteNewsCategory(int newsCategoryId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newsCategory = _newsCategoryRepository.FirstOrDefaultItemAsync(s => s.Id == newsCategoryId).Result;
                if (newsCategory == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CategoryNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _newsCategoryRepository.RemoveAsync(newsCategory, true);

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