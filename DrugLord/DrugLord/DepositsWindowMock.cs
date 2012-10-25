using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DrugLord
{
    public class DepositsWindowMock
    {
        public DepositsWindowMock()
        {
            Depositors = new ObservableCollection<Depositor>
                             {
                                 new Depositor{Name = "joe",DrugsAmount = 1230,MoneyAmount = 200,},
                                 new Depositor{Name = "moe",DrugsAmount = 1230,MoneyAmount = 200,},
                                 new Depositor{Name = "jieojfew@jifoejfioe",DrugsAmount = 1230,MoneyAmount = 200,},
                                 new Depositor{Name = "jieojfew@jifoejfioe",DrugsAmount = 2230,MoneyAmount = 200,},
                                 new Depositor{Name = "jieojfew@jifoejfioe",DrugsAmount = 4230,MoneyAmount = 500,},
                                 new Depositor{Name = "jieojfew@jifoejfioe",DrugsAmount = 1230,MoneyAmount = 200,},
                             };
        }

        public ObservableCollection<Depositor> Depositors { get; set; }
    }

    public class Depositor : INotifyPropertyChanged
    {
        string name;
        decimal moneyAmount;
        decimal drugsAmount;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal MoneyAmount
        {
            get { return moneyAmount; }
            set
            {
                moneyAmount = value;
                OnPropertyChanged("MoneyAmount");
            }
        }

        public decimal DrugsAmount
        {
            get { return drugsAmount; }
            set
            {
                drugsAmount = value;
                OnPropertyChanged("DrugsAmount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}