using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Notification.Notification.Request
{
    public class RequestEditNotificationViewModel
    {
        [Required(ErrorMessage = "آی دی را وارد کنید")]
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان 5 کاراکتر می باشد")]
        public string Title { get; set; }
        public IFormFile ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}