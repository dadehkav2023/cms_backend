using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.AboutUs.Request;
using Application.ViewModels.AboutUs.Response;

namespace Application.Services.AboutUs
{
    public interface IAboutUsService
    {
        Task<BusinessLogicResult<bool>> SetAboutUs(RequestSetAboutUsViewModel requestSetAboutUsViewModel);
        Task<BusinessLogicResult<ResponseGetAboutUs>> GetAboutUs();
    }
}