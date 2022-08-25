using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess
{
    public interface IQueryableRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> Table { get; }
    }
}
