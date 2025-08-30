using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.RelatedLink.Request;
using Application.ViewModels.RelatedLink.Response;

namespace Application.Services.RelatedLink
{
    public interface IRelatedLinkService
    {
        Task<IBusinessLogicResult<bool>> NewRelatedLink(RequestNewRelatedLinkViewModel requestNewRelatedLinkViewModel);
        Task<IBusinessLogicResult<bool>> EditRelatedLink(RequestEditRelatedLinkViewModel requestEditRelatedLinkViewModel);
        Task<IBusinessLogicResult<ResponseGetRelatedLinkListViewModel>> GetRelatedLinkList(RequestGetRelatedLinkViewModel requestGetRelatedLinkViewModel);
        Task<IBusinessLogicResult<bool>> DeleteRelatedLink(int relatedLinkId);
    }
}