using Rebus.Configuration;
using Rebus.Transports.Msmq;

namespace DrugDealer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        BuiltinContainerAdapter adapter;

        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            adapter = new BuiltinContainerAdapter();

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
