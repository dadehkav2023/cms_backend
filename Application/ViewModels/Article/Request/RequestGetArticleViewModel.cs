using Application.ViewModels.Public;

namespace Application.ViewModels.Article.Request
{
    public class RequestGetArticleViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public int? PublishYear { get; set; }

    }
}