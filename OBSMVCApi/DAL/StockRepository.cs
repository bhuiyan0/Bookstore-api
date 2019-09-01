using OBSMVCApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OBSMVCApi.DAL
{
    public class StockRepository : IRepository<Stock>
    {
        private ApplicationDbContext _db;
        public StockRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<Stock>> Get()
        {
            return await _db.Stocks.ToListAsync();
        }


        public async Task<Stock> Get(int id)
        {
            var stock = await _db.Stocks.FindAsync(id);
            return stock;
        }


        public async Task<object> Post(Stock entity)
        {
            
            if ( _db.Stocks.Any(s=>s.BookId==entity.BookId))
            {
                return "Book already exits";
            }
            else
            {
                _db.Stocks.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<object> Put(Stock entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var stock = await _db.Stocks.FindAsync(id);
            if (stock!=null)
            {
                _db.Stocks.Remove(stock);
                await _db.SaveChangesAsync();
                return stock;
            }

            return null;
        }

        // Get book stock by BookId
        public Stock GeTByBookId(int id)
        {
            var book = _db.Stocks.Single(b => b.BookId == id);
            return book;
        }

        public async Task<object> Put(int id, Stock entity)
        {
            var stock = _db.Stocks.Find(id);
            stock.BookId = entity.BookId;
            stock.Quantity = entity.Quantity;
            stock.ReorderLevel = entity.ReorderLevel;
            await _db.SaveChangesAsync();
            return entity;
        }

        public Task<IEnumerable<Stock>> GetActive()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetInactive()
        {
            throw new System.NotImplementedException();
        }
    }
}