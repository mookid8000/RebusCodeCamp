using System.ServiceModel;
using SoapPimp.Dtos;

namespace SoapPimp
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Money GetMoney(string secretCode);

        [OperationContract]
        Drugs GetDrugs(string secretCode);
    }
}
