using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    // Helper sınıfını yazmamızın amacı NHibernate'in bir çok Database'i destekliyor olması.
    public abstract class NHibernateHelper : IDisposable
    {
        // EntityFramework'deki context'in karşılığı olarak düşünülebilir.
        private ISessionFactory _sessionFactory;

        // Virtual tanımlamamızın amacı ORM'araçlarının lazy loading yapabilmesi, navigation property kullanabilmesini sağlamak.
        public virtual ISessionFactory SessionFactory => _sessionFactory ?? (_sessionFactory = InitializeFactory());

        // InitializeFactory method'ını abstract olarak belirliyoruz çünkü ISessionFactory kullanılacak Database'e göre initialize edilmelidir.
        protected abstract ISessionFactory InitializeFactory();

        //Oluşturulan Session'ın açılması gerekiyor.
        public virtual ISession OpenSession() => SessionFactory.OpenSession();



        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _sessionFactory?.Dispose();
                    SessionFactory?.Dispose();
                    SessionFactory?.Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~NHibernateHelper()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
