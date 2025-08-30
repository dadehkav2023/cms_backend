using Application.BusinessLogic;
using Application.Services.CMS.Setting;
using Application.ViewModels.CMS.Setting.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.CMS.Identity;
using Application.Services.CMS.Identity.Role;
using Application.ViewModels.CMS.Identity.Request;
using Domain.Entities.Identity.Role;

namespace CMS.Admin.Controllers.CMS
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IRoleService _roleService;

        public IdentityController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles="Admin")]
        [HttpPost("SetRoleToUser")]
        public async Task<IActionResult> SetRoleToUser(
            [FromBody] RequestAddRolesToUserViewModel requestAddRolesToUserViewModel)
        {
            return (await _roleService.SetRoleToUser(requestAddRolesToUserViewModel)).ToWebApiResult().ToHttpResponse();
        }

        [Authorize(Roles="Admin")]
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(
            [FromBody] RequestRemoveRolesFromUserViewModel requestRemoveRolesFromUserViewModel)
        {
            return (await _roleService.RemoveRoleFromUser(requestRemoveRolesFromUserViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles="Admin")]
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles(int? userId)
        {
            return (await _roleService.GetAllRoles(userId)).ToWebApiResult().ToHttpResponse();
        }

        [Authorize(Roles="Admin")]
        [HttpGet("GetRolesOfUser")]
        public async Task<IActionResult> GetRolesOfUser(int userId)
        {
            return (await _roleService.GetRolesOfUser(userId)).ToWebApiResult().ToHttpResponse();
        }
    }
}