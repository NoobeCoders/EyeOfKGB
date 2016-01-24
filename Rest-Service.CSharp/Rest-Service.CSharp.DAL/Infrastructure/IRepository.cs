using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_Service.CSharp.DAL.Infrastructure
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        //Список всех объектов
        IEnumerable<T> GetAll();
           
        //Объект по идентификатору
        T GetById(int id);

        //Добавление объекта
        void Add(T entity);

        //Обновление объекта
        void Update(T entity);

        //Удаление объекта
        void Delete(int id);
    }
}
