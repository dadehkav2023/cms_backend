using Application.Services.Menu;
using Application.ViewModels.Menu.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Api.Controllers.Menu
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        [HttpGet("GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            var result = (await menuService.GetMenu(depth: 1)).ToWebApiResult().ToHttpResponse();
            return result;
        }

    }
}
