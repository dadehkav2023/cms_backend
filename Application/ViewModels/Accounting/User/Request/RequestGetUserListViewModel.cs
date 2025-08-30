using Application.ViewModels.Public;

namespace Application.ViewModels.Accounting.User.Request
{
    public class RequestGetUserListViewModel : RequestGetListViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
    }
}