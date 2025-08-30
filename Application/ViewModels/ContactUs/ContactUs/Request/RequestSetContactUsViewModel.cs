using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.ContactUs.ContactUs.Request
{
    public class RequestSetContactUsViewModel
    {
        public string Title { get; set; }
        public IFormFile HeaderImage { get; set; }
        [Required(ErrorMessage = "متن تماس با را وارد کنید")]
        public string ContactUsText { get; set; }
        public string Email { get; set; }
    }
}