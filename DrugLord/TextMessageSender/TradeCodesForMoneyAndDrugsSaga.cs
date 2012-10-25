using System;
using DrugLord.Messages;
using Rebus;

namespace TextMessageSender
{
    public class TradeCodesForMoneyAndDrugsSaga :
        Saga<DrugMinionSagaData>,
        IAmInitiatedBy<SecretCodeHasBeenGenerated>
    {
        readonly IBus bus;

        public TradeCodesForMoneyAndDrugsSaga(IBus bus)
        {
            this.bus = bus;
        }

        public override void ConfigureHowToFindSaga()
        {
            Incoming<MoneyReply>(m => m.CorrId).CorrelatesWith(d => d.SecretCode);
            Incoming<DrugReply>(m => m.CorrId).CorrelatesWith(d => d.SecretCode);
        }

        public void Handle(SecretCodeHasBeenGenerated message)
        {
            Data.SecretCode = message.Code;

            bus.SendLocal(new DrugRequest { CorrId = message.Code, SecretCode = message.Code });
            bus.SendLocal(new MoneyRequest { CorrId = message.Code, SecretCode = message.Code });
        }
    }

    public class MoneyRequest
    {
        public string CorrId { get; set; }
        public string SecretCode { get; set; }
    }

    public class MoneyReply
    {
        public string CorrId { get; set; }
        public Money Money { get; set; }
    }

    public class DrugRequest
    {
        public string CorrId { get; set; }
        public string SecretCode { get; set; }
    }

    public class DrugReply
    {
        public string CorrId { get; set; }
        public Drugs Drugs { get; set; }
    }

    public class DrugMinionSagaData : ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }

        public string SecretCode { get; set; }
        public Money Money { get; set; }
        public Drugs Drugs { get; set; }
    }
}