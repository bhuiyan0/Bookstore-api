using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using OBSMVCApi.DAL;
using OBSMVCApi.Models;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/BookReview")]
    public class BookReviewController : ApiController
    {
        private IRepository<BookReview> _irepo;
        private BookReviewRepository _repo;
        public BookReviewController(IRepository<BookReview> repository,BookReviewRepository bookReviewRepository)
        {
            _irepo = repository;
            _repo = bookReviewRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var author = await _irepo.Get();
            return Ok(author);
        }
        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var bookReview = await _irepo.Get(id);
            return Ok(bookReview);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(BookReview model)
        {
            await _irepo.Post(model);
            return Ok(model);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, BookReview model)
        {
            await _irepo.Put(id,model);
            return Ok("Review Updated");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _irepo.Delete(id);
            return Ok("Review Delete");
        }

        [HttpGet, Route("GetReviewById/{id}")]
        public IHttpActionResult GetReviewById(int id)
        {
            var review = _repo.GetReviewByBookId(id).ToList();
            if (review.Any())
            {
                return Ok(review);
            }

            return Ok("There is no review for this book");
        }
    }
}
