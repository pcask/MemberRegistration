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
    public class CacheAspect : MethodInterceptionAspect // OnMethodBoundaryAspect' sınıfını kullanmıyoruz çünkü method'a girilmeden önce işlem yapmak istiyoruz.
    {
        private readonly Type _cacheType;
        private readonly int _cacheTimeAsMinute;
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheTimeAsMinute = 60)
        {
            _cacheType = cacheType;
            _cacheTimeAsMinute = cacheTimeAsMinute;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("The cache type used is not an ICacheManager");
            }

            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            base.RuntimeInitialize(method);
        }

        // OnEntry method'ı method çağrıldıktan sonraki çalışacak ilk kod bloklarının yerleşeceği yer olarak ele alınabilir,
        // ancak biz daha method çağrılmadan önce işlem yapmak istiyorsak MethodInterceptionAspect' sınıfının OnInvoke method'ını override edebiliriz.
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = $"{args.Method.ReflectedType.FullName}_{args.Method.Name}";

            var arguments = args.Arguments.ToList();

            var key = $"{methodName}({String.Join(",", arguments.Select(a => a != null ? a.ToString() : "<Null>"))})";

            if (_cacheManager.IsAdd(key) == false) // Key daha önce eklenmemişse ilgili method çağrılır ve ardından ReturnValue değeri Cache'e eklenir.
            {
                base.OnInvoke(args);
                _cacheManager.Add(key, args.ReturnValue, _cacheTimeAsMinute);
            }
            else // Eğer key Cache'de varsa ilgili method'a çağrı yapılmadan sadece ReturnValue değerine Cache'deki o key'e karşılık gelen data atanacaktır.
            {
                args.ReturnValue = _cacheManager.Get<object>(key);
            }
        }
    }
}
