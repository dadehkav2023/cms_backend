using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.ImageGallery.Request
{
    public class RequestEditGalleryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان اجباری می باشد")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان 5 کاراکتر می باشد")]
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool IsActive { get; set; } = true;
    }
}