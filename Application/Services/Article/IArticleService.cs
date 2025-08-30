using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Article.Request;
using Application.ViewModels.Article.Response;

namespace Application.Services.Article
{
    public interface IArticleService
    {
        Task<IBusinessLogicResult<bool>> NewArticle(
            RequestNewArticleViewModel requestNewArticleViewModel);
        
        Task<IBusinessLogicResult<bool>> EditArticle(
            RequestEditArticleViewModel requestEditArticleViewModel);
        
        Task<IBusinessLogicResult<ResponseGetArticleListViewModel>> GetArticleList(
            RequestGetArticleViewModel requestGetArticleViewModel);
        
        Task<IBusinessLogicResult<bool>> DeleteArticle(
            int articleId);
    }
}