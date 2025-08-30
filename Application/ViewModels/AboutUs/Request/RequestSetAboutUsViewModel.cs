using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.AboutUs.Request
{
    public class RequestSetAboutUsViewModel
    {
        public string Title { get; set; }
        public IFormFile HeaderImage { get; set; }
        public string AboutUsText { get; set; }
        public string Email { get; set; }
    }
}