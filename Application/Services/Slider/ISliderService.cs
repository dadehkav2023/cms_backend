using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.CMS.Setting.Request;
using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using Application.ViewModels.Slider.Response;

namespace Application.Services.Slider
{
    public interface ISliderService
    {
        Task<IBusinessLogicResult<bool>> CreateNewSlider(RequestCreateSliderViewModel requestCreateSliderViewModel, int userId);
        Task<IBusinessLogicResult<bool>> EditSlider(RequestEditSliderViewModel requestEditSliderViewModel, int userId);
        Task<IBusinessLogicResult<ResponseGetSliderListViewModel>> GetSlider(RequestGetSliderListViewModel requestGetSliderListViewModel, int userId);
        Task<IBusinessLogicResult<bool>> DeleteSlider(int sliderId, int userId);
    }
}