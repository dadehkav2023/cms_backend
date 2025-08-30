using Application.Interfaces.IRepositories;
using Application.ViewModels.Accounting;
using Common.HashAlgoritm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity.Role;
using Domain.Entities.Identity.User;
using Application.Services.CMS.Identity.User;
using CMS.Admin.Helper.Response;
using Application.ViewModels.Accounting.User.Request;
using Application.ViewModels.Accounting.ForgetPass.ByPhone;
using Microsoft.AspNetCore.Authorization;
using Application.ViewModels.Accounting.ForgetPass.ByEmail;

namespace CMS.Admin.Controllers.AccountIdentity
{
    /// <summary>
    /// مدیریت کاربران اختصاصی CMS بدون SSO
    /// </summary>
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginViewModel login)
        {
            return (await userService.LoginUser(login)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddPotentialUser([FromBody] UserPotentialInViewModel model)
        {
            return (await userService.UserPotentialSaveAsync(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> UserPotentialPhoneConfirmAsync([FromBody] UserPotentialPhoneConfirmViewModel model)
        {
            return (await userService.UserPotentialPhoneConfirmAsync(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUserRealPotential([FromBody] RegisterUserRealViewModel model)
        {
            return (await userService.RegisterUserRealPotential(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUserLegalPotential([FromBody] RegisterUserLegalViewModel model)
        {
            return (await userService.RegisterUserLegalPotential(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUserReal([FromBody] RegisterUserRealViewModel model)
        {
            return (await userService.RegisterUserReal(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUserLegal([FromBody] RegisterUserLegalViewModel model)
        {
            return (await userService.RegisterUserLegal(model)).ToWebApiResult().ToHttpResponse();
        }

        //*********  Phone  *****************
        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPasswordByPhoneAsync([FromBody] PhoneConfirmViewmodel model)
        {
            return (await userService.ForgotPasswordByPhoneAsync(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPasswordByPhoneNumber([FromBody] ResetPasswordByPhonenumberViewModel model)
        {
            return (await userService.ResetPasswordByPhoneNumber(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> UserPhoneConfirmAsync([FromBody] UserPhoneConfirmViewModel model)
        {
            return (await userService.UserPhoneConfirmAsync(model)).ToWebApiResult().ToHttpResponse();
        }

        //*********  Email  *****************
        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPasswordByEmailAsync([FromBody] ForgetPasswordByEmailViewmodel model)
        {
            return (await userService.ForgotPasswordByEmailAsync(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPasswordByEmail([FromBody] ResetPasswordByEmailViewModel model)
        {
            return (await userService.ResetPasswordByEmail(model)).ToWebApiResult().ToHttpResponse();
        }

        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> UserEmailConfirmAsync([FromBody] UserEmailConfirmViewModel model)
        {
            return (await userService.UserEmailConfirmAsync(model)).ToWebApiResult().ToHttpResponse();
        }

        //*********    *****************
        [ProducesResponseType(statusCode: 200, type: typeof(bool))]
        [HttpPost("[action]")]
        //[Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            return (await userService.ChangePassword(model)).ToWebApiResult().ToHttpResponse();
        }
        
    }
}
