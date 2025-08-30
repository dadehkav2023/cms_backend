using System.Collections.Generic;
using Application.ViewModels.Public;
using Common.Enum;

namespace Application.ViewModels.News.PhotoNews.Request
{
    public class RequestGetPhotoNewsViewModel : RequestGetListViewModel
    {
        public int? Priority { get; set; }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool? IsActive { get; set; }
        public bool? ShowInMainPage { get; set; }

        public bool LoadPublishedNews { get; set; } = true;
        
       public List<int>? CategoryIds { get; set; }
       public string StartDateTime { get; set; }        
       public string EndDateTime { get; set; }     

    }
}