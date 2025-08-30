using System.Threading.Tasks;
using Application.Services.RelatedLink;
using Application.ViewModels.RelatedLink.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.RelatedLink
{
    [Microsoft.AspNetCore.Components.Route("api/admin/[controller]")]
    [ApiController]
    public class RelatedLinkController
    {
        private readonly IRelatedLinkService _relatedLinkService;

        public RelatedLinkController(IRelatedLinkService relatedLinkService)
        {
            _relatedLinkService = relatedLinkService;
        }

        //[Authorize]
        [HttpPost("NewRelatedLink")]
        public async Task<IActionResult> NewRelatedLink(
            [FromBody] RequestNewRelatedLinkViewModel requestNewRelatedLinkViewModel)
        {
            return (await _relatedLinkService.NewRelatedLink(requestNewRelatedLinkViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("EditRelatedLink")]
        public async Task<IActionResult> EditRelatedLink(
            [FromBody] RequestEditRelatedLinkViewModel requestEditRelatedLinkViewModel)
        {
            return (await _relatedLinkService.EditRelatedLink(requestEditRelatedLinkViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("GetRelatedLink")]
        public async Task<IActionResult> GetRelatedLink(
            [FromBody] RequestGetRelatedLinkViewModel requestGetRelatedLinkViewModel)
        {
            return (await _relatedLinkService.GetRelatedLinkList(requestGetRelatedLinkViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpDelete("DeleteRelatedLink")]
        public async Task<IActionResult> DeleteRelatedLink(
            [FromForm] int relatedLinkId)
        {
            return (await _relatedLinkService.DeleteRelatedLink(relatedLinkId)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}