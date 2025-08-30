using Application.ViewModels.Public;

namespace Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Request
{
    public class RequestGetContactUsMessageViewModel : RequestGetListViewModel
    {
        public int? UserId { get; set; }
        public string FirstNameAndLastName { get; set; }
        public string Email { get; set; }

        public string TextMessage { get; set; }
    }
}