using System.ComponentModel;

namespace DrugLord
{
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

        public void TakeValuesFrom(Depositor depositor)
        {
            MoneyAmount = depositor.MoneyAmount;
            DrugsAmount = depositor.DrugsAmount;
        }
    }
}