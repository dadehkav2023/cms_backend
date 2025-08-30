using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Map.Request;
using Application.ViewModels.Map.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Enum;

namespace Application.Services.Map
{
    public class MapService : IMapService
    {
        private readonly IRepository<Domain.Entities.Map.Map> _mapRepository;
        private readonly IMapper _mapper;

        public MapService(IUnitOfWorkMap unitOfWorkMap, IMapper mapper)
        {
            _mapRepository = unitOfWorkMap.GetRepository<Domain.Entities.Map.Map>();
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> SetProvinceMap(
            RequestSetProvinceMapViewModel requestSetProvinceMapViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var currentProvinceMap = _mapRepository
                    .DeferredWhere(p => p.Province == requestSetProvinceMapViewModel.Province).FirstOrDefault();

                if (currentProvinceMap == null)
                {
                    var newProvinceMap = _mapper.Map<Domain.Entities.Map.Map>(requestSetProvinceMapViewModel);
                    await _mapRepository.AddAsync(newProvinceMap);
                }
                else
                {
                    var mapProvince = _mapper.Map(requestSetProvinceMapViewModel, currentProvinceMap);
                    await _mapRepository.UpdateAsync(mapProvince, true);
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

        public async Task<IBusinessLogicResult<List<ResponseGetProvinceViewModel>>> GetProvinceList()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var provinces = Enum.GetValues<ProvinceEnum>().Select(x => new ResponseGetProvinceViewModel
                {
                    Province = x
                }).ToList();

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<ResponseGetProvinceViewModel>>(succeeded: true, result: provinces,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<ResponseGetProvinceViewModel>>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<List<ResponseGetProvinceMapViewModel>>> GetProvinceMap()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var mapProvince = _mapRepository.DeferdSelectAll();
                
                //todo use mapper instead of this
                var result = mapProvince.Select(x => new ResponseGetProvinceMapViewModel
                {
                    Description = x.Description,
                    Province = new ResponseGetProvinceViewModel
                    {
                        Province = x.Province
                    },
                    WebsiteAddress = x.WebsiteAddress
                }).ToList();

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<ResponseGetProvinceMapViewModel>>(succeeded: true,
                    result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<ResponseGetProvinceMapViewModel>>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }
    }
}