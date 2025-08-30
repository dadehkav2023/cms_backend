using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.PhotoNews.Response;
using Application.ViewModels.News.Request;
using Application.ViewModels.News.TextNews.Request;
using Application.ViewModels.News.TextNews.Response;
using Common.Enum;

namespace Application.Services.News
{
    public interface INewsService
    {
        Task<IBusinessLogicResult<bool>> NewNews(
            RequestNewNewsViewModel requestNewNewsViewModel);

        Task<IBusinessLogicResult<bool>> EditNews(
            RequestEditNewsViewModel requestEditNewsViewModel);
        
        Task<IBusinessLogicResult<bool>> DeleteNews(
            RequestDeleteNewsViewModel requestDeleteNewsViewModel);
    }
}