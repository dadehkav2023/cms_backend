using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Admin.Controllers.AccountIdentity
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
