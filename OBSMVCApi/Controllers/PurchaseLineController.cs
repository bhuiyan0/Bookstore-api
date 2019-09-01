using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OBSMVCApi.DAL;
using OBSMVCApi.Models;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/PurchaseLine")]
    public class PurchaseLineController : ApiController
    {
        private IRepository<PurchaseLine> _irepo;
        private PurchaseLineRepository _repo;

        public PurchaseLineController(IRepository<PurchaseLine> repository,PurchaseLineRepository purchaseLineRepository)
        {
            _irepo = repository;
            _repo = purchaseLineRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var purchaseLines = await _irepo.Get();
            return Ok(purchaseLines);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var purchaseLines = await _irepo.Get(id);
            return Ok(purchaseLines);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PurchaseLine model)
        {
            await _irepo.Post(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(PurchaseLine model)
        {
            await _irepo.Put(model);
            return Content(HttpStatusCode.Accepted, "Record Updated");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _irepo.Delete(id);
            return Content(HttpStatusCode.Accepted, "Record Deleted");
        }

        // Get Purchase line by PurchaseId
        [HttpGet, Route("GetByPurchase/{id}")]
        public IHttpActionResult GetByPurchaseId(int id)
        {
            var purchaseLines = _repo.GetByPurchaseId(id);
            return Ok(purchaseLines);
        }
    }
}
