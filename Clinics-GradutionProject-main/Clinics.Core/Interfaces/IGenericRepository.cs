using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        //Task Add(Recipe recipe);
        Task Delete(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        
    }
}
