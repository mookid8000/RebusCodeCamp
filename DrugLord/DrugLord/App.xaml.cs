﻿using System;
using System.Configuration;
using System.Timers;
using System.Windows;
using DrugLord.Messages;
using DrugLord.Windows;
using Rebus;
using Rebus.Configuration;
using Rebus.Logging;
using Rebus.Shared;
using Rebus.Transports.Msmq;
using Rebus.MongoDb;

namespace DrugLord
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
        readonly WindowLoggerFactory windowLoggerFactory = new WindowLoggerFactory();

        public event Action<MessageItem> MessageReceived = delegate { };

        public App()
        {
            Startup += Initialize;
            Exit += Destroy;
        }

        void Initialize(object sender, StartupEventArgs e)
        {
            adapter.Handle<string>(str =>
            {
                var messageContext = MessageContext.GetCurrent();
                var returnAddress = messageContext.Headers.ContainsKey(Headers.ReturnAddress)
                                        ? (string) messageContext.Headers[Headers.ReturnAddress]
                                        : "(????)";

                MessageReceived(new MessageItem(returnAddress, str));

                if (messageContext.Headers.ContainsKey(Headers.ReturnAddress))
                {
                    var userToken = Data.Current.GetUserTokenFor(returnAddress);

                    adapter.Bus.Reply(userToken);
                }
            });

            adapter.Register(() => new MakeDepositHandler(adapter.Bus));

            Configure.With(adapter)
                .Logging(l => l.Use(windowLoggerFactory))
                .Transport(t => t.UseMsmqAndGetInputQueueNameFromAppConfig())
                .Subscriptions(s => s.StoreInMongoDb(ConfigurationManager.AppSettings["mongo"], "subscriptions"))
                .CreateBus().Start();

            timer.Elapsed += PossiblySendSecretCode;
            timer.Interval = 1000;
            timer.Start();
        }

        void Destroy(object sender, ExitEventArgs e)
        {
            adapter.Dispose();
        }

        void PossiblySendSecretCode(object sender, ElapsedEventArgs e)
        {
            if (random.Next(SecretCodePublishIntervalAvg * 2) > 0) return;

            var secretCode = Guid.NewGuid().ToString();
            Data.Current.SaveSecretCode(secretCode);

            Log.Info("Generated new code: {0} - publishing it to my minions!", secretCode);

            adapter.Bus.Publish(new SecretCodeHasBeenGenerated(secretCode));
        }

        ILog Log { get { return windowLoggerFactory.GetCurrentClassLogger(); } }
    }
}
