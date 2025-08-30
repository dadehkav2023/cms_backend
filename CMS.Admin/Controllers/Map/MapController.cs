using System.Threading.Tasks;
using Application.Services.Map;
using Application.ViewModels.Map.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Map
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MapController : Controller
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }
        
        //[Authorize]
        [HttpPost("SetMapProvince")]
        public async Task<IActionResult> SetMapProvince(
            [FromForm] RequestSetProvinceMapViewModel requestSetProvinceMapViewModel)
        {
            return (await _mapService.SetProvinceMap(requestSetProvinceMapViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
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