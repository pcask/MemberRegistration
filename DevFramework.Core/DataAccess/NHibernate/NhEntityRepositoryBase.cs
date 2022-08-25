using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NhEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {

        private readonly NHibernateHelper _nhHelper;

        public NhEntityRepositoryBase(NHibernateHelper helper)
        {
            _nhHelper = helper;
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            //  EF'deki örnek kullanım;
            //  using (var context = new TContext())
            //  {
            //      return filter == null ? context.Set<TEntity>().ToList()
            //                            : context.Set<TEntity>().Where(filter).ToList();
            //  }


            // Aynı EF'deki context kullanımı gibi burada da session kullanıyor olacağız.
            using (var session = _nhHelper.OpenSession())
            {
                return filter == null ? session.Query<TEntity>().ToList()
                                      : session.Query<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var session = _nhHelper.OpenSession())
            {
                return session.Query<TEntity>().FirstOrDefault(filter);
            }
        }

        public TEntity Add(TEntity entity)
        {
            //  EF'deki örnek kullanım;
            //  using (var context = new TContext())
            //  {
            //      context.Entry(entity).State = EntityState.Added;
            //      context.SaveChanges();
            //      return entity;
            //  }

            using (var session = _nhHelper.OpenSession())
            {
                session.Save(entity);
                return entity;
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var session = _nhHelper.OpenSession())
            {
                session.Update(entity);
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var session = _nhHelper.OpenSession())
            {
                session.Delete(entity);
            }
        }

    }
}
