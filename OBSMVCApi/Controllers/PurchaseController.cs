using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OBSMVCApi.DAL;
using OBSMVCApi.DTO;
using OBSMVCApi.Models;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/purchase")]
    public class PurchaseController : ApiController
    {
        private IRepository<Purchase> _repo;
        private PurchaseRepository _prepo;

        public PurchaseController(IRepository<Purchase> repository,PurchaseRepository purchaseRepository)
        {
            _repo = repository;
            _prepo = purchaseRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var purchase = await _repo.Get();
            return Ok(purchase);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var purchase = await _repo.Get(id);
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Purchase model)
        {
            await _repo.Post(model);
            return Ok(model);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put(int id,Purchase model)
        {
            await _repo.Put(id,model);
            return Content(HttpStatusCode.Accepted, "Record Updated");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return Content(HttpStatusCode.Accepted, "Record Deleted");
        }

        [HttpPost,Route("Insert")]
        public async Task<IHttpActionResult> PostByVM(PurchaseViewModel model)
        {
            await _prepo.Insert(model);
            return Ok(model);
        }
    }
}
