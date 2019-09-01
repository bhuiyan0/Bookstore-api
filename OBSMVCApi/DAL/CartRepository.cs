using OBSMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using OBSMVCApi.DTO;

namespace OBSMVCApi.DAL
{
    public class CartRepository : IRepository<Cart>
    {
        private ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<object> Delete(int id)
        {
            var cart = await _db.Carts.FindAsync(id);
            if (cart != null)
            {
                _db.Carts.Remove(cart);
                await _db.SaveChangesAsync();
                return cart;
            }

            return null;
        }

        public async Task<object> DeleteRange(string userId)
        {
            _db.Carts.RemoveRange(_db.Carts.Where(a => a.UserId==userId));
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<IEnumerable<Cart>> Get()
        {
            return await _db.Carts.ToListAsync();
        }

        public async Task<Cart> Get(int id)
        {
            var cart = await _db.Carts.FindAsync(id);
            return cart;
        }

        public async Task<object> Post(Cart entity)
        {
            if (_db.Carts.Any(b => b.UserId == entity.UserId && b.BookId == entity.BookId))
            {
                return null;
            }
            _db.Carts.Add(entity);
            await _db.SaveChangesAsync();
            return entity;

        }


        public async Task<object> Put(Cart entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(int id, Cart entity)
        {
            var cart = await _db.Carts.FindAsync(id);
            cart.Quantity = entity.Quantity;
            _db.Entry(cart).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return cart;
        }
        public async Task<object> Posts(int bId, string uId)
        {
            if (_db.Carts.Any(b => b.UserId == uId && b.BookId == bId))
            {
                int cartId = (from c in _db.Carts
                              where c.BookId == bId && c.UserId == uId
                              select c.CartId).SingleOrDefault();

                var cart = await _db.Carts.FindAsync(cartId);
                cart.Quantity += 1;
                _db.Entry(cart).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return null;

            }
            else
            {
                var cart = new Cart();
                cart.BookId = bId;
                cart.UserId = uId;
                cart.Quantity = 1;
                _db.Carts.Add(cart);
                await _db.SaveChangesAsync();
                return cart;
            }
        }


        // Get cart list by UserId nasir
        public IEnumerable<CartViewModel> GetCartsByUser(string id)
        {
            var carts = (from c in _db.Carts
                         join b in _db.Books on c.BookId equals b.BookId
                         join a in _db.Authors on b.AuthorId equals a.AuthorId
                         where c.UserId == id
                         select new CartViewModel
                         {
                             CartId = c.CartId,
                             UserId = c.UserId,
                             BookId = c.BookId,
                             BookName = b.BookName,
                             AuthorName = a.AuthorName,
                             Price = b.SellingPrice,
                             Quantity = c.Quantity,
                             Total = b.SellingPrice * c.Quantity,
                             ImageUrl = b.ImageUrl
                         }).ToList();
            return carts;
        }

        // Get cart list by UserId by mamun
        public CartEditViewModel GetCartsByUserId(string id)
        {
            var carts = (from c in _db.Carts
                         select new CartEditViewModel
                         {
                             UserId = id,
                             CartList = (from ca in _db.Carts
                                         join b in _db.Books on ca.BookId equals b.BookId
                                         join a in _db.Authors on b.AuthorId equals a.AuthorId
                                         where ca.UserId == id
                                         select new CartList
                                         {
                                             CartId = ca.CartId,
                                             AuthorName = a.AuthorName,
                                             Price = b.SellingPrice,
                                             Quantity = c.Quantity,
                                             Total = b.SellingPrice * c.Quantity,
                                             ImageUrl = b.ImageUrl,
                                             BookId = b.BookId,
                                             BookName = b.BookName
                                         }).ToList()
                         }).FirstOrDefault();
            return carts;
        }

        public int CartCount(string id)
        {
            return _db.Carts.Where(a => a.UserId == id).ToList().Count();
        }

        public decimal CartTotal(string id)
        {
            decimal total = 0;
            var carts = (from c in _db.Carts
                         join b in _db.Books
                         on c.BookId equals b.BookId
                         where c.UserId == id
                         select new { c.BookId, c.CartId, c.Quantity, b.SellingPrice }).ToList();
            carts.ForEach(a =>
            {
                var itemTotal = a.Quantity * a.SellingPrice;
                total += itemTotal;
            });
            return total;
        }
    }
}