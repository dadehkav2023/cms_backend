using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Public;
using Common.EnumList;
using Domain.Entities.Store;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<County> _countyRepository;
        private readonly IRepository<CityOrVillage> _cityOrVillageRepository;

        public LocationService(IUnitOfWork uow)
        {
            _provinceRepository = uow.GetRepository<Province>();
            _countyRepository = uow.GetRepository<County>();
            _cityOrVillageRepository = uow.GetRepository<CityOrVillage>();
        }

        public async Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllProvincesAsync(CancellationToken cancellationToken)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var provinces = await _provinceRepository
                    .DeferredSelectAllNoTracking()
                    .Select(province => new SelectOptionViewModel { Title = province.Title, Id = province.Id })
                    .ToListAsync(cancellationToken);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: true, result: provinces, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: false, result: null, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllCountiesByProvinceIdAsync(int provinceId, CancellationToken cancellationToken)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var counties = await _countyRepository
                    .DeferredSelectAllNoTracking()
                    .Where(county => county.ProvinceId == provinceId)
                    .Select(county => new SelectOptionViewModel { Title = county.Title, Id = county.Id })
                    .ToListAsync(cancellationToken);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: true, result: counties, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: false, result: null, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllRuralsByCountyIdAsync(int countyId, CancellationToken cancellationToken)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                var rurals = await _cityOrVillageRepository
                    .DeferredWhereAsNoTracking(cov => cov.Part.CountyId == countyId)
                    .Where(cov => cov.LocationType == LocationTypeEnum.RuralDistrict)
                    .Select(cov => new SelectOptionViewModel { Id = cov.Id, Title = cov.Title })
                    .ToListAsync(cancellationToken);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: true, result: rurals, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: false, result: null, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllCitiesByCountyIdAsync(int countyId, CancellationToken cancellationToken)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                var cities = await _cityOrVillageRepository
                    .DeferredWhereAsNoTracking(cov => cov.Part.CountyId == countyId)
                    .Where(cov => cov.LocationType == LocationTypeEnum.City)
                    .Select(city => new SelectOptionViewModel { Title = city.Title, Id = city.Id })
                    .ToListAsync(cancellationToken);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: true, result: cities, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: false, result: null, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllVillagesByCountyIdAsync(int countyId, CancellationToken cancellationToken)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                var villages = await _cityOrVillageRepository
                    .DeferredWhereAsNoTracking(cov => cov.Part.CountyId == countyId)
                    .Where(cov => cov.LocationType == LocationTypeEnum.Village)
                    .Select(village => new SelectOptionViewModel { Title = village.Title, Id = village.Id })
                    .ToListAsync(cancellationToken);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: true, result: villages, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<SelectOptionViewModel>>(succeeded: false, result: null, messages: messages, exception: exception);
            }
        }
    }
}
