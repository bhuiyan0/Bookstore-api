using OBSMVCApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OBSMVCApi.DAL
{
    public class BookReviewRepository : IRepository<BookReview>
    {
        private ApplicationDbContext _db;
        public BookReviewRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<BookReview>> Get()
        {
            return await _db.BookReviews.OrderBy(r=>r.ReviewDate).ToListAsync();
        }


        public async Task<BookReview> Get(int id)
        {
            var review = await _db.BookReviews.FindAsync(id);
            return review;
        }


        public async Task<object> Post(BookReview entity)
        {
            _db.BookReviews.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(BookReview entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var review = await _db.BookReviews.FindAsync(id);
            if (review != null)
            {
                _db.BookReviews.Remove(review);
                await _db.SaveChangesAsync();
            }
            return review;
        }

        // Get Book Review by BookId
        public IEnumerable<BookReview> GetReviewByBookId(int id)
        {
            var books = from b in _db.BookReviews
                        where b.BookId == id
                        select b;
            return books;
        }

        public async Task<object> Put(int id, BookReview entity)
        {
            var bookreview = _db.BookReviews.Find(id);
            bookreview.BookId = entity.BookId;
            bookreview.Book = entity.Book;
            bookreview.Comments = entity.Comments;
            bookreview.UserId = bookreview.UserId;
            bookreview.Comments = bookreview.Comments;
            await _db.SaveChangesAsync();
            return entity;
        }

       
        public Task<IEnumerable<BookReview>> GetActive()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<BookReview>> GetInactive()
        {
            throw new System.NotImplementedException();
        }
    }
}