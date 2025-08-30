using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.TextNews.Attachment.Request;
using Application.ViewModels.News.TextNews.Attachment.Response;

namespace Application.Services.News.TextNews.Attachment
{
    public interface ITextNewsAttachmentService
    {
        Task<IBusinessLogicResult<bool>> NewNewsAttachment(
            RequestTextNewNewsAttachmentViewModel requestTextNewNewsAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> EditNewsAttachment(
            RequestEditTextNewsAttachmentViewModel requestEditTextNewsAttachmentViewModel);

        Task<IBusinessLogicResult<ResponseGetTextNewsAttachmentListViewModel>>
            GetNewsAttachmentList(RequestTextGetNewsAttachmentViewModel requestTextGetNewsAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> DeleteNewsAttachment(
            int newsAttachmentId);
    }
}