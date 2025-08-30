using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.PhotoNews.Response;

namespace Application.Services.News.PhotoNews
{
    public interface IPhotoNewsService
    {
        Task<IBusinessLogicResult<bool>> NewPhotoNews(
            RequestNewPhotoNewsViewModel requestNewPhotoNewsViewModel);
        
        Task<IBusinessLogicResult<bool>> EditPhotoNews(
            RequestEditPhotoNewsViewModel requestEditPhotoNewsViewModel);

        Task<IBusinessLogicResult<ResponseGetPhotoNewsListViewModel>> GetPhotoNews(
        RequestGetPhotoNewsViewModel requestGetPhotoNewsViewModel);
        
        Task<IBusinessLogicResult<bool>> DeletePhotoNews(
        int newsId);
    }
}