using System.Threading.Tasks;
using Application.Services.Map;
using Application.ViewModels.Map.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.Map
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : Controller
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }
        
        
        //[Authorize]
        [HttpGet("GetProvince")]
        public async Task<IActionResult> GetProvince()
        {
            return (await _mapService.GetProvinceList())
                .ToWebApiResult()
                .ToHttpResponse();
        }
        
        //[Authorize]
        [HttpGet("GetProvinceMap")]
        public async Task<IActionResult> GetProvinceMap()
        {
            return (await _mapService.GetProvinceMap())
                .ToWebApiResult()
                .ToHttpResponse();
        }
    }
}