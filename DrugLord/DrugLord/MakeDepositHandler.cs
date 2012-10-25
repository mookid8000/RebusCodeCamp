using DrugLord.Messages;
using MongoDB.Driver;
using Rebus;

namespace DrugLord
{
    public class MakeDepositHandler : IHandleMessages<MakeDeposit>, IHandleMessages<DepositMoney>, IHandleMessages<DepositDrugs>
    {
        readonly IBus bus;

        public MakeDepositHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(MakeDeposit message)
        {
            var depositor = MessageContext.GetCurrent().ReturnAddress;
            bus.SendLocal(new DepositMoney(message.Money, depositor));
            bus.SendLocal(new DepositDrugs(message.Drugs, depositor));
        }

        public void Handle(DepositMoney message)
        {
            Data.Current.DepositMoney(message.Depositor, message.Money);
        }

        public void Handle(DepositDrugs message)
        {
            Data.Current.DepositDrugs(message.Depositor, message.Drugs);
        }
    }

    public class DepositMoney
    {
        public Money Money { get; set; }
        public string Depositor { get; set; }

        public DepositMoney(Money money, string depositor)
        {
            Money = money;
            Depositor = depositor;
        }
    }

    public class DepositDrugs
    {
        public Drugs Drugs { get; set; }
        public string Depositor { get; set; }

        public DepositDrugs(Drugs drugs, string depositor)
        {
            Drugs = drugs;
            Depositor = depositor;
        }
    }
}