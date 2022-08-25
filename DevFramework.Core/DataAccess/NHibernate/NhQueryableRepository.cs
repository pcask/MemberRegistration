using DevFramework.Core.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NhQueryableRepository<TEntity> : IQueryableRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly NHibernateHelper _nhHelper;
        private IQueryable<TEntity> _entities;

        public NHibernateHelper NhHelper => _nhHelper;

        public NhQueryableRepository(NHibernateHelper nhHelper)
        {
            _nhHelper = nhHelper;
        }

        public virtual IQueryable<TEntity> Entities => _entities ?? (_entities = _nhHelper.OpenSession().Query<TEntity>());
        public IQueryable<TEntity> Table => this.Entities;

    }
}
