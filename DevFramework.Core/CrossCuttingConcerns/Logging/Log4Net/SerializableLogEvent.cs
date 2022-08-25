using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    // Logları Json formatında tutabilmemiz için log nesnelerimizin Serializable niteliğine sahip olması gerekir.
    [Serializable]
    public class SerializableLogEvent
    {
        private readonly LoggingEvent _loggingEvent;

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        public string UserName => _loggingEvent.UserName;
        public string Date => _loggingEvent.TimeStamp.ToString("G");
        public object MessageObject => _loggingEvent.MessageObject;
    }
}
