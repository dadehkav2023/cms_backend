using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.TextNews.Request;
using Application.ViewModels.News.TextNews.Response;

namespace Application.Services.News.TextNews
{
    public interface ITextNewsService
    {
        Task<IBusinessLogicResult<bool>> NewNews(
            RequestNewTextNewsViewModel requestNewTextNewsViewModel);

        Task<IBusinessLogicResult<bool>> EditNews(
            RequestEditTextNewsViewModel requestEditTextNewsViewModel);

        Task<IBusinessLogicResult<ResponseGetTextNewsListViewModel>> GetNews(
            RequestGetTextNewsViewModel requestGetTextNewsViewModel);

        Task<IBusinessLogicResult<bool>> DeleteNews(
            int newsId);
    }
}