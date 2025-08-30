using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Notification.Notification.Response;
using Application.ViewModels.Statement.Request;
using Application.ViewModels.Statement.Response;

namespace Application.Services.Statement
{
    public interface IStatementService
    {
        Task<IBusinessLogicResult<bool>> NewStatement(
            RequestNewStatementViewModel requestNewStatementViewModel, int userId);
        
        Task<IBusinessLogicResult<bool>> EditStatement(
            RequestEditStatementViewModel requestEditStatementViewModel, int userId);
        
        Task<IBusinessLogicResult<ResponseGetStatementListViewModel>> GetStatementList(
            RequestGetStatementViewModel requestGetStatementViewModel, int userId);
        
        Task<IBusinessLogicResult<bool>> DeleteStatement(
            int statementId, int userId);
    }
}