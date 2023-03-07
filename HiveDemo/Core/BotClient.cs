using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiveDemo.Core
{
    public class BotClient
    {
        private string className;
        private CancellationTokenSource cancellationTokenSource;

        public BotClient(string className)
        {
            this.className = className;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            Task.Factory.StartNew(() => RunLoop(cancellationTokenSource.Token));
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
        }

        private void RunLoop(CancellationToken cancellationToken)
        {
            Type type = Type.GetType(className);
            object obj = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("Execute");

            while (!cancellationToken.IsCancellationRequested)
            {


                method.Invoke(obj, null);
            }
        }
    }
}
