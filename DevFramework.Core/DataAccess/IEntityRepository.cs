using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess
{
    // T generic tipine where ile bazı kısıtlamalar getirelim;
    // class --> sadece reference tip gönderilebilir,
    // IEntity --> sadece IEntity interface'ini implement etmiş nesne gönderilebilir,
    // new() --> sadece örneklenebilir bir nesne gönderilebilir. (interface veya abstract class gönderilemez.)
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        // Geriye tüm veya gönderilen filter'a göre istenilen data'yı döndürecek;
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
