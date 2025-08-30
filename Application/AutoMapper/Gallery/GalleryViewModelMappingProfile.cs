using Application.ViewModels.ImageGallery.Request;
using Application.ViewModels.ImageGallery.Response;
using AutoMapper;

namespace Application.AutoMapper.Gallery
{
    public class GalleryViewModelMappingProfile : Profile
    {
        public GalleryViewModelMappingProfile()
        {
            CreateMap<RequestNewGalleryViewModel, Domain.Entities.Gallery.Gallery>().ReverseMap();
            CreateMap<RequestEditGalleryViewModel, Domain.Entities.Gallery.Gallery>().ReverseMap();
            CreateMap<ResponseGetGalleryViewModel, Domain.Entities.Gallery.Gallery>().ReverseMap();
        }
    }
}