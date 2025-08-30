using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using Application.ViewModels.Slider.Response;
using AutoMapper;

namespace Application.AutoMapper.Slider
{
    public class SliderViewModelSliderMappingProfile : Profile
    {
        public SliderViewModelSliderMappingProfile()
        {
            CreateMap<RequestCreateSliderViewModel, Domain.Entities.Slider.Slider>().ReverseMap();
            CreateMap<RequestEditSliderViewModel, Domain.Entities.Slider.Slider>().ReverseMap();
            CreateMap<ResponseGetSliderViewModel, Domain.Entities.Slider.Slider>().ReverseMap();

        }
    }
}
