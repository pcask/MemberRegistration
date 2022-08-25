using DevFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.CacheAspects
{
    [Serializable]
    public class CacheRemoveAspect : OnMethodBoundaryAspect
    {
        private readonly Type _cacheType;
        private readonly string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }

        public CacheRemoveAspect(Type cacheType, string pattern) : this(cacheType)
        {
            _pattern = pattern;
        }


        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
                throw new Exception("The cache type used is not an ICacheManager");

            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            base.RuntimeInitialize(method);
        }

        // CacheRemoveAspect'imizi Add, Update, Delete method'larında uygulamalıyız çünkü daha önce GetAll method'ına uyguladığımız CacheAspect'i ile
        // cache'lediğimiz veri listesi artık değişmiş oldu. Eğer bu method'larda işlemler başarılı bir şekilde gerçekleşdiyse var olan cache'i silmek için 
        // OnSeccess method'ını override etmemiz yeterli olacaktır.
        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (String.IsNullOrEmpty(_pattern))
                _cacheManager.RemoveByPattern($"{args.Method.ReflectedType.FullName}.*");
            else
                _cacheManager.RemoveByPattern(_pattern);

            base.OnSuccess(args);
        }
    }
}
