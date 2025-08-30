using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CMS.Identity.User
{
    public class TokenViewModel
    {
        public TokenViewModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; protected set; }
        public string RefreshToken { get; protected set; }
    }
}
