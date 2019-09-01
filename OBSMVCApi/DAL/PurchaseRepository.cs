using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OBSMVCApi.DTO;
using OBSMVCApi.Models;

namespace OBSMVCApi.DAL
{
    public class PurchaseRepository : IRepository<Purchase>
    {
        private ApplicationDbContext _db;
        public PurchaseRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<Purchase>> Get()
        {
            return await _db.Purchases
                .Include(p => p.Publisher)
                .Include(p=>p.PurchaseLine)
                .Include(pay=>pay.Payment)
                .ToListAsync();
        }


        public async Task<Purchase> Get(int id)
        {
            var purchase = await _db.Purchases.FindAsync(id);
            return purchase;
        }


        public async Task<object> Post(Purchase entity)
        {
            _db.Purchases.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(Purchase entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var purchase = await _db.Purchases.FindAsync(id);
            if (purchase != null)
            {
                _db.Purchases.Remove(purchase);
                await _db.SaveChangesAsync();
                return purchase;
            }

            return null;
        }

        public async Task<object> Put(int id, Purchase entity)
        {
            var purchase = _db.Purchases.Find(id);
            purchase.PublisherId = entity.PublisherId;
            purchase.Publisher = entity.Publisher;
            purchase.PurchaseDate = entity.PurchaseDate;
            purchase.PurchaseLine = entity.PurchaseLine;
            purchase.DiscountAmount = entity.DiscountAmount;
            purchase.UserId = entity.UserId;
            purchase.VatAmount = entity.VatAmount;
            await _db.SaveChangesAsync();
            return entity;
        }

        public Task<IEnumerable<Purchase>> GetActive()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetInactive()
        {
            throw new System.NotImplementedException();
        }


        public async Task<PurchaseViewModel> Insert(PurchaseViewModel model)
        {
            var payment = new Payment();
            var purchase = new Purchase();
            var pLine = new PurchaseLine();

            payment.PaymentType = "Purchase";
            payment.PaymentMethod = model.PaymentMethod;
            payment.Amount = model.Amount;
            payment.BankName = model.BankName;
            payment.TransactionId = model.TransactionId;
            payment.PaymentNote = model.PaymentNote;
            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();

            purchase.ReferenceNo = model.ReferenceNo;
            purchase.PurchaseStatus = model.PurchaseStatus;
            purchase.UserId = model.UserId;
            purchase.PurchaseDate = model.PurchaseDate;
            purchase.TotalAmount = model.TotalAmount;
            purchase.VatAmount = model.VatAmount;
            purchase.DiscountAmount = model.DiscountAmount;
            purchase.PublisherId = model.PublisherId; //fk m
            purchase.PaymentId = payment.PaymentId;  //fk a
            _db.Purchases.Add(purchase);
            await _db.SaveChangesAsync();

            model.PurchaseLines.ForEach(ol =>
            {
                pLine.PurchaseId = purchase.PurchaseId;  //fk a
                pLine.BookId = ol.BookId;
                pLine.Quantity = ol.Quantity;
                pLine.Rate = ol.Rate;
                _db.PurchaseLines.Add(pLine);
                _db.SaveChanges();

                //update stock 
                int stockId = _db.Stocks.Where(a => a.BookId == ol.BookId).Select(s => s.StockId).SingleOrDefault();
                Stock stock = _db.Stocks.Find(stockId);
                stock.Quantity += ol.Quantity;
                _db.Entry(stock).State = EntityState.Modified;
               _db.SaveChanges();

            });


            return model;
        }

    }
}