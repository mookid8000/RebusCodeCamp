using System.Runtime.Serialization;

namespace SoapPimp.Dtos
{
    [DataContract]
    public class Drugs
    {
        [DataMember]
        public decimal Amount { get; set; }

        public Drugs(decimal amount)
        {
            Amount = amount;
        }
    }
}