using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Utilities;
using Application.ViewModels.News.Category.Response;
using Application.ViewModels.Public;
using Common.Enum;
using Common.EnumList;

namespace Application.ViewModels.News.TextNews.Response
{
    public class ResponseGetTextNewsListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetTextNewsViewModel> NewsList { get; set; }
    }

    public class ResponseGetTextNewsViewModel
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public string HeadTitle { get; set; }
        public string SubTitle { get; set; }
        public string SummaryTitle { get; set; }
        public string Lead { get; set; }
        public string Content { get; set; }
        public NewsTypeEnum NewsType { get; set; }
        public string NewsTypeTest => NewsType.GetDescription();
        public NewsPriority NewsPriority { get; set; }
        public string NewsPriorityText => NewsPriority.GetDescription();
        [JsonIgnore] public DateTime? PublishedDateTime { get; set; }
        public string PublishedDateTimeAsJalali => PublishedDateTime.ConvertMiladiToJalali();
        public IEnumerable<ResponseGetNewsCategoryViewModel> NewsCategories { get; set; }
    }
}