using OBSMVCApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OBSMVCApi.DAL
{
    public class OrderlineRepository : IRepository<OrderLine>
    {
        private ApplicationDbContext _db;
        public OrderlineRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<OrderLine>> Get()
        {
            return await _db.OrderLines.ToListAsync();
        }

        public async Task<OrderLine> Get(int id)
        {
            var orderLine = await _db.OrderLines.FindAsync(id);
            return orderLine;
        }

        public async Task<object> Post(OrderLine entity)
        {
            var qty = (from s in _db.Stocks
                where s.BookId == entity.BookId
                select s).Single();


            if (entity.Quantity > qty.Quantity)
            {
                return null;
            }
            else
            {
                _db.OrderLines.Add(entity);
                qty.Quantity = qty.Quantity - entity.Quantity;
                await _db.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<object> Put(OrderLine entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var orderLine = await _db.OrderLines.FindAsync(id);

            if (orderLine != null)
            {
                _db.OrderLines.Remove(orderLine);
                await _db.SaveChangesAsync();
                return orderLine;
            }

            return null;
        }

        //get all Order line by orderId
        public IEnumerable<OrderLine> GetByOrderId(int orderId)
        {
            var orderLines = from ol in _db.OrderLines
                             where ol.OrderId == orderId
                             select ol;
            return orderLines;
        }

        public async Task<object> Put(int id, OrderLine entity)
        {
            var orderline = _db.OrderLines.Find(id);
            orderline.OrderId = entity.OrderId;
            
            orderline.Quantity = entity.Quantity;
            orderline.Rate = entity.Rate;
            orderline.BookId = entity.BookId;
           
            await _db.SaveChangesAsync();
            return entity;
        }

        public Task<IEnumerable<OrderLine>> GetActive()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<OrderLine>> GetInactive()
        {
            throw new System.NotImplementedException();
        }
    }
}