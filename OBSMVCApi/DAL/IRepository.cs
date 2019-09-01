using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using OBSMVCApi.Models;

namespace OBSMVCApi.DAL
{
    public interface IRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(int id);
        Task<object> Post(TEntity entity);
        Task<object> Put(TEntity entity);
        Task<object> Put(int id,TEntity entity);
        Task<object> Delete(int id);
      
    }
}
