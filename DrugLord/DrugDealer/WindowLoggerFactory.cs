using Rebus.Logging;

namespace DrugLord
{
    public class WindowLoggerFactory : IRebusLoggerFactory
    {
        readonly LogOutputWindow window;
        readonly WindowLogger logger;

        public WindowLoggerFactory()
        {
            logger = new WindowLogger();
            window = new LogOutputWindow(logger);
            window.Show();
        }

        public ILog GetCurrentClassLogger()
        {
            return logger;
        }
    }
}