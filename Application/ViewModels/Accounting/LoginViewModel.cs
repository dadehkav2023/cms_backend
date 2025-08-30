using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsPersistent { get; set; }
    }
}
