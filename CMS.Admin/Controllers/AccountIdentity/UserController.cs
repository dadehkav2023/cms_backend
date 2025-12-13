// using System.Threading.Tasks;
// using Application.Services.Accounting;
// using Application.Services.CMS.Identity.User;
// using Application.ViewModels.Accounting.Request;
// using Application.ViewModels.Accounting.User.Request;
// using CMS.Admin.Helper.Response;
// using Microsoft.AspNetCore.Mvc;
//
// namespace CMS.Admin.Controllers.AccountIdentity
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserController : Controller
//     {
//         private readonly IUserService _userService;
//         private readonly IRegisterService _registerService;
//
//         public UserController(IUserService userService, IRegisterService registerService)
//         {
//             _userService = userService;
//             _registerService = registerService;
//         }
//
//         // [HttpPost("GetUser")]
//         // public async Task<IActionResult> GetUser([FromBody] RequestGetUserListViewModel requestGetUserListViewModel)
//         // {
//         //     return (await _userService.GetUser(requestGetUserListViewModel)).ToWebApiResult().ToHttpResponse();
//         // }
//
//         // [HttpPost("Register")]
//         // public async Task<IActionResult> Register(
//         //     [FromBody] RequestRegisterViewModel requestRegisterViewModel)
//         // {
//         //     return (await _registerService.Register(requestRegisterViewModel)).ToWebApiResult()
//         //         .ToHttpResponse();
//         // }
//     }
// }