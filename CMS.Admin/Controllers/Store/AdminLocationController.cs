using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Location;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Store
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    public class AdminLocationController : Controller
    {
        private readonly ILocationService _locationService;

        public AdminLocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProvinces(CancellationToken cancellationToken)
        {
            return (await _locationService.GetAllProvincesAsync(cancellationToken)).ToWebApiResult().ToHttpResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCountiesByProvinceId(int provinceId, CancellationToken cancellationToken)
        {
            return (await _locationService.GetAllCountiesByProvinceIdAsync(provinceId, cancellationToken)).ToWebApiResult().ToHttpResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRuralsByCountyId(int countyId, CancellationToken cancellationToken)
        {
            return (await _locationService.GetAllRuralsByCountyIdAsync(countyId, cancellationToken)).ToWebApiResult().ToHttpResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCitiesByCountyId(int countyId, CancellationToken cancellationToken)
        {
            return (await _locationService.GetAllCitiesByCountyIdAsync(countyId, cancellationToken)).ToWebApiResult().ToHttpResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVillagesByCountyId(int countyId, CancellationToken cancellationToken)
        {
            return (await _locationService.GetAllVillagesByCountyIdAsync(countyId, cancellationToken)).ToWebApiResult().ToHttpResponse();
        }
    }
}
