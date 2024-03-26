using Clinics.Core.Interfaces;
using Clinics.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ClinicContext _context;
        public GenericRepository(ClinicContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetById(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }
        public void Update(T entity)
        {           
           _context.Entry(entity).State = EntityState.Modified;          
        }
        public async Task Delete(T entity)
        {     
             _context.Remove(entity);                     
        }

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

       
    }
}
