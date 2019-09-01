using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using OBSMVCApi.Models;

namespace OBSMVCApi.DAL
{
    public class FeedbackRepository : IRepository<Feedback>
    {
        private ApplicationDbContext _db;
        public FeedbackRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<Feedback>> Get()
        {
            return await _db.Feedbacks.ToListAsync();
        }


        public async Task<Feedback> Get(int id)
        {
            var feedback = await _db.Feedbacks.FindAsync(id);
            return feedback;
        }


        public async Task<object> Post(Feedback entity)
        {
            _db.Feedbacks.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Put(Feedback entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<object> Delete(int id)
        {
            var feedback = await _db.Feedbacks.FindAsync(id);
            if (feedback!=null)
            {
                _db.Feedbacks.Remove(feedback);
                await _db.SaveChangesAsync();
                return feedback;
            }

            return null;
        }
       
        public async Task<object> Put(int id, Feedback entity)
        {
            var feedback = _db.Feedbacks.Find(id);
            feedback.Name = entity.Name;
            feedback.FeedbackDate = entity.FeedbackDate;
            feedback.Email = entity.Email;
            feedback.Comments = entity.Comments;
            
          
            await _db.SaveChangesAsync();
            return entity;
        }

      
    }
}