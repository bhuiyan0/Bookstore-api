using OBSMVCApi.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace OBSMVCApi.DAL
{

    public class BookRepository : IRepository<Book>
    {
        private ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        // get all books list
        public async Task<IEnumerable<BookViewModel>> GetAllBooks()
        {
            //return await _db.Books
            //    .Include(a=>a.Author)
            //    .Include(c=>c.Category)
            //    .Include(p=>p.Publisher).ToListAsync();

            var books = (from b in _db.Books
                         join a in _db.Authors
                         on b.AuthorId equals a.AuthorId
                         join p in _db.Publishers
                         on b.PublisherId equals p.PublisherId
                         join s in _db.Stocks
                         on b.BookId equals s.BookId
                         join c in _db.Categories
                         on b.CategoryId equals c.CategoryId
                         select new BookViewModel
                         {
                             BookId = b.BookId,
                             BookName = b.BookName,
                             AuthorId = b.AuthorId,
                             CategoryId = b.CategoryId,
                             Descriptions = b.Descriptions,
                             Edition = b.Edition,
                             ImageUrl = b.ImageUrl,
                             IsActive = b.IsActive,
                             ISBN = b.ISBN,
                             Language = b.Language,
                             NumberOfPage = b.NumberOfPage,
                             CostPrice = b.CostPrice,
                             SellingPrice = b.SellingPrice,
                             PublisherId = b.PublisherId,
                          //   TranslatorId = b.TranslatorId,
                             AuthorName = a.AuthorName,
                             Quantity = s.Quantity,
                             ReorderLevel = s.ReorderLevel,
                             PublisherName = p.PublisherName,
                             CategoryName=c.CategoryName
                             
                         }).ToListAsync();
            //(b=>new BookViewModel { BookId = b.BookId,BookName=b.BookName,AuthorId=b.AuthorId,AuthorName=b.AuthorName,PublisherName=b.AuthorName });

            return await books;
        }


        // get all active books list
        public async Task<IEnumerable<BookViewModel>> GetActiveBooks()
        {
            var books = (from b in _db.Books
                         join a in _db.Authors
                         on b.AuthorId equals a.AuthorId
                         join p in _db.Publishers
                         on b.PublisherId equals p.PublisherId
                         join s in _db.Stocks
                         on b.BookId equals s.BookId
                         join c in _db.Categories
                         on b.CategoryId equals c.CategoryId
                         where b.IsActive==true
                         select new BookViewModel
                         {
                             BookId = b.BookId,
                             BookName = b.BookName,
                             AuthorId = b.AuthorId,
                             CategoryId = b.CategoryId,
                             Descriptions = b.Descriptions,
                             Edition = b.Edition,
                             ImageUrl = b.ImageUrl,
                             IsActive = b.IsActive,
                             ISBN = b.ISBN,
                             Language = b.Language,
                             NumberOfPage = b.NumberOfPage,
                             CostPrice = b.CostPrice,
                             SellingPrice = b.SellingPrice,
                             PublisherId = b.PublisherId,
                            // TranslatorId = b.TranslatorId,
                             AuthorName = a.AuthorName,
                             Quantity = s.Quantity,
                             ReorderLevel = s.ReorderLevel,
                             PublisherName = p.PublisherName,
                             CategoryName = c.CategoryName
                         }).ToListAsync();
            return await books;
        }


        // get all inactive books list
        public async Task<IEnumerable<BookViewModel>> GetInactive()
        {
            var books = (from b in _db.Books
                         join a in _db.Authors
                         on b.AuthorId equals a.AuthorId
                         join p in _db.Publishers
                         on b.PublisherId equals p.PublisherId
                         join s in _db.Stocks
                         on b.BookId equals s.BookId
                         join c in _db.Categories
                         on b.CategoryId equals c.CategoryId
                         where b.IsActive == false
                         select new BookViewModel
                         {
                             BookId = b.BookId,
                             BookName = b.BookName,
                             AuthorId = b.AuthorId,
                             CategoryId = b.CategoryId,
                             Descriptions = b.Descriptions,
                             Edition = b.Edition,
                             ImageUrl = b.ImageUrl,
                             IsActive = b.IsActive,
                             ISBN = b.ISBN,
                             Language = b.Language,
                             NumberOfPage = b.NumberOfPage,
                             CostPrice = b.CostPrice,
                             SellingPrice = b.SellingPrice,
                             PublisherId = b.PublisherId,
                           //  TranslatorId = b.TranslatorId,
                             AuthorName = a.AuthorName,
                             Quantity = s.Quantity,
                             ReorderLevel = s.ReorderLevel,
                             PublisherName = p.PublisherName,
                             CategoryName = c.CategoryName
                         }).ToListAsync();
            return await books;

        }


        // get book by id
        
        public async Task<BookViewModel> GetById(int id)
        {
            var books = (from b in _db.Books
                         join a in _db.Authors
                         on b.AuthorId equals a.AuthorId
                         join p in _db.Publishers
                         on b.PublisherId equals p.PublisherId
                         join s in _db.Stocks
                         on b.BookId equals s.BookId
                         join c in _db.Categories
                         on b.CategoryId equals c.CategoryId
                         where b.BookId == id
                         select new BookViewModel
                         {
                             BookId = b.BookId,
                             BookName = b.BookName,
                             AuthorId = b.AuthorId,
                             CategoryId = b.CategoryId,
                             Descriptions = b.Descriptions,
                             Edition = b.Edition,
                             ImageUrl = b.ImageUrl,
                             IsActive = b.IsActive,
                             ISBN = b.ISBN,
                             Language = b.Language,
                             NumberOfPage = b.NumberOfPage,
                             CostPrice = b.CostPrice,
                             SellingPrice = b.SellingPrice,
                             PublisherId = b.PublisherId,
                          //   TranslatorId = b.TranslatorId,
                             AuthorName = a.AuthorName,
                             Quantity = s.Quantity,
                             ReorderLevel = s.ReorderLevel,
                             PublisherName = p.PublisherName,
                             CategoryName = c.CategoryName
                         }).FirstOrDefaultAsync();
            return await books;
        }


        //insert book 
        public async Task<object> Post(Book entity)
        {
            _db.Books.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }


        //insert book and stock by using view model
        public async Task<object> PostByVM(BookViewModel model)
        {
            var book = new Book();
            book.AuthorId = model.AuthorId;
            book.BookName = model.BookName;
            book.Descriptions = model.Descriptions;
            book.Edition = model.Edition;
            book.CategoryId = model.CategoryId;
            book.PublisherId = model.PublisherId;
         //   book.TranslatorId = model.TranslatorId;
            book.CostPrice = model.CostPrice;
            book.SellingPrice = model.SellingPrice;
            book.Language = model.Language;
            book.NumberOfPage = model.NumberOfPage;
            book.ImageUrl = model.ImageUrl;
            book.ISBN = model.ISBN;
            book.IsActive = true;

            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            var stock = new Stock();
            stock.BookId = book.BookId;
            stock.Quantity = model.Quantity;
            stock.ReorderLevel = model.ReorderLevel;
            _db.Stocks.Add(stock);
            await _db.SaveChangesAsync();

            return model;
        }

        // edit book and stock by using view model 
        public async Task<object> EditByVM(int id, BookEditViewModel model)
        {
           
            var book = _db.Books.Find(id);
            book.BookName = model.BookName;
            book.CategoryId = model.CategoryId;
            book.PublisherId = model.PublisherId;
            book.AuthorId = model.AuthorId;
            book.Language = model.Language;
            book.ImageUrl = model.ImageUrl;
            book.NumberOfPage = model.NumberOfPage;
            book.Descriptions = model.Descriptions;
            book.Edition = model.Edition;
            book.ISBN = model.ISBN;
         //   book.TranslatorId = model.TranslatorId;
            book.CostPrice = model.CostPrice;
            book.SellingPrice = model.SellingPrice;
            await _db.SaveChangesAsync();

            var stockId = (from s in _db.Stocks
                           where s.BookId == id
                           select s.StockId).SingleOrDefault();
            var stock = _db.Stocks.Find(stockId);
            stock.ReorderLevel = model.ReorderLevel;

            await _db.SaveChangesAsync();
            return model;
        }


        public async Task<object> Put(Book entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        //Update by id
        public async Task<object> Put(int id, Book entity)
        {
            var book = _db.Books.Find(id);
            book.BookName = entity.BookName;
            book.CategoryId = entity.CategoryId;
            book.Category = book.Category;
            book.PublisherId = book.PublisherId;
            book.Publisher = book.Publisher;
            book.AuthorId = book.AuthorId;
            book.Author = book.Author;
            book.Language = book.Language;
            book.ImageUrl = book.ImageUrl;
            book.NumberOfPage = book.NumberOfPage;
            book.BookReviews = entity.BookReviews;
            book.Descriptions = entity.Descriptions;
            book.Edition = entity.Edition;
            book.ISBN = entity.ISBN;
        //    book.TranslatorId = entity.TranslatorId;
            book.OrderLines = entity.OrderLines;
            book.Wishlists = entity.Wishlists;
            book.CostPrice = entity.CostPrice;
            book.SellingPrice = entity.SellingPrice;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book != null)
            {
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
                return book;
            }

            return null;
        }

        //Delete By Soft Delete
        public async Task<object> SoftDelete(int id)
        {
            var book = await _db.Books.FindAsync(id);

            if (book != null)
            {
                book.IsActive = false;
                await _db.SaveChangesAsync();
                return book;
            }
            return null;
        }

        // get all books by author
        public IEnumerable<Book> GetBooksByAuthor(int authorId)
        {
            var books = from b in _db.Books
                        where b.AuthorId == authorId && b.IsActive==true
                        select b;
            return books;
        }

        // get all books by publisher
        public IEnumerable<Book> GetBooksByPublisher(int publisherId)
        {
            var books = from b in _db.Books
                        where b.PublisherId == publisherId && b.IsActive == true
                        select b;
            return books;
        }

        // get all books by category
        public IEnumerable<Book> GetBooksByCategory(int categoryId)
        {
            var books = from b in _db.Books
                        where b.CategoryId == categoryId && b.IsActive == true
                        select b;
            return books;
        }

        // Search book by book name
        public IEnumerable<Book> SearchBook(string searchString)
        {
            var books = from b in _db.Books
                        where b.BookName.ToLower().Contains(searchString.ToLower())
                        select b;
            return books;
        }


        Task<IEnumerable<Book>> IRepository<Book>.Get()
        {
            throw new System.NotImplementedException();
        }

        Task<Book> IRepository<Book>.Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}