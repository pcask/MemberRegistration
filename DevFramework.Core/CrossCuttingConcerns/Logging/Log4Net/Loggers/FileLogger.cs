using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class FileLogger : LoggerService
    {
        // log4net.config dosyasında ki JsonFileLogger isimdeki logger bulunacak, içerisinde Log Level (info, warn, debug, exception vb) bilgisi ve
        // Appender ( hangi dosyaya hangi layout kullanılarak log işleminin yapılacağı ) bilgisini barındıran ILog nesnesi base cons.'a gönderilecek.
        public FileLogger() : base(LogManager.GetLogger("JsonFileLogger")) 
        {
           
        }
    }
}
