using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Add(string key, object data, int cacheTimeAsMinute);
        bool IsAdd(string key); // Cache ekli mi değil mi?
        void Remove(string key);
        void RemoveByPattern(string pattern); // Belirtilen pattern'a uygun key'lere sahip cache'leri sil.
        void Clear();
    }
}
