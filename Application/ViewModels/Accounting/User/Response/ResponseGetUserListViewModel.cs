using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.Accounting.User.Response
{
    public class ResponseGetUserListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetUserViewModel> List { get; set; }
    }

    public class ResponseGetUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}