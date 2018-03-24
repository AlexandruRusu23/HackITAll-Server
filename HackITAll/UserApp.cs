using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace HackITAll
{
    public class UserApp : ClaimsPrincipal
    {
        public UserApp(ClaimsPrincipal principal)
        : base(principal)
        {
        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Role
        {
            get
            {
                return this.FindFirst(ClaimTypes.Role).Value;
            }
        }

        public string UserId
        {
            get
            {
                return this.FindFirst(ClaimTypes.UserData).Value;
            }
        }
    }
}