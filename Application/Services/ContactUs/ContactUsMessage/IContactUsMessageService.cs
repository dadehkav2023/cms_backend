using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Request;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Response;

namespace Application.Services.ContactUs.ContactUsMessage
{
    public interface IContactUsMessageService
    {
        Task<BusinessLogicResult<bool>> NewContactUsMessage(
            RequestNewContactUsMessageViewModel requestNewContactUsMessageViewModel, int? userId = null);
        
        Task<BusinessLogicResult<ResponseGetContactUsMessageListViewModel>> GetContactUsMessageList(
            RequestGetContactUsMessageViewModel requestGetContactUsMessageViewModel);
    }
}