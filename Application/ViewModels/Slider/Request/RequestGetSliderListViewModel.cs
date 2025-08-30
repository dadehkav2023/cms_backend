using System;
using Application.ViewModels.Public;

namespace Application.ViewModels.Slider.Request
{
    public class RequestGetSliderListViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? CanShow { get; set; }
        public string StartShowDateTime { get; set; }
        public string EndShowDateTime { get; set; }
    }
}