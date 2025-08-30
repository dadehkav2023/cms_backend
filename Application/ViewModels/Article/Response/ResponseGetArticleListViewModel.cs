using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.Article.Response
{
    public class ResponseGetArticleListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetArticleViewModel> ArticleList { get; set; }
    }

    public class ResponseGetArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        
    }
}