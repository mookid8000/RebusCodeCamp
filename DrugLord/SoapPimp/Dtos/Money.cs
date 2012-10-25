using System.Runtime.Serialization;

namespace SoapPimp.Dtos
{
    [DataContract]
    public class Money
    {
        [DataMember]
        public decimal Amount { get; set; }

        public Money(decimal amount)
        {
            Amount = amount;
        }
    }
}