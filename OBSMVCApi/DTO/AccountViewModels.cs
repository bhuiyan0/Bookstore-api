using OBSMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OBSMVCApi.Models
{
    // Models returned by AccountController actions.
    

    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
