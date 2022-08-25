using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevFramework.Core.Utilities.Mvc.Infrastructure
{
    // Mvc'de controller'ları oluşturan DefaultControllerFactory sınıfından kalıtım alarak NinjectControllerFactory sınıfımızı oluşturuyoruz.
    // Bu sınıfı Global.asax dosyasında ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()))
    // Şeklinde set ediyoruz ve artık controller'larımızın Dependency Injection ile istemiş oldukları nesneleri çözümleyip inject edebiliyoruz.
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;

        // MvcWebUI projesindeki Global.asax dosyasından gönderilen tüm NinjectModule nesnelerimizle oluşturacağımız "kernel", module'lar
        // içerisindeki tanımlı tüm çözümlemeleri yapabilecektir.
        public NinjectControllerFactory(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //_kernel.Get(Type service) kullanımı ile parametre geçilen controller tipinde bir nesne oluşturulacaktır,
            // eğer gelen controller tipinde parametreli bir constructor varsa bunun çözümlemesini _kernel yapacak ve Get method'ı ile
            // geriye kernel'ın sahip olduğu çözümlemelere (modules) dayanarak controller'ın tüm injection gereksinimleri karşılanacak ve geri gönderilecektir. 
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}
