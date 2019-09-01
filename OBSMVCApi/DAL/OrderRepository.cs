using OBSMVCApi.DTO;
using OBSMVCApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OBSMVCApi.DAL
{
    public class OrderRepository : IRepository<Order>
    {
        private ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<Order>> Get()
        {
            return await _db.Orders
                .Include(p => p.OrderLine)
                .ToListAsync();
        }

        public async Task<Order> Get(int id)
        {
            var orders = await _db.Orders.FindAsync(id);
            return orders;
        }

        public async Task<object> Post(Order entity)
        {
            _db.Orders.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(Order entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
                return order;
            }

            return null;
        }


        //get all orders by user
        public IEnumerable<Order> GetOrdersByUser(string userID)
        {
            var orders = from o in _db.Orders
                         where o.UserID == userID
                         select o;
            return orders;
        }

        public async Task<object> Put(int id, Order entity)
        {
            var order = _db.Orders.Find(id);
            order.OrderDate = entity.OrderDate;
            order.UserID = entity.UserID;
            order.VatAmount = entity.VatAmount;
            order.DiscountAmount = entity.DiscountAmount;
            order.TotalAmount = entity.TotalAmount;

            await _db.SaveChangesAsync();
            return entity;
        }


        public async Task<OrderViewModel> Insert(OrderViewModel model)
        {
            var payment = new Payment();
            var address = new ShippingAddress();
            var order = new Order();
            var orderLine = new OrderLine();

            payment.PaymentType = "Order";
            payment.Amount = model.Amount;
            payment.PaymentMethod = model.PaymentMethod;
            // payment.BankName = model.BankName;
            payment.AccountNo = model.AccountNo;
            payment.TransactionId = model.TransactionId;
            payment.PaymentNote = model.PaymentNote;
            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();

            address.Name = model.Name;
            address.Phone = model.Phone;
            address.Email = model.Email;
            address.District = model.District;
            address.Address = model.Address;
            _db.ShippingAddresses.Add(address);
            await _db.SaveChangesAsync();

            order.OrderStatus = model.OrderStatus;
            order.UserID = model.UserID;
            order.OrderDate = model.OrderDate;
            order.TotalAmount = payment.Amount;
            // order.VatAmount = model.VatAmount;
            // order.DiscountAmount = model.DiscountAmount;
            order.ShippingAddressId = address.AddressId; //fk
            order.PaymentId = payment.PaymentId;  //fk
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            model.OrderLines.ForEach(ol =>
           {
               orderLine.OrderId = order.OrderId;  //fk
               orderLine.BookId = ol.BookId;
               orderLine.Quantity = ol.Quantity;
               orderLine.Rate = ol.Rate;
               _db.OrderLines.Add(orderLine);
               _db.SaveChanges();

               //update stock 
               int stockId = _db.Stocks.Where(a => a.BookId == ol.BookId).Select(s => s.StockId).SingleOrDefault();
               Stock stock = _db.Stocks.Find(stockId);
               stock.Quantity -= ol.Quantity;
               _db.Entry(stock).State = EntityState.Modified;
               _db.SaveChanges();
           });

            //delete cart items 
            return model;
        }


        public async Task<IEnumerable<OrdersViewModel>> GetByVM()
        {
            var orders = (from o in _db.Orders
                          join u in _db.Users on o.UserID equals u.Id
                          join p in _db.Payments on o.PaymentId equals p.PaymentId
                          join a in _db.ShippingAddresses on o.ShippingAddressId equals a.AddressId
                          select new OrdersViewModel
                          {
                              Amount = p.Amount,
                              PaymentMethod = p.PaymentMethod,
                              AccountNo = p.AccountNo,
                              TransactionId = p.TransactionId,
                              PaymentNote = p.PaymentNote,
                              BankName = p.BankName,

                              Name = a.Name,
                              Phone = a.Phone,
                              Email = a.Email,
                              District = a.District,
                              Address = a.Address,

                              OrderStatus = o.OrderStatus,
                              OrderDate = o.OrderDate,
                              OrderId = o.OrderId,

                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              UserEmail = u.Email,
                              UserPhone = u.PhoneNumber,
                          }).ToListAsync();
            return await orders;

        }
        public OrdersViewModel GetByOrderId(int id)
        {
            var orders = (from o in _db.Orders
                          join u in _db.Users on o.UserID equals u.Id
                          join p in _db.Payments on o.PaymentId equals p.PaymentId
                          join a in _db.ShippingAddresses on o.ShippingAddressId equals a.AddressId
                          where o.OrderId == id
                          select new OrdersViewModel
                          {
                              Amount = p.Amount,
                              PaymentMethod = p.PaymentMethod,
                              AccountNo = p.AccountNo,
                              TransactionId = p.TransactionId,
                              PaymentNote = p.PaymentNote,
                              BankName = p.BankName,

                              Name = a.Name,
                              Phone = a.Phone,
                              Email = a.Email,
                              District = a.District,
                              Address = a.Address,

                              OrderStatus = o.OrderStatus,
                              OrderDate = o.OrderDate,
                              OrderId = o.OrderId,

                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              UserEmail = u.Email,
                              UserPhone = u.PhoneNumber,
                              OrderLines = (from ol in _db.OrderLines
                                            join b in _db.Books on ol.BookId equals b.BookId
                                            where ol.OrderId == id
                                            select new OrderLineViewModel
                                            {
                                                BookName = b.BookName,
                                                Quantity = ol.Quantity,
                                                ItemTotal = ol.Quantity * ol.Rate,
                                                Rate = ol.Rate
                                            }).ToList()
                          }).SingleOrDefault();
            return orders;

        }



        public async Task<IEnumerable<OrderViewModel>> GetById(int id)
        {
            var order = (from o in _db.Orders
                         join ol in _db.OrderLines on o.OrderId equals ol.OrderId
                         where ol.OrderId == id
                         select new OrderViewModel
                         {
                             OrderId = ol.OrderId,
                             OrderStatus = o.OrderStatus,
                             UserID = o.UserID,
                             OrderDate = o.OrderDate,
                             //Quantity = _db.OrderLines.AsEnumerable().Where(oId => oId.OrderId == id).Sum(q => q.Quantity),
                             Quantity = ol.Quantity,

                         }).ToListAsync();
            return await order;
        }
        public int GetLastId()
        {
            return _db.Orders.Max(a => a.OrderId);
        }

        public async Task<Order> UpdateOrderStatus(int id,Order model)
        {
            var order =await _db.Orders.FindAsync(id);
            order.OrderStatus = model.OrderStatus;
            _db.Entry(order).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return order;
        }

    }
}