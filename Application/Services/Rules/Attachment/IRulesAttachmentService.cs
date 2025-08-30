using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Rules.Attachment.Request;
using Application.ViewModels.Rules.Attachment.Response;

namespace Application.Services.Rules.Attachment
{
    public interface IRulesAttachmentService
    {
        Task<IBusinessLogicResult<bool>> NewRulesAttachment(
            RequestNewRulesAttachmentViewModel requestNewRulesAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> EditRulesAttachment(
            RequestEditRulesAttachmentViewModel requestEditRulesAttachmentViewModel);

        Task<IBusinessLogicResult<ResponseGetRulesAttachmentListViewModel>>
            GetRulesAttachmentList(RequestGetRulesAttachmentViewModel requestGetRulesAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> DeleteRulesAttachment(
            int rulesAttachmentId);
    }
}