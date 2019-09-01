using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OBSMVCApi.Models;

namespace OBSMVCApi.DAL
{
    public class PublisherRepository:IRepository<Publisher>
    {
        private ApplicationDbContext _db;
        public PublisherRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<Publisher>> Get()
        {
            return await _db.Publishers.ToListAsync();
        }

        public async Task<IEnumerable<Publisher>> GetActive()
        {
            return await _db.Publishers.Where(a => a.IsActive == true).ToListAsync();
        }
        public async Task<IEnumerable<Publisher>> GetInactive()
        {
            return await _db.Publishers.Where(a => a.IsActive == false).ToListAsync();
        }
        public async Task<Publisher> Get(int id)
        {
            var publisher = await _db.Publishers.FindAsync(id);
            return publisher;
        }


        public async Task<object> Post(Publisher entity)
        {
            _db.Publishers.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(Publisher entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var publisher = await _db.Publishers.FindAsync(id);
            if (publisher!=null)
            {
                _db.Publishers.Remove(publisher);
                await _db.SaveChangesAsync();
                return publisher;
            }

            return null;
        }
        public async Task<object> SoftDelete(int id)
        {
            var publisher = await _db.Publishers.FindAsync(id);

            
            if (publisher != null)
            {
                publisher.IsActive = false;
                await _db.SaveChangesAsync();
                return publisher;
            }
            return null;
        }
        public async Task<object> Put(int id, Publisher entity)
        {
            var publisher = _db.Publishers.Find(id);
            publisher.PublisherName = entity.PublisherName;
            publisher.Address = entity.Address;
            publisher.Books = entity.Books;
            publisher.ContactNo = entity.ContactNo;
            publisher.Email = entity.Email;
         
            await _db.SaveChangesAsync();
            return entity;
        }     
    }
}