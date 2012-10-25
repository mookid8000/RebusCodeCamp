using System.Configuration;
using MongoDB.Driver;

namespace DrugDealer
{
    public class Data
    {
        static readonly Data CurrentData = new Data();
        readonly MongoDatabase database;

        public static Data Current
        {
            get { return CurrentData; }
        }

        public Data()
        {
            database = MongoDatabase.Create(ConfigurationManager.AppSettings["mongo"]);
        }

        public void SaveSecretCode(string secretCode)
        {
            database.GetCollection("secretCodes").Insert(new {code = secretCode});
        }
    }
}