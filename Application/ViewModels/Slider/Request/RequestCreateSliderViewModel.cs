using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Slider.Request
{
    public class RequestCreateSliderViewModel
    {
        [Required(ErrorMessage = "عنوان اسلایدر را وارد کنید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "تصویر اسلایدر را انتخاب کنید")]
        public IFormFile SliderFile { get; set; }
        public string Description { get; set; }
        public string LinkAddress { get; set; }
        [Required(ErrorMessage = "ترتیب نمایش را مشخص کنید")]
        public int SortOrder { get; set; }
        public bool CanShow { get; set; } = true;
        public string StartDateTimeShow { get; set; }
        public string EndDateTimeShow { get; set; }

    }
}