using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Utilities;
using Application.ViewModels.News.Category.Response;
using Application.ViewModels.Public;

namespace Application.ViewModels.News.VideoNews.Response
{
    public class ResponseGetVideoNewsListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetVideoNewsViewModel> NewsList { get; set; }
    }

    public class ResponseGetVideoNewsViewModel
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Summary { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore] public DateTime? PublishedDateTime { get; set; }
        public string PublishedDateTimeAsJalali => PublishedDateTime.ConvertMiladiToJalali();

        public IEnumerable<ResponseGetNewsCategoryViewModel> NewsCategories { get; set; }
        
        
    }
}