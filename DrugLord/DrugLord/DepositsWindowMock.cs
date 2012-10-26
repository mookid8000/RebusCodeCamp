using System.Collections.ObjectModel;

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
}