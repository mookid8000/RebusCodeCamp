using System.ComponentModel;
using System.Timers;
using System.Windows;

namespace DrugDealer
{
    /// <summary>
    /// Interaction logic for LogOutputWindow.xaml
    /// </summary>
    public partial class LogOutputWindow : INotifyPropertyChanged
    {
        readonly Timer timer = new Timer();
        readonly WindowLogger logger;
        string logText;

        public LogOutputWindow()
        {
            InitializeComponent();
        }

        public LogOutputWindow(WindowLogger logger) : this()
        {
            this.logger = logger;

            timer.Elapsed += UpdateText;
            timer.Interval = 200;

            DataContext = this;
        }

        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);
            timer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            timer.Stop();
            base.OnClosing(e);
        }

        void UpdateText(object sender, ElapsedEventArgs e)
        {
            LogText = logger.GetText();
        }

        public string LogText
        {
            get { return logText; }
            set
            {
                logText = value;
                OnPropertyChanged("LogText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        void ClearText(object sender, RoutedEventArgs e)
        {
            logger.Clear();
        }
    }
}
