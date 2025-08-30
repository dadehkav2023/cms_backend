using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Utilities;
using Application.ViewModels.Public;

namespace Application.ViewModels.Slider.Response
{
    public class ResponseGetSliderListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetSliderViewModel> SliderList { get; set; }
    }

    public class ResponseGetSliderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string LinkAddress { get; set; }
        public int SortOrder { get; set; }
        public bool CanShow { get; set; }
        [JsonIgnore]
        public DateTime? StartDateTimeShow { get; set; }
        [JsonIgnore]
        public DateTime? EndDateTimeShow { get; set; }

        public string? StartDateTime => StartDateTimeShow != null ? StartDateTimeShow.ConvertMiladiToJalali() : null;

        public string? EndDateTime => EndDateTimeShow != null ? EndDateTimeShow.ConvertMiladiToJalali() : null;
    }
}