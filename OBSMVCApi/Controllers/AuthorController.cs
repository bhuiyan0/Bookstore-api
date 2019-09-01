using OBSMVCApi.DAL;
using OBSMVCApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/author")]
    public class AuthorController : ApiController
    {
        private readonly IRepository<Author> _repo;
        private readonly AuthorRepository repo;
        public AuthorController(IRepository<Author> repository, AuthorRepository authorRepository)
        {
            _repo = repository;
            repo = authorRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var author = await _repo.Get();
            return Ok(author);
        }

        [HttpGet, Route("GetActive")]
        public async Task<IHttpActionResult> GetActive()
        {
            var author = await repo.GetActive();
            return Ok(author);
        }

        [HttpGet, Route("GetInactive")]

        public async Task<IHttpActionResult> GetInactive()
        {
            var author = await repo.GetInactive();
            return Ok(author);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var author = await _repo.Get(id);
            if (author != null)
            {
                return Ok(author);
            }

            return Ok("Author not found");
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Author model)
        {
            await _repo.Post(model);
            return Ok(model);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, Author model)
        {
            await _repo.Put(id, model);
            return Ok(model);
        }

        //[HttpDelete]
        //public async Task<IHttpActionResult> Delete(int id)
        //{
        //    var data= await _repo.Delete(id);
        //    if (data!=null)
        //    {
        //        return Ok("Deleted");
        //    }

        //    return Ok("Author not found");
        //}

        [HttpPut, Route("Delete/{id}")]
        public async Task<IHttpActionResult> SoftDelete(int id)
        {
            var data = await repo.SoftDelete(id);
            if (data != null)
            {
                return Ok("Deleted");
            }

            return Ok("Author not found");
        }

    }
}
