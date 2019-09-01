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
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private IRepository<Cart> _repo;
        public CartRepository repo;
        public CartController(IRepository<Cart> repository,CartRepository cartRepository)
        {
            _repo = repository;
            repo = cartRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var carts = await _repo.Get();
            return Ok(carts);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var carts = await _repo.Get(id);
            return Ok(carts);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(int bId, string uId)
        {

            var data= await repo.Posts(bId, uId);
            if (data==null)
            {
                return Ok("cart updated");
            }
            return Ok("Added New Book To cart");
        }

        [HttpPut,Route("{id}")]
        public async Task<IHttpActionResult> Put(int id,Cart model)
        {
            await _repo.Put(id,model);
            return Content(HttpStatusCode.Accepted, "Record Updated");
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(Cart model)
        {
            await _repo.Put(model);
            return Content(HttpStatusCode.Accepted, "Record Updated");
        }

        [HttpDelete,Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return Content(HttpStatusCode.Accepted, "Record Deleted");
        }

        [HttpDelete,Route("delete/{id}")]
        public async Task<IHttpActionResult> DeleteByUser(string id)
        {
            await repo.DeleteRange(id);
            return Content(HttpStatusCode.Accepted, "Record Deleted");
        }

        [HttpGet, Route("GetByUser/{userId}")]
        public IHttpActionResult GetByUser(string userId)
        {
            var wishLists = repo.GetCartsByUser(userId).ToList();
            if (wishLists.Any())
            {
                return Ok(wishLists);
            }
            return Ok("There is book in your cart");
        }

        [HttpGet, Route("GetByUserId/{userId}")]
        public IHttpActionResult GetByUserId(string userId)
        {
            var wishLists = repo.GetCartsByUserId(userId);
            if (wishLists != null)
            {
                return Ok(wishLists);
            }
            return Ok("There is book in your cart");
        }

        [HttpGet,Route("CartCount/{id}")]
        public int CartCount(string id)
        {
            return repo.CartCount(id);
        }

        [HttpGet,Route("CartTotal/{id}")]
        public decimal CartTotal(string id)
        {
            return repo.CartTotal(id);
        }



    }
}
