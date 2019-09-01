using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Net;
using System.Web.Http.Cors;
using OBSMVCApi;
using OBSMVCApi.Models;
using OBSMVCApi.Results;
using OBSMVCApi.Providers;

namespace OBSMVCApi.Controllers
{
    //[Authorize]

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        ApplicationDbContext db = new ApplicationDbContext();


        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AccountController(ApplicationUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo    --- The Current UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            var userId = HttpContext.Current.User.Identity.GetUserId();  //add
            var user = db.Users.Find(userId);  //add
            return new UserInfoViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Address = user.Address,
                DoB = user.DoB,
                ImageUrl = user.ImageUrl,
                Id = user.Id,
                //Roles = (from userRole in db.Users
                //         join role in db.Roles on userRole.Id equals userId
                //         select role.Name).ToList()
                Role = (from userRole in user.Roles
                        join role in db.Roles on userRole.RoleId equals role.Id
                        select role.Name).FirstOrDefault()
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }


        // POST api/Account/ChangePassword
        [Route("ChangePassword/{id}")]
        public async Task<IHttpActionResult> ChangePassword(string id,ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(id, model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        public RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        // POST api/Account/Register/User
        [AllowAnonymous]
        [HttpPost, Route("Register/Customer")]
        public async Task<IHttpActionResult> UserRegister(UserRegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserType = "Customer",
                UserName = model.UserName,
                IsActive = true
            };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok(model);
        }


        // POST api/Account/Register/SysUser
        [AllowAnonymous]
        [HttpPost, Route("Register/SystemUser")]
        public async Task<IHttpActionResult> SysUserRegister(SysUserRegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserType = "Employee",
                UserName = model.UserName,
                IsActive = true
            };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            UserManager.AddToRole(user.Id, model.Role);
            //if (model.Roles.Count!=0)
            //{
            //    UserManager.AddToRoles(user.Id, model.Roles.ToArray());  //roles add to user
            //}
            return Ok(model);
        }


        // [Authorize(Roles="Admin")]
        [Route("users")]
        public IHttpActionResult GetUsersWithRoles()
        {
            var users = (from user in db.Users
                         select new UserInfoViewModel
                         {
                             Email = user.Email,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             PhoneNumber = user.PhoneNumber,
                             Gender = user.Gender,
                             Address = user.Address,
                             UserType = user.UserType,
                             DoB = user.DoB,
                             ImageUrl = user.ImageUrl,
                             Id = user.Id,
                             IsActive = user.IsActive,

                             //Roles = (from userRole in user.Roles
                             //         join role in db.Roles on userRole.RoleId equals role.Id
                             //         select role.Name).ToList(),

                             Role = (from userRole in user.Roles
                                     join role in db.Roles on userRole.RoleId equals role.Id
                                     select role.Name).FirstOrDefault()
                         });
            return Ok(users);
        }


        // [Authorize(Roles = "Admin")]
        [HttpGet, Route("{id}")]
        public IHttpActionResult GetUser(string id)
        {
            var users = (from user in db.Users
                         where user.Id == id
                         select new UserInfoViewModel
                         {
                             Id=user.Id,
                             Email = user.Email,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             PhoneNumber = user.PhoneNumber,
                             Gender = user.Gender,
                             Address = user.Address,
                             DoB = user.DoB,
                             UserType = user.UserType,
                             ImageUrl = user.ImageUrl,
                             IsActive = user.IsActive,
                             Role = (from userRole in user.Roles
                                     join role in db.Roles on userRole.RoleId equals role.Id
                                     select role.Name).FirstOrDefault()
                         }).FirstOrDefault();
            return Ok(users);
        }


        //update user data
        [HttpPut, Route("update/{id}")]
        public IHttpActionResult UpdateUser(string id, UserInfoViewModel model)
        {
            var user = UserManager.FindById(id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            //user.UserName = model.UserName;
            //user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.ImageUrl = model.ImageUrl;
            user.DoB = model.DoB;
            user.Gender = model.Gender;
            user.Address = model.Address;
            try
            {
                UserManager.Update(user);
                return Ok(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //delete user by  id
        [HttpDelete, Route("{id}")]
        public IHttpActionResult DeleteUser(string id)
        {
            var user = UserManager.FindById(id);
            if (user != null)
            {
                try
                {
                    UserManager.Delete(user);
                    return Content(HttpStatusCode.Accepted, "User has been deleted");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Content(HttpStatusCode.BadGateway, "User not found");
        }

        #region not needed
        //=========================================================================================================================
        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName,user.Id,user.UserType,user.FirstName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }



        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }
        #endregion 
    }
}
