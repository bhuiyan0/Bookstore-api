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
    [RoutePrefix("api/feedback")]
    public class FeedbackController : ApiController
    {
        private IRepository<Feedback> _repo;

        public FeedbackController(IRepository<Feedback> repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var publisher = await _repo.Get();
            return Ok(publisher);
        }

        [HttpGet,Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var publisher = await _repo.Get(id);
            if (publisher != null)
            {
                return Ok(publisher);
            }
            return Ok("Not found");
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Feedback model)
        {
            await _repo.Post(model);
            return Ok(model);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put(int id,Feedback model)
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
