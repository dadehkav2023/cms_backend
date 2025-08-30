using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.ServiceDesk.Request
{
    public class RequestEditServiceDeskViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان 5 کاراکتر می باشد")]
        public string Title { get; set; }
        public IFormFile ImageFile { get; set; }        
        public string LinkService { get; set; }
        public bool IsActive { get; set; } = true;
    }
}