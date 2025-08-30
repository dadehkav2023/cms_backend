using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.ContactUs.ContactUs.Request;
using Application.ViewModels.ContactUs.ContactUs.Response;

namespace Application.Services.ContactUs
{
    public interface IContactUsService
    {
        Task<BusinessLogicResult<bool>> SetContactUs(RequestSetContactUsViewModel requestSetContactUsViewModel);
        Task<BusinessLogicResult<ResponseGetContactUs>> GetContactUs();
    }
}