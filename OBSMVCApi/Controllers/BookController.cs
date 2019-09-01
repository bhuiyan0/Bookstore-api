using System.Linq;
using System.Net;
using OBSMVCApi.DAL;
using OBSMVCApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/Book")]
    public class BookController : ApiController
    {
        private IRepository<Book> _irepo;
        private BookRepository _repo;

        public BookController(IRepository<Book> repository, BookRepository bookRepository)
        {
            _irepo = repository;
            _repo = bookRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var book = await _repo.GetAllBooks();
            return Ok(book);
        }


        [HttpGet, Route("GetActive")]
        public async Task<IHttpActionResult> GetActive()
        {
            var book = await _repo.GetActiveBooks();
            return Ok(book);
        }

        [HttpGet, Route("GetInactive")]
        public async Task<IHttpActionResult> GetInactive()
        {
            var book = await _repo.GetInactive();
            return Ok(book);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var author = await _repo.GetById(id);
            if (author!=null)
            {
                return Ok(author);
            }

            return Ok("Book Not found");
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Book model)
        {
            await _irepo.Post(model);
            return Ok(model);
        }

         [HttpPost,Route("create")]
        public async Task<IHttpActionResult> PostByVM(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repo.PostByVM(model);
            return Ok(model);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, BookEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repo.EditByVM(id,model);
            return Ok(model);
        }

        [HttpDelete,Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var data = await _irepo.Delete(id);
            if (data != null)
            {
                return Ok("Deleted");
            }
            return Ok("The book you want to delete is not found");
        }

        [HttpPut, Route("Delete/{id}")]
        public async Task<IHttpActionResult> SoftDelete(int id)
        {
            var data = await _repo.SoftDelete(id);
            if (data != null)
            {
                return Ok("Deleted");
            }

            return Ok("Author not found");
        }

        //get all books by author
        [HttpGet, Route("GetByAuthor/{authorId}")]
        public  IHttpActionResult GetBooksByAuthor(int authorId)
        {
            var books = _repo.GetBooksByAuthor(authorId).ToList();
            if (books.Any())
            {
                return Ok(books);
            }

            return Ok("Not found");
        }

        //get all books by publisher
        [HttpGet, Route("GetByPublisher/{publisherId}")]
        public  IHttpActionResult GetBooksByPublisher(int publisherId)
        {
            var books = _repo.GetBooksByPublisher(publisherId).ToList();
            if (books.Any())
            {
                return Ok(books);
            }

            return Ok("Not found");
        }

        //get all books by category
        [HttpGet, Route("GetByCategory/{categoryId}")]
        public  IHttpActionResult GetBooksByCategory(int categoryId)
        {
            var books = _repo.GetBooksByCategory(categoryId).ToList();
            if (books.Any())
            {
                return Ok(books);
            }

            return Ok("Not found");
        }

        // Get Books by book name
        [HttpGet,Route("SearchBook/{searchString}")]
        public IHttpActionResult SearchByBookName(string searchString)
        {
            var books = _repo.SearchBook(searchString);
            return Ok(books);
        }
    }
}
