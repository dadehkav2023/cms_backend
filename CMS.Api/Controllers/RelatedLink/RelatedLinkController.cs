using System.Threading.Tasks;
using Application.Services.RelatedLink;
using Application.ViewModels.RelatedLink.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.RelatedLink
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class RelatedLinkController
    {
        private readonly IRelatedLinkService _relatedLinkService;

        public RelatedLinkController(IRelatedLinkService relatedLinkService)
        {
            _relatedLinkService = relatedLinkService;
        }

       
        //[Authorize]
        [HttpPost("GetRelatedLink")]
        public async Task<IActionResult> GetRelatedLink(
            [FromBody] RequestGetRelatedLinkViewModel requestGetRelatedLinkViewModel)
        {
            return (await _relatedLinkService.GetRelatedLinkList(requestGetRelatedLinkViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

      
    }
}