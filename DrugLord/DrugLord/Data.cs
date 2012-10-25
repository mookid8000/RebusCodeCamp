using System;
using System.Configuration;
using DrugLord.Messages;
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
            database.GetCollection("secretCodes").Insert(new { code = secretCode });
        }

        public string GetUserTokenFor(string endpoint)
        {
            var userTokens = database.GetCollection("userTokens");
            userTokens.EnsureIndex(IndexKeys.Ascending("endpoint"), IndexOptions.SetUnique(true));

            var existingUserToken = userTokens.FindOne(Query.EQ("endpoint", endpoint));

            if (existingUserToken == null)
            {
                var userToken = endpoint + "/" + Guid.NewGuid().ToString().Substring(0, 6);
                userTokens.Insert(new { endpoint, userToken }, SafeMode.True);
                database.GetCollection("stash").Insert(new {depositor = endpoint});
                return userToken;
            }

            return existingUserToken["userToken"].AsString;
        }

        public void DepositMoney(string depositor, Money money)
        {
            if (database.GetCollection("money").FindOne(Query.And(Query.EQ("transferId", money.TransferId), Query.EQ("amount", (double)money.Amount))) == null)
            {
                throw new InvalidOperationException(string.Format("Money transfer ID '{0}' is invalid!", money.TransferId));
            }

            var criteria = Query.And(Query.EQ("depositor", depositor), Query.NE("money.transfers", money.TransferId));
            var operation = Update
                .AddToSet("money.transfers", money.TransferId)
                .Inc("money.amount", (double) money.Amount);

            var safeModeResult = database.GetCollection("stash").Update(criteria, operation, SafeMode.True);

            if (safeModeResult.DocumentsAffected == 0)
            {
                throw new InvalidOperationException(string.Format("Could not deposit money for '{0}' (transfer ID '{1}')", depositor, money.TransferId));
            }
        }

        public void DepositDrugs(string depositor, Drugs drugs)
        {
            if (database.GetCollection("drugs").FindOne(Query.And(Query.EQ("transferId", drugs.TransferId), Query.EQ("amount", (double)drugs.Amount))) == null)
            {
                throw new InvalidOperationException(string.Format("Drug transfer ID '{0}' is invalid!", drugs.TransferId));
            }

            var criteria = Query.And(Query.EQ("depositor", depositor), Query.NE("drugs.transfers", drugs.TransferId));
            var operation = Update
                .AddToSet("drugs.transfers", drugs.TransferId)
                .Inc("drugs.amount", (double)drugs.Amount);

            var safeModeResult = database.GetCollection("stash").Update(criteria, operation, SafeMode.True);

            if (safeModeResult.DocumentsAffected == 0)
            {
                throw new InvalidOperationException(string.Format("Could not deposit money for '{0}' (transfer ID '{1}')", depositor, drugs.TransferId));
            }
        }
    }
}