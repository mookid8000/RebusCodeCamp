using System.Runtime.Serialization;

namespace SoapPimp.Dtos
{
    [DataContract]
    public class Drugs
    {
        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string TransferId { get; set; }

        public Drugs(decimal amount, string transferId)
        {
            Amount = amount;
            TransferId = transferId;
        }
    }
}