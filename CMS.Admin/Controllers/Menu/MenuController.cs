using Application.Services.Menu;
using Application.ViewModels.Menu.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Admin.Controllers.Menu
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }
        [Authorize(Roles = nameof(RoleEnum.Menu))]
        [HttpPost("AddMenu")]
        public async Task<IActionResult> AddMenu([FromForm] MenuViewModelRequest request)
        {
            return (await menuService.AddMenu(request)).ToWebApiResult().ToHttpResponse();
        }
        [Authorize(Roles = nameof(RoleEnum.Menu))]
        [HttpPost("AddMenuItem")]
        public async Task<IActionResult> AddMenuItem([FromForm] MenuItemViewModelRequest request)
        {
            return (await menuService.AddMenuItem(request)).ToWebApiResult().ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Menu))]
        [HttpGet("GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            var result = (await menuService.GetMenu(depth: 1)).ToWebApiResult().ToHttpResponse();
            return result;
        }
        [HttpGet("GetMenuById")]
        public async Task<IActionResult> GetMenuById(int Id)
        {
            var result = (await menuService.GetMenuById(Id)).ToWebApiResult().ToHttpResponse();
            return result;
        }
         [HttpGet("GetRootMenuById")]
        public async Task<IActionResult> GetRootMenuById(int Id)
        {
            var result = (await menuService.GetRootMenuById(Id)).ToWebApiResult().ToHttpResponse();
            return result;
        }

        [Authorize(Roles = nameof(RoleEnum.Menu))]
        [HttpPut("EditMenu")]
        public async Task<IActionResult> EditMenu([FromForm] MenuViewModelRequest request)
        {
            return (await menuService.EditMenu(request)).ToWebApiResult().ToHttpResponse();
        }

        [HttpPut("EditMenuItem")]
        [Authorize(Roles = nameof(RoleEnum.Menu))]
        public async Task<IActionResult> EditMenuItem([FromForm] MenuItemViewModelRequest request)
        {
            return (await menuService.EditMenuItem(request)).ToWebApiResult().ToHttpResponse();
        }

        [HttpDelete("RemoveMenu")]
        [Authorize(Roles = nameof(RoleEnum.Menu))]
        public async Task<IActionResult> RemoveMenu([FromForm] RemoveMenuRequest removemenu)
        {
            return (await menuService.RemoveMenu(removemenu.Id)).ToWebApiResult().ToHttpResponse();
        }
    }
}
