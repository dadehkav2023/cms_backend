using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Statement.Category.Request;
using Application.ViewModels.Statement.Category.Response;

namespace Application.Services.Statement.Category
{
    public interface IStatementCategoryService
    {
        Task<IBusinessLogicResult<bool>> NewStatementCategory(
            RequestNewStatementCategoryViewModel requestNewStatementCategoryViewModel);

        Task<IBusinessLogicResult<bool>> EditStatementCategory(
            RequestEditStatementCategoryViewModel requestEditStatementCategoryViewModel);

        Task<IBusinessLogicResult<List<ResponseGetStatementCategoryViewModel>>>
            GetAllStatementCategoryList();

        Task<IBusinessLogicResult<bool>> DeleteStatementCategory(
            int statementCategoryId);
    }
}