using OBSMVCApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OBSMVCApi.DAL
{
    public class PurchaseLineRepository : IRepository<PurchaseLine>
    {
        private ApplicationDbContext _db;
        public PurchaseLineRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<PurchaseLine>> Get()
        {
            return await _db.PurchaseLines.ToListAsync();
        }


        public async Task<PurchaseLine> Get(int id)
        {
            var purchaseLine = await _db.PurchaseLines.FindAsync(id);
            return purchaseLine;
        }


        public async Task<object> Post(PurchaseLine entity)
        {
            var qty = (from s in _db.Stocks
                where s.BookId == entity.BookId
                select s).Single();

            _db.PurchaseLines.Add(entity);
            qty.Quantity = qty.Quantity + entity.Quantity;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(PurchaseLine entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var purchaseLine = await _db.PurchaseLines.FindAsync(id);
            if (purchaseLine!=null)
            {
                _db.PurchaseLines.Remove(purchaseLine);
                await _db.SaveChangesAsync();
                return purchaseLine;
            }

            return null;
        }

        //get purchase lines by orderId
        public IEnumerable<PurchaseLine> GetByPurchaseId(int purchaseId)
        {
            var purchaseLines = from pl in _db.PurchaseLines
                                where pl.PurchaseId == purchaseId
                                select pl;
            return purchaseLines;
        }

        public Task<object> Put(int id, PurchaseLine entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PurchaseLine>> GetActive()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PurchaseLine>> GetInactive()
        {
            throw new System.NotImplementedException();
        }
    }
}