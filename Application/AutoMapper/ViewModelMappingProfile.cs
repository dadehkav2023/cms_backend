using System.Collections.Generic;
using Application.ViewModels.CMS.Setting.Request;
using AutoMapper;
using Domain.Entities.CMS.Setting;
using Application.ViewModels.CMS.Setting.Response;
using Common.Enum;

namespace Application.AutoMapper
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            CreateMap<RequestSetSettingViewModel, Setting>().ReverseMap();
            CreateMap<ResponseGetSettingViewModel, Setting>().ReverseMap();

            //CreateMap<AddCityViewModel, AddCityCommand>()
            //    .ConstructUsing(p => new AddCityCommand(p.CityeName, p.StateId)).ReverseMap();
        }
    }
}
