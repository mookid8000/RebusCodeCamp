using System;
using System.Configuration;
using System.Timers;
using DrugDealer.Messages;
using DrugDealer.Windows;
using Rebus;
using Rebus.Configuration;
using Rebus.Shared;
using Rebus.Transports.Msmq;
using Rebus.MongoDb;

namespace DrugDealer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        const int SecretCodePublishIntervalAvg = 5;
        readonly BuiltinContainerAdapter adapter = new BuiltinContainerAdapter();
        readonly Timer timer = new Timer();
        readonly Random random = new Random();

        public event Action<MessageItem> MessageReceived = delegate { };

        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
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
                .Subscriptions(s => s.StoreInMongoDb(ConfigurationManager.AppSettings["mongo"], "subscriptions"))
                .CreateBus().Start();

            timer.Elapsed += PossiblySendSecretCode;
            timer.Interval = 1000;

            base.OnStartup(e);
        }

        void PossiblySendSecretCode(object sender, ElapsedEventArgs e)
        {
            if (random.Next(SecretCodePublishIntervalAvg*2) > 0) return;

            adapter.Bus.Publish(new SecretCodeHasBeenGenerated(Guid.NewGuid().ToString()));
        }

        protected override void OnExit(System.Windows.ExitEventArgs e)
        {
            adapter.Dispose();

            base.OnExit(e);
        }
    }
}
