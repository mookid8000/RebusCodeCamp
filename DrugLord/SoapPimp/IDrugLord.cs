using System.ServiceModel;
using SoapPimp.Dtos;

namespace SoapPimp
{
    [ServiceContract]
    public interface IDrugLord
    {
        [OperationContract]
        Money GetMoney(string userToken, string secretCode);

        [OperationContract]
        Drugs GetDrugs(string userToken, string secretCode);
    }
}
