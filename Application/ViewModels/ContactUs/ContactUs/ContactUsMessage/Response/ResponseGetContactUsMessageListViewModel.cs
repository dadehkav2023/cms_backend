using System.Collections.Generic;
using Application.ViewModels.ContactUs.ContactUs.Response;
using Application.ViewModels.Public;

namespace Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Response
{
    public class ResponseGetContactUsMessageListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetContactUsMessageViewModel> ContactUseMessageList { get; set; }
    }

    public class ResponseGetContactUsMessageViewModel
    {
        public int? UserId { get; set; }
        public string FirstNameAndLastName { get; set; }
        public string Email { get; set; }

        public string TextMessage { get; set; }
    }
}