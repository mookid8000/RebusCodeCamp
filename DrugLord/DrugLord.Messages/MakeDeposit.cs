namespace DrugLord.Messages
{
    public class MakeDeposit
    {
        public Money Money { get; set; }
        public Drugs Drugs { get; set; }
        public string SecretCode { get; set; }
    }

    public class Money
    {
        public Money(decimal amount, string transferId)
        {
            Amount = amount;
            TransferId = transferId;
        }

        public decimal Amount { get; private set; }
        public string TransferId { get; private set; }
    }

    public class Drugs
    {
        public Drugs(decimal amount, string transferId)
        {
            Amount = amount;
            TransferId = transferId;
        }

        public decimal Amount { get; private set; }
        public string TransferId { get; private set; }
    }
}