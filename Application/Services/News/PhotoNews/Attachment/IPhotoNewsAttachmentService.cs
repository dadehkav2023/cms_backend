using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.PhotoNews.Attachment.Response;

namespace Application.Services.News.PhotoNews.Attachment
{
    public interface IPhotoNewsAttachmentService
    {
        Task<IBusinessLogicResult<bool>> NewNewsAttachment(
            RequestNewPhotoNewsAttachmentViewModel requestNewPhotoNewsAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> EditNewsAttachment(
            RequestEditPhotoNewsAttachmentViewModel requestEditPhotoNewsAttachmentViewModel);

        Task<IBusinessLogicResult<ResponseGetPhotoNewsAttachmentListViewModel>>
            GetNewsAttachmentList(RequestGetPhotoNewsAttachmentViewModel requestGetPhotoNewsAttachmentViewModel);

        Task<IBusinessLogicResult<bool>> DeleteNewsAttachment(
            int newsAttachmentId);
    }
}