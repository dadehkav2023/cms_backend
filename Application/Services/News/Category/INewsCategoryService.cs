using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.Category.Request;
using Application.ViewModels.News.Category.Response;

namespace Application.Services.News.Category
{
    public interface INewsCategoryService
    {
        Task<IBusinessLogicResult<bool>> NewNewsCategory(
            RequestNewNewsCategoryViewModel requestNewNewsCategoryViewModel);

        Task<IBusinessLogicResult<bool>> EditNewsCategory(
            RequestEditNewsCategoryViewModel requestEditNewsCategoryViewModel);

        Task<IBusinessLogicResult<List<ResponseGetNewsCategoryViewModel>>>
            GetAllNewsCategoryList();

        Task<IBusinessLogicResult<bool>> DeleteNewsCategory(
            int newsCategoryId);
    }
}