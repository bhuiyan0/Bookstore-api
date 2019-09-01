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
    [RoutePrefix("api/stock")]
    public class StockController : ApiController
    {
        private IRepository<Stock> _repo;
        public StockController(IRepository<Stock> repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var author = await _repo.Get();
            return Ok(author);
        }
        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var stock = await _repo.Get(id);
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Stock model)
        {
            await _repo.Post(model);
            return Ok(model);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put(int id,Stock model)
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

    }
}
