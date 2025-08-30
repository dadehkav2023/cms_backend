using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Attachment.Response;

namespace Application.Services.Article.Article
{
    public interface IArticleAttachmentService
    {
        Task<IBusinessLogicResult<bool>> NewArticleAttachment(
            RequestNewArticleAttachmentViewModel requestNewArticleAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> EditArticleAttachment(
            RequestEditArticleAttachmentViewModel requestEditArticleAttachmentViewModel);

        Task<IBusinessLogicResult<ResponseGetArticleAttachmentListViewModel>>
            GetArticleAttachmentList(RequestGetArticleAttachmentViewModel requestGetArticleAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> DeleteArticleAttachment(
            int articleAttachmentId);
    }
}