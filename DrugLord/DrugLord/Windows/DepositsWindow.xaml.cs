using System.Collections.ObjectModel;
using System.Timers;
using System.Linq;

namespace DrugLord.Windows
{
    /// <summary>
    /// Interaction logic for DepositsWindow.xaml
    /// </summary>
    public partial class DepositsWindow
    {
        readonly Timer timer = new Timer();

        public ObservableCollection<Depositor> Depositors { get; set; }

        public DepositsWindow()
        {
            Depositors = new ObservableCollection<Depositor>();
            InitializeComponent();
            DataContext = this;
        }

        protected override void OnInitialized(System.EventArgs e)
        {
            timer.Elapsed += delegate { UpdateGraphs(); };
            timer.Interval = 2000;
            timer.Start();

            base.OnInitialized(e);

            UpdateGraphs();
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);

            timer.Stop();
        }

        void UpdateGraphs()
        {
            var newDepositors = Data.Current.GetDepositors().ToList();
            
            var newDepositorNames = newDepositors.Select(d => d.Name).ToArray();
            var currentDepositorNames = Depositors.Select(d => d.Name).ToArray();
            
            var depositorsToRemove = Depositors.Where(d => !newDepositorNames.Contains(d.Name));
            var depositorsToAdd = newDepositors.Where(n => !currentDepositorNames.Contains(n.Name));

            foreach (var depositorToRemove in depositorsToRemove)
            {
                Depositors.Remove(depositorToRemove);
            }

            foreach (var depositorToAdd in depositorsToAdd)
            {
                Depositors.Add(depositorToAdd);
            }

            // update all in-place now
            foreach (var depositor in newDepositors)
            {
                Depositors.Single(d => d.Name == depositor.Name).TakeValuesFrom(depositor);
            }
        }
    }
}
