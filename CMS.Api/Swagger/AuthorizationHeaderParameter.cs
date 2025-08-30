using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CMS.Api.Swagger
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ////var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            ////var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            ////var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

            ////if (isAuthorized && !allowAnonymous)
            ////{
            //if (operation.Parameters == null)
            //    operation.Parameters = new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "AuthorizationToken",
            //    In = ParameterLocation.Header,
            //    Description = "access token",
            //    Required = false,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "String",
            //        Default = new OpenApiString("Bearer ")
            //    }
            //});
            ////}
            ///

            var hasAuthorize =
                      context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                      || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme {Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"}
                            }
                        ] = new[] {"api1"}
                    }
                };
            }
        }
    }
}