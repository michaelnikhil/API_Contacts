using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.DataAccess
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly DBContactsContext _context;

        public InMemoryRepository(DBContactsContext context)
        {
            _context = context;
        }
        public T Add(T item)
        {
            try
            {
                T added = (_context.Set<T>().Add(item)).Entity;
                _context.SaveChanges();
                return added;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            _context.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
