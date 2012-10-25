using System;
using TestSoapPimp.ServiceReference1;

namespace TestSoapPimp
{
    class Program
    {
        static void Main()
        {
            var client = new DrugLordClient();
            try
            {
                var money = client.GetMoney("textMessageClient.input@MHG-PC/9acc4b", "2");
                
                Console.WriteLine("Yay, got money: {0} (transferId: {1})", money.Amount, money.TransferId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
