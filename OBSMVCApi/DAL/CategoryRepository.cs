using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OBSMVCApi.Models;

namespace OBSMVCApi.DAL
{
    public class CategoryRepository : IRepository<Category>
    {
        ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetActive()
        {
            return await _db.Categories.Where(a => a.IsActive == true).ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetInactive()
        {
            return await _db.Categories.Where(a => a.IsActive == false).ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            return category;
        }
        public async Task<Category> GetByName(string name)
        {
            var category = await _db.Categories.Where(c=>c.CategoryName==name).FirstOrDefaultAsync();
            return category;
        }


        public async Task<object> Post(Category entity)
        {
            if (_db.Categories.Any(c => c.CategoryName == entity.CategoryName))
            {
                return null;
            }
            entity.IsActive = true;
            _db.Categories.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(Category entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }


        public async Task<object> Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return category;
            }

            return null;
        }
        public async Task<object> SoftDelete(int id)
        {
            var category = await _db.Categories.FindAsync(id);


            if (category != null)
            {
                category.IsActive = false;
                await _db.SaveChangesAsync();
                return category;
            }
            return null;
        }
        public async Task<object> Put(int id, Category entity)
        {

            var category = _db.Categories.Find(id);
            category.CategoryName = entity.CategoryName;
            category.Books = entity.Books;            
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}