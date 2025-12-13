using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities.Identity.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IOC.IdentityServer4Configs
{
    public static class IdentityServer4Config
    {
        public static object RoleIdentity { get; private set; }

        public static IServiceCollection Add_SSOAPI_Config(this IServiceCollection services, string Client_Id,
            string Authority, string Audience)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = Audience; // "https://localhost:44305/resources";
                options.Authority = Authority; // "https://localhost:44305";

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        //Get the calling app client id that came from the token produced by Azure AD
                        UserManager<User> userManager =
                            context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                        ClaimsPrincipal userPrincipal = context.Principal;
                        var userName = userPrincipal.Claims.First(c => c.Type == "name").Value;

                        var findUser = userManager.FindByNameAsync(userName).Result;

                        if (findUser == null)
                        {
                            var firstName = userPrincipal.Claims.First(x => x.Type == "first_name").Value;
                            var lastName = userPrincipal.Claims.First(x => x.Type == "last_name").Value;
                            var nationalCode = userPrincipal.Claims.First(x => x.Type == "national_code").Value;
                            var phoneNumber = userPrincipal.Claims.First(x => x.Type == "phone_number").Value;
                            var newUser = new User
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                NationalCode = nationalCode,
                                PhoneNumber = phoneNumber,
                                UserName = userName,
                                Email = $"{userName}@mazagroom.ir",
                            };
                            var resultCreateNewUser = userManager.CreateAsync(newUser).Result;
                            if (!resultCreateNewUser.Succeeded)
                            {
                                context.Fail("cannot create user with user name: "+userName);
                                return Task.CompletedTask;
                            }
                            findUser = userManager.FindByNameAsync(userName).Result;
                        }

                        var UserRole = userManager.GetRolesAsync(findUser).Result;
                        var UserClaim = userManager.GetClaimsAsync(findUser).Result;

                        //if (!context.Principal.HasClaim(c => c.Value == Client_Id))
                        //{
                        //    context.Fail($"The claim client_id is not present in the token.");
                        //}

                        //Add claim if they are
                        var claims = new List<Claim>();

                        foreach (var userrole in UserRole)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, userrole));
                        }

                        var appIdentity = new ClaimsIdentity(claims);
                        foreach (var userclaim in UserClaim)
                        {
                            claims.Add(new Claim(userclaim.Type, userclaim.Value));
                        }

                        appIdentity = new ClaimsIdentity(claims);

                        context.Principal.AddIdentity(appIdentity);

                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }
    }
}