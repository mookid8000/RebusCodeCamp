using System.Collections.ObjectModel;

namespace DrugLord
{
    public class DepositsWindowMock
    {
        public DepositsWindowMock()
        {
            Depositors = new ObservableCollection<Depositor>
                             {
                                 new Depositor {Name = "joe", DrugsAmount = 123, MoneyAmount = 200,},
                                 new Depositor {Name = "moe", DrugsAmount = 230, MoneyAmount = 200,},
                                 new Depositor {Name = "jieojfew@jifoejfioe", DrugsAmount = 230, MoneyAmount = 200,},
                                 new Depositor {Name = "jieojfew@jifoejfioe", DrugsAmount = 130, MoneyAmount = 200,},
                                 new Depositor {Name = "jieojfew@jifoejfioe", DrugsAmount = 430, MoneyAmount = 500,},
                                 new Depositor {Name = "jieojfew@jifoejfioe", DrugsAmount = 10, MoneyAmount = 200,},
                             };
        }

        public ObservableCollection<Depositor> Depositors { get; set; }
    }
}