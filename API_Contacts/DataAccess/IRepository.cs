using System.Collections.Generic;


namespace API_Contacts.DataAccess
{
    /// <summary>
    /// Interface for the CRUD operations
    /// </summary>
    public interface IRepository<T> where T : class
    {
        T Add(T item);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Delete(T item);
        void Update(T item);

    }
}
