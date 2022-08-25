using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    public class JsonLayout : LayoutSkeleton
    {
        public override void ActivateOptions()
        {
            // Şuan için herhangi bir işlem yapmamıza gerek yok.
        }

        // LoggingEvent'in içerisinde LogAspect'de oluşturulan MessageObject( Method'ın genel bilgileri ve parametre bilgileri)
        // ve UserName, LoggerName, Level, TimeStamp ve bunun dışında bir çok property barındırmaktadır.
        // Biz sadece bize lazım olanları içerisinden alıp Serialize edebileceğimiz bir nesneye ihtiyaç duyarız. (SerializableLogEvent)
        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logEvent = new SerializableLogEvent(loggingEvent);

            //                                               Standart json formatıdır.
            var json = JsonConvert.SerializeObject(logEvent, Formatting.Indented);
            writer.WriteLine(json);
        }
    }
}
