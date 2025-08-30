using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Statement.Attachment.Request;
using Application.ViewModels.Statement.Attachment.Response;

namespace Application.Services.Statement.Attachment
{
    public interface IStatementAttachmentService
    {
        Task<IBusinessLogicResult<bool>> NewStatementAttachment(
            RequestNewStatementAttachmentViewModel requestNewStatementAttachmentViewModel, int userId);

        Task<IBusinessLogicResult<bool>> EditStatementAttachment(
            RequestEditStatementAttachmentViewModel requestEditStatementAttachmentViewModel, int userId);

        Task<IBusinessLogicResult<ResponseGetStatementAttachmentListViewModel>>
            GetStatementAttachmentList(RequestGetStatementAttachmentViewModel requestGetStatementAttachmentViewModel, int userId);

        Task<IBusinessLogicResult<bool>> DeleteStatementAttachment(
            int statementAttachmentId, int userId);
    }
}