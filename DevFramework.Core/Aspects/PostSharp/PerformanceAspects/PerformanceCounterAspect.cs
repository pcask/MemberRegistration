using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.PerformanceAspects
{
    [Serializable]
    public class PerformanceCounterAspect : OnMethodBoundaryAspect
    {
        [NonSerialized]
        private Stopwatch _stopWatch;
        private readonly int _interval;

        public PerformanceCounterAspect(int interval = 2)
        {
            _interval = interval;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            _stopWatch = (Stopwatch)Activator.CreateInstance<Stopwatch>();
            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopWatch.Start();
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _stopWatch.Stop();
            var elapsedTime = _stopWatch.Elapsed.TotalSeconds;
            if (elapsedTime > _interval)
            {
                // Performans log verileri gerçek bir senaryoda bir database'e, dosyaya yazdırılabilir veya ilgili kişilere mail gönderilebilir.
                Debug.WriteLine($"\nPerformance Counter for {args.Method.DeclaringType.FullName}_{args.Method.Name}");
                Debug.WriteLine($"The estimated operating time is {_interval} seconds, but the elapsed time is {elapsedTime} seconds!\n");
            }
            _stopWatch.Reset();

            base.OnExit(args);
        }
    }
}
