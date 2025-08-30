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
using CMS.Api.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.News
{
    [Route("api/[controller]")]
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


        #region Category

        //[Authorize]
        [HttpGet("Category/GetCategory")]
        public async Task<IActionResult> GetNewsCategory()
        {
            return (await _newsCategoryService.GetAllNewsCategoryList())
                .ToWebApiResult()
                .ToHttpResponse();
        }
       
        #endregion
    }
}