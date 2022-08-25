using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    // IQueryableRepository'nin amacı Context'i sürekli aç-kapat yapmadan tek bir context üzerinden ard arda sorgular yazabilmek ve 
    // sorgumuz nihai haline ulaştığında database'e bu halinin gönderilmesi hedefleniyor.
    // Örnek olarak, OData operasyonlarının gerçekleştirilmesi istendiğinde kullanılabilir.
    public class EfQueryableRepository<TEntity> : IQueryableRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private DbContext _context;
        private IDbSet<TEntity> dbSet;

        public EfQueryableRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Table => dbSet ?? (dbSet = _context.Set<TEntity>());
    }
}
