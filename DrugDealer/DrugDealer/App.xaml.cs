using System;
using DrugDealer.Windows;
using Rebus;
using Rebus.Configuration;
using Rebus.Shared;
using Rebus.Transports.Msmq;

namespace DrugDealer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        BuiltinContainerAdapter adapter;

        public event Action<MessageItem> MessageReceived = delegate { };

        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            adapter = new BuiltinContainerAdapter();

            adapter.Handle<string>(str =>
                                       {
                                           var messageContext = MessageContext.GetCurrent();
                                           var sender = messageContext.Headers.ContainsKey(Headers.ReturnAddress)
                                                            ? (string) messageContext.Headers[Headers.ReturnAddress]
                                                            : "(????)";
                                           
                                           MessageReceived(new MessageItem(sender, str));
                                       });

            Configure.With(adapter)
                .Logging(l => l.Use(new WindowLoggerFactory()))
                .Transport(t => t.UseMsmqAndGetInputQueueNameFromAppConfig())
                .CreateBus().Start();

            base.OnStartup(e);
        }

        protected override void OnExit(System.Windows.ExitEventArgs e)
        {
            base.OnExit(e);
            adapter.Dispose();
        }
    }
}
