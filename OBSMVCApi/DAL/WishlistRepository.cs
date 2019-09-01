using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OBSMVCApi.DTO;
using OBSMVCApi.Models;

namespace OBSMVCApi.DAL
{
    public class WishlistRepository : IRepository<WishList>
    {
        private ApplicationDbContext _db;
        public WishlistRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<WishList>> Get()
        {
            return await _db.WishLists.ToListAsync();
        }


        public async Task<WishList> Get(int id)
        {
            var wishList = await _db.WishLists.FindAsync(id);
            return wishList;
        }

        public async Task<object> Put(WishList entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var wishList = await _db.WishLists.FindAsync(id);
            if (wishList!=null)
            {
                _db.WishLists.Remove(wishList);
                await _db.SaveChangesAsync();
                return wishList;
            }

            return null;
        }

        public async Task<object> Post(WishList entity)
        {
            if (_db.WishLists.Any(b => b.UserId == entity.UserId && b.BookId == entity.BookId))
            {
                return null;
            }
            else
            {
                _db.WishLists.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<object> Posts(int bId, string uId)
        {
            if (_db.WishLists.Any(b => b.UserId == uId && b.BookId == bId))
            {
                return null;
            }
            else
            {
                var wishList = new WishList();
                wishList.BookId = bId;
                wishList.UserId = uId;
                _db.WishLists.Add(wishList);
                await _db.SaveChangesAsync();
                return wishList;
            }
        }

        // Get wish list by UserId
        public IEnumerable<WishListViewModel> GetWishListByUser(string id)
        {
            var wishLists = (from w in _db.WishLists
                            join b in _db.Books on w.BookId equals b.BookId
                            join a in _db.Authors on b.AuthorId equals a.AuthorId
                            where w.UserId == id
                            select new WishListViewModel {
                                WishListId=w.WishListId,
                                UserId= w.UserId,
                                BookId=w.BookId,
                                BookName=b.BookName,
                                Price=b.SellingPrice,
                                ImageUrl=b.ImageUrl,
                                AuthorName=a.AuthorName,
                            }).ToList();
            return wishLists;
        }

        public Task<object> Put(int id, WishList entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<WishList>> GetActive()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<WishList>> GetInactive()
        {
            throw new System.NotImplementedException();
        }
    }
}