using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.ImageGallery.Request;
using Application.ViewModels.ImageGallery.Response;

namespace Application.Services.ImageGallery
{
    public interface IImageGalleryService
    {
        Task<IBusinessLogicResult<bool>> NewGallery(RequestNewGalleryViewModel requestNewGalleryViewModel, int userId);
        Task<IBusinessLogicResult<bool>> EditGallery(RequestEditGalleryViewModel requestEditGalleryViewModel, int userId);
        Task<IBusinessLogicResult<ResponseGetGalleryListViewModel>> GetGallery(RequestGetGalleryViewModel requestGetGalleryViewModel, int userId);
        Task<IBusinessLogicResult<bool>> RemoveGallery(int galleryId, int userId);
    }
}