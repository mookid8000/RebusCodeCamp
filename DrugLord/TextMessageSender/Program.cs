using System;
using Rebus.Configuration;
using Rebus.Transports.Msmq;

namespace TextMessageSender
{
    class Program
    {
        static void Main()
        {
            using(var adapter = new BuiltinContainerAdapter())
            {
                Configure.With(adapter)
                    .Transport(t => t.UseMsmqAndGetInputQueueNameFromAppConfig())
                    .DetermineEndpoints(d => d.FromRebusConfigurationSection())
                    .CreateBus().Start();

                while(true)
                {
                    Console.Write("> ");
                    var text = Console.ReadLine();
                    if (string.IsNullOrEmpty(text)) break;
                    adapter.Bus.Send(text);
                }
            }
        }
    }
}
