using OBSMVCApi.DAL;
using OBSMVCApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        private IRepository<Category> _repo;
        public CategoryRepository repo;
        public CategoryController(IRepository<Category> repository , CategoryRepository categoryRepository)
        {
            _repo = repository;
            repo = categoryRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var category = await _repo.Get();
            return Ok(category);
        }

        [HttpGet, Route("GetActive")]
        public async Task<IHttpActionResult> GetActive()
        {
            var category = await repo.GetActive();
            return Ok(category);
        }

        [HttpGet, Route("GetInactive")]

        public async Task<IHttpActionResult> GetInactive()
        {
            var category = await repo.GetInactive();
            return Ok(category);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var category = await repo.Get(id);
            if (category!=null)
            {
                return Ok(category);
            }

            return Ok("Category Not found");
        }

        //[HttpGet, Route("{name}")]
        //public async Task<IHttpActionResult> Get(string name)
        //{
        //    var category = await repo.GetByName(name);
        //    if (category!=null)
        //    {
        //        return Ok(category);
        //    }

        //    return Ok("Category Not found");
        //}

        [HttpPost]
        public async Task<IHttpActionResult> Post(Category model)
        {
            await _repo.Post(model);
            return Ok(model);
        }

        [HttpPut,Route("{id}")]
        public async Task<IHttpActionResult> Put(int id,Category model)
        {
            await repo.Put(id,model);
            return Ok(model);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var data = await _repo.Delete(id);
            if (data != null)
            {
                return Ok("Deleted");
            }
            return Ok("The author you want to delete is not found");
        }

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
