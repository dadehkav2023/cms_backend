using Common.Enum.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting.User.Request
{
    public class RegisterUserViewModel
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string SecurityStamp { get; set; }
    }

    public class RegisterUserRealViewModel : RegisterUserViewModel
    {
        public string LastName { get; set; }
    }

    public class RegisterUserLegalViewModel : RegisterUserViewModel
    {
        public UserLegalType CompanyType { get; set; }
    }
}
