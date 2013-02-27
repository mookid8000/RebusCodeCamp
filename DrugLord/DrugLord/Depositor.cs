using System;
using System.ComponentModel;

namespace DrugLord
{
    public class Depositor : INotifyPropertyChanged
    {
        string name;
        decimal moneyAmount;
        decimal drugsAmount;
        DateTime lastUpdated;

        public Depositor()
        {
            lastUpdated = DateTime.Now;
        }

        public DateTime LastUpdated
        {
            get { return lastUpdated; }
        }

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
                OnPropertyChanged("MoneyBarWidth");
            }
        }

        public decimal MoneyBarWidth
        {
            get { return MoneyAmount; }
        }

        public decimal DrugsAmount
        {
            get { return drugsAmount; }
            set
            {
                drugsAmount = value;
                OnPropertyChanged("DrugsAmount");
                OnPropertyChanged("DrugsBarWidth");
            }
        }

        public decimal DrugsBarWidth
        {
            get { return DrugsAmount; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void TakeValuesFrom(Depositor depositor)
        {
            if (MoneyAmount == depositor.MoneyAmount
                && DrugsAmount == depositor.DrugsAmount) return;

            MoneyAmount = depositor.MoneyAmount;
            DrugsAmount = depositor.DrugsAmount;

            lastUpdated = DateTime.Now;
            OnPropertyChanged("LastUpdated");
        }
    }
}