using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Slider.Request
{
    public class RequestGetFrontSliderViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile SliderFile { get; set; }
        public string LinkAddress { get; set; }
        public bool CanShow { get; set; } = true;
        public DateTime? StartDateTimeShow { get; set; } = null;
        public DateTime? EndDateTimeShow { get; set; } = null;

    }
}