using MemberRegistration.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.Business.ServiceAdapters.Kps
{
    // Bu sınıfın amacı tükettiğimiz servise olan bağımlılığı ortadan kaldırmaktır. Örneğin test süreçlerimizde gerçek bir servisi tüketmeden,
    // servisin yapacağı işten ziyade kendi kodumuzun yapacağı işe hızlı bir şekilde odaklanmamızı sağlayacaktır.
    // Servisin çalışmaması, yavaş çalışması, güncellenmesi, sonlanması gibi senaryolara karşılık geçici çözümler üretebileceğimiz
    // sınıf olarak da ele alınaiblir. 
    public class MockAdapter : IKpsService
    {
        public bool Validate(Member member)
        {
            return true;
        }
    }
}
