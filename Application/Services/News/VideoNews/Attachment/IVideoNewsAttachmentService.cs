using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Attachment.Response;

namespace Application.Services.News.VideoNews.Attachment
{
    public interface IVideoNewsAttachmentService
    {
        Task<IBusinessLogicResult<bool>> NewNewsAttachment(
            RequestNewVideoNewsAttachmentViewModel requestNewVideoNewsAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> EditNewsAttachment(
            RequestEditVideoNewsAttachmentViewModel requestEditVideoNewsAttachmentViewModel);

        Task<IBusinessLogicResult<ResponseGetVideoNewsAttachmentListViewModel>>
            GetNewsAttachmentList(RequestGetVideoNewsAttachmentViewModel requestGetVideoNewsAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> DeleteNewsAttachment(
            int newsAttachmentId);
    }
}