using System;
using System.Collections.Generic;
using Application.ViewModels.Public;
using Common.Enum;

namespace Application.ViewModels.News.TextNews.Request
{
    public class RequestGetTextNewsViewModel : RequestGetListViewModel
    {
        public int? Priority { get; set; }
        public int? Id { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public bool? ShowInMainPage { get; set; }
        public string HeadTitle { get; set; }
        public string SubTitle { get; set; }
        public string SummaryTitle { get; set; }
        public string Lead { get; set; }
        public string Content { get; set; }
        public NewsTypeEnum? NewsType { get; set; }
        public NewsPriority? NewsPriority { get; set; }
        public bool LoadPublishedDate { get; set; }
        public List<int>? CategoryIds { get; set; }
        public string StartDateTime { get; set; }        
        public string EndDateTime { get; set; }     
        
    }
}