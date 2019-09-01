using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using OBSMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNetIdentity.WebApi.Controllers
{
    //[Authorize(Roles="Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        public RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        public UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext()));

        //get all roles 
        [HttpGet,Route(Name = "GetAllRoles")]   
        public IHttpActionResult GetAllRoles()
        {
            var roles = roleManager.Roles;
            return Ok(roles);
        }

        //get specific role by role id or rolename
        [HttpGet, Route("{name}", Name = "GetRoleById")]
        public IHttpActionResult GetRole(string name)
        {
            var role = roleManager.FindByName(name);
            if (role != null)
            {
                return Ok(role);
            }
            return NotFound();
        }
       

        //Create new role 
        [HttpPost,Route("create")]
        public  IHttpActionResult Create(CreateRoleBindingModel model)
        {
            if (!roleManager.RoleExists(model.Name))
            {
                var role = new IdentityRole();
                role.Name = model.Name;
                roleManager.Create(role);
                return Ok();
               // return RedirectToRoute("GetAllRoles", null);

            }
            return Content(HttpStatusCode.BadRequest, "Role is already exist");
        }

        //Update role by role id
        [HttpPut,Route("update/{id}")]
        public async Task<IHttpActionResult> Edit(CreateRoleBindingModel model,string id)
        {
            IdentityRole role = roleManager.FindById(id);
            if (role!=null)
            {
                role.Name = model.Name;
                await roleManager.UpdateAsync(role);
                return Ok(model);          
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Something went wrong ! ");
            }
        }


        //Delete Specific Role by name or id
        [HttpDelete,Route("delete/{id}")]
        public async Task<IHttpActionResult> DeleteRole(string Id)
        {
            var role =await roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    return Ok(result);
                }
                return Ok();
            }
            return NotFound();
        }

        //get all roles of specific user 
        [HttpGet,Route("getroles/{name}")]
        public IHttpActionResult GetRolesOfUser(string name)
        {
            var user = userManager.FindByName(name);
            var roles = userManager.GetRoles(user.Id).ToList();
            return Ok(roles);
        }

        //role manage for invidual user
        [HttpPost,Route("EditUserRole/{userId}")]
        public IHttpActionResult EditUserRole(string userId,CreateRoleBindingModel model)
        {
            var roles=userManager.GetRoles(userId).ToList();
            var user = userManager.FindById(userId);
            try
            {
                foreach (var item in model.Roles)
                {
                    if (!roleManager.RoleExists(item))
                    {
                        return Content(HttpStatusCode.BadRequest, "Role name "+item +" is not exist!");
                    }
                }
                userManager.RemoveFromRoles(userId, roles.ToArray());
                userManager.AddToRoles(userId, model.Roles.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }
    }
}
