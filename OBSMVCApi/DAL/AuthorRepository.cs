using OBSMVCApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace OBSMVCApi.DAL
{
    public class AuthorRepository : IRepository<Author>
    {
       public ApplicationDbContext context;
        public AuthorRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<Author>> Get()
        {
            return await context.Authors
                .Include(a=>a.Books).ToListAsync();
        }
        public async Task<IEnumerable<Author>> GetActive()
        {
            return await context.Authors.Where(a => a.IsActive == true).ToListAsync();
        }
        public async Task<IEnumerable<Author>> GetInactive()
        {
            return await context.Authors.Where(a => a.IsActive == false).ToListAsync();
        }
        public async Task<Author> Get(int id)
        {
            var data= await context.Authors.FindAsync(id);
            return data;
        }

        public async Task<object> Post(Author entity)
        {
            entity.IsActive = true;
            context.Authors.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }


        public async Task<object> Put(Author entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }


        public async Task<object> Delete(int id)
        {
            var author = context.Authors.Find(id);
            if (author != null)
            {
                context.Authors.Remove(author);
                await context.SaveChangesAsync();
                return author;
            }
            return null;
        }

        public async Task<object> SoftDelete(int id)  
        {
            var author = context.Authors.Find(id);
            if (author != null)
            {
                author.IsActive = false;
                
                await context.SaveChangesAsync();
                return author;
            }
            return null;
        }

        public async Task<object> Put(int id, Author entity)
        {
            var author = context.Authors.Find(id);
            author.AuthorName = entity.AuthorName;
            author.AuthorInfo= entity.AuthorInfo;
            author.Address = entity.Address;
            author.Email = entity.Email;
            author.DoB = entity.DoB;
            author.ContactNo = entity.ContactNo;
            author.Books = entity.Books;
            author.ImageUrl = entity.ImageUrl;
            await context.SaveChangesAsync();
            return entity;
        } 
    }
}