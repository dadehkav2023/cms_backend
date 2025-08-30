using System.Threading.Tasks;
using Application.Services.News;
using Application.Services.News.Attachment;
using Application.Services.News.Category;
using Application.Services.News.PhotoNews;
using Application.Services.News.TextNews;
using Application.Services.News.TextNews.Attachment;
using Application.ViewModels.News.Category.Request;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.Request;
using Application.ViewModels.News.TextNews.Request;
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.News
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly INewsCategoryService _newsCategoryService;

        public NewsController(INewsService newsService, INewsCategoryService newsCategoryService)
        {
            _newsService = newsService;
            _newsCategoryService = newsCategoryService;
        }

        //[Authorize]
        [HttpPost("NewNews")]
        public async Task<IActionResult> NewNews(
            [FromForm] RequestNewNewsViewModel requestNewNewsViewModel)
        {
            return (await _newsService.NewNews(requestNewNewsViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("EditNews")]
        public async Task<IActionResult> EditNews(
            [FromForm] RequestEditNewsViewModel requestEditNewsViewModel)
        {
            return (await _newsService.EditNews(requestEditNewsViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpDelete("DeleteNews")]
        public async Task<IActionResult> DeleteNews(
            [FromForm] RequestDeleteNewsViewModel requestDeleteNewsViewModel)
        {
            return (await _newsService.DeleteNews(requestDeleteNewsViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        


        #region Category

        //[Authorize]
        [HttpPost("Category/NewCategory")]
        public async Task<IActionResult> NewNewsCategory(
            [FromForm] RequestNewNewsCategoryViewModel requestNewNewsCategoryViewModel)
        {
            return (await _newsCategoryService.NewNewsCategory(
                    requestNewNewsCategoryViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("Category/EditCategory")]
        public async Task<IActionResult> EditNewsCategory(
            [FromForm] RequestEditNewsCategoryViewModel requestEditNewsCategoryViewModel)
        {
            return (await _newsCategoryService.EditNewsCategory(
                    requestEditNewsCategoryViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpGet("Category/GetCategory")]
        public async Task<IActionResult> GetNewsCategory()
        {
            return (await _newsCategoryService.GetAllNewsCategoryList())
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpDelete("Category/DeleteCategory")]
        public async Task<IActionResult> DeleteNewsCategory(
            [FromForm] int newsCategoryId)
        {
            return (await _newsCategoryService.DeleteNewsCategory(
                    newsCategoryId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        #endregion
    }
}