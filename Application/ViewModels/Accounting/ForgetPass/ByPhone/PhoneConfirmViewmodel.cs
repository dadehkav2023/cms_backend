using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting.ForgetPass.ByPhone
{
    public class PhoneConfirmViewmodel
    {
        [Required]
        [RegularExpression(@"^09[0-9]{2}[0-9]{7}$")]
        public string PhoneNumber { get; set; }


    }
}
