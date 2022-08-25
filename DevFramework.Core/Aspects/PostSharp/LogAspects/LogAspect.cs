using DevFramework.Core.CrossCuttingConcerns.Logging;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.LogAspects
{
    [Serializable]
    // Aspect'timizi sınıf seviyesinde uygulamak istersek, sadece sınıf method'larını ( constructor method'lar hariç ) hedeflemek için;
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    public class LogAspect : OnMethodBoundaryAspect
    {
        private readonly Type _loggerType;
        private LoggerService _loggerService;
        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType != typeof(LoggerService))
            {
                throw new Exception("The logger type used is not a LoggerService");
            }

            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);

            base.RuntimeInitialize(method);
        }

        // Method bilgilerinin loglanmasını ele alıcak olursak OnEntry method'ını override etmemiz yeterli olacaktır.
        public override void OnEntry(MethodExecutionArgs args)
        {
            // Info değerlerinin loglanıp loglanmayacağı kontrolüni yapalım.
            if (_loggerService.IsInfoEnabled != true)
                return;

            try
            {
                // GetParameters method'ı bize ParameterInfo[] dönecektir ve "p" her bir ParameterInfo nesnesine denk gelirken
                // "i" ise dizi iterasyonu sağlamak için kullanılır.
                var logParameters = args.Method.GetParameters().Select((p, i) => new LogParameter
                {
                    Name = p.Name,
                    Type = p.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i) // i'ninci parametreye geçilen değer (Argument parametreye geçilen değeri ifade etmektedir.)
                }).ToList();

                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType?.FullName,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };

                _loggerService.Info(logDetail);
            }
            catch (Exception)
            {
                // Loglama işlemi başarız olma durumunda yapılması gereken işlemler yer alacak.
            }

        }
    }
}
