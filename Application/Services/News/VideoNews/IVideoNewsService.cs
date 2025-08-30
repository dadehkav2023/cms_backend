using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.VideoNews.Request;
using Application.ViewModels.News.VideoNews.Response;

namespace Application.Services.News.VideoNews
{
    public interface IVideoNewsService
    {
        Task<IBusinessLogicResult<bool>> NewVideoNews(
            RequestNewVideoNewsViewModel requestNewVideoNewsViewModel);
        
        Task<IBusinessLogicResult<bool>> EditVideoNews(
            RequestEditVideoNewsViewModel requestEditVideoNewsViewModel);

        Task<IBusinessLogicResult<ResponseGetVideoNewsListViewModel>> GetVideoNews(
        RequestGetVideoNewsViewModel requestGetVideoNewsViewModel);
        
        Task<IBusinessLogicResult<bool>> DeleteVideoNews(
        int newsId);
    }
}