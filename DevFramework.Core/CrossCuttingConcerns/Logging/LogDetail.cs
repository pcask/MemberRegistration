using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging
{
    // Lognalacak method'ın bilgilerini karşılayacağımız sınıf
    public class LogDetail
    {
        public string FullName { get; set; }       // Namespace bilgisi
        public string MethodName { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }
}
