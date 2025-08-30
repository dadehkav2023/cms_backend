using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Helper.Response
{
    public static class ResponseExtention
    {
        public static IActionResult ToHttpResponse(this WebApiResult data)
        {
           
            switch (data.HttpStatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(data);
                case HttpStatusCode.BadRequest:
                default:
                    return new BadRequestObjectResult(data);
            }
        }

    }
}
