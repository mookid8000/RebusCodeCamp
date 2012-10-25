using System;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DrugLord
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

        public string GetUserTokenFor(string endpoint)
        {
            var userTokens = database.GetCollection("userTokens");
            userTokens.EnsureIndex(IndexKeys.Ascending("endpoint"), IndexOptions.SetUnique(true));

            var existingUserToken = userTokens.FindOne(Query.EQ("endpoint", endpoint));

            if (existingUserToken == null)
            {
                var userToken = endpoint + "/" + Guid.NewGuid().ToString().Substring(0, 6);
                userTokens.Insert(new {endpoint, userToken});
                return userToken;
            }

            return existingUserToken["userToken"].AsString;
        }
    }
}