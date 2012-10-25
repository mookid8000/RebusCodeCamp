using System.Windows;
using DrugDealer.Windows;

namespace DrugDealer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void ShowMessagesWindow(object sender, RoutedEventArgs e)
        {
            var window = new MessagesWindow();
            window.Show();
            window.WindowState = WindowState.Maximized;
        }
    }
}
