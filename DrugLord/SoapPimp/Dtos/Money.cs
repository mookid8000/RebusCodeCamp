using System.Runtime.Serialization;

namespace SoapPimp.Dtos
{
    [DataContract]
    public class Money
    {
        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string TransferId { get; set; }

        public Money(decimal amount, string transferId)
        {
            Amount = amount;
            TransferId = transferId;
        }
    }
}