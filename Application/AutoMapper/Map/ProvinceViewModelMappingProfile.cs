using System.Collections.Generic;
using Application.ViewModels.Map.Request;
using Application.ViewModels.Map.Response;
using AutoMapper;
using Common.Enum;

namespace Application.AutoMapper.Map
{
    public class ProvinceViewModelMappingProfile : Profile
    {
        public ProvinceViewModelMappingProfile()
        {
            CreateMap<RequestSetProvinceMapViewModel, Domain.Entities.Map.Map>().ReverseMap();
            CreateMap<ResponseGetProvinceMapViewModel, Domain.Entities.Map.Map>().ReverseMap();
        }
    }
}