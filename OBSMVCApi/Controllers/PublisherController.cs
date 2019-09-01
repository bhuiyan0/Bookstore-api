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
    [RoutePrefix("api/publisher")]
    public class PublisherController : ApiController
    {
        private IRepository<Publisher> _repo;
        private PublisherRepository prepo;


        public PublisherController(IRepository<Publisher> repository, PublisherRepository publisherRepository)
        {
            _repo = repository;
            prepo = publisherRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var publisher = await _repo.Get();
            return Ok(publisher);
        }

        [HttpGet, Route("GetActive")]
        public async Task<IHttpActionResult> GetActive()
        {
            var publisher = await prepo.GetActive();
            return Ok(publisher);
        }

        [HttpGet, Route("GetInactive")]

        public async Task<IHttpActionResult> GetInactive()
        {
            var publisher = await prepo.GetInactive();
            return Ok(publisher);
        }


        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var publisher = await _repo.Get(id);
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Publisher model)
        {
            await _repo.Post(model);
            return Ok(model);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put(int id,Publisher model)
        {
            await _repo.Put(id, model);
            return Content(HttpStatusCode.Accepted, "Record Updated");
        }

        //[HttpDelete]
        //public async Task<IHttpActionResult> Delete(int id)
        //{
        //    await _repo.Delete(id);
        //    return Content(HttpStatusCode.Accepted, "Record Deleted");
        //}

        [HttpPut, Route("Delete/{id}")]
        public async Task<IHttpActionResult> SoftDelete(int id)
        {
            var data = await prepo.SoftDelete(id);
            if (data != null)
            {
                return Ok("Deleted");
            }

            return Ok("Author not found");
        }
    }
}
