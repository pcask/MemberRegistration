using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Utilities.Common
{
    // Wcf projemizdeki servisleri kullanacak client'lar için proxy üretecek generic nesnemiz;
    public class WcfProxy<T>
    {
        // Bir client'ın Wcf servislerine ulaşması için servisin ABC'sine ihtiyaç duyar. Address-Binding-Contract
        public static T CreateChannel()
        {
            // Servisimizi kullanacak Client'ın (şimdilik Mvc projesini kullanıyoruz) web.config dosyasında AppSettings section'ına
            // ServiceAddress key'ini ve value olarakda servisin adresini placeholder içerecek formatta yazıyoruz.
            // http://localhost:59847/{0}.svc
            // Buradaki amacımız generic bir proxy oluşturmaktır.

            string baseAddress = ConfigurationManager.AppSettings["ServiceAddress"];

            // Gönderilen type bir interface olacağından isimlendirme standardından gelen I harfini çıkarıp
            // placeholder'a argument olarak geçmemiz yeterli olacaktır.
            string address = String.Format(baseAddress, typeof(T).Name.Substring(1));

            var binding = new BasicHttpBinding();

            var channelFactory = new ChannelFactory<T>(binding, address);
            return channelFactory.CreateChannel();
        }
    }
}
