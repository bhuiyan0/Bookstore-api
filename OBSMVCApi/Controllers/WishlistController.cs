using OBSMVCApi.DAL;
using OBSMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/Wishlist")]
    public class WishlistController : ApiController
    {
        private IRepository<WishList> _repo;
        public WishlistRepository repo;
        public WishlistController(IRepository<WishList> repository, WishlistRepository wishlistRepository)
        {
            _repo = repository;
            repo = wishlistRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var wishlists = await _repo.Get();
            return Ok(wishlists);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var wishlists = await _repo.Get(id);
            return Ok(wishlists);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(int bId, string uId)
        {

            await repo.Posts(bId,uId);
            return Ok();

        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(WishList model)
        {
            await _repo.Put(model);
            return Content(HttpStatusCode.Accepted, "Record Updated");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return Content(HttpStatusCode.Accepted, "Record Deleted");
        }

        [HttpGet, Route("GetByUser/{userId}")]
        public IHttpActionResult GetByUser(string userId)
        {
            var wishLists = repo.GetWishListByUser(userId).ToList();
            if (wishLists.Any())
            {
                return Ok(wishLists);
            }
            return Ok("There is book in your wish list");
        }


    }
}
