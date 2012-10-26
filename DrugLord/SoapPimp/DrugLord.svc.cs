using System;
using System.Configuration;
using System.ServiceModel;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SoapPimp.Dtos;

namespace SoapPimp
{
    public class DrugLord : IDrugLord
    {
        readonly MongoDatabase database;
        readonly Random random = new Random();
        readonly MongoCollection<BsonDocument> money;
        readonly MongoCollection<BsonDocument> drugs;

        public DrugLord()
        {
            database = MongoDatabase.Create(ConfigurationManager.AppSettings["mongo"]);

            money = database.GetCollection("money");
            money.EnsureIndex(IndexKeys.Ascending("userToken", "secretCode"), IndexOptions.SetUnique(true));

            drugs = database.GetCollection("drugs");
            drugs.EnsureIndex(IndexKeys.Ascending("userToken", "secretCode"), IndexOptions.SetUnique(true));
        }

        public Money GetMoney(string userToken, string secretCode)
        {
            WaitRandomTime();
            PossiblyVomitJustBecause();
            ValidatePresenceOfUserToken(userToken);
            ValidatePresenceOfSecretCode(secretCode);

            try
            {
                var amount = random.Next(70);
                var transferId = Guid.NewGuid().ToString();
                money.Insert(new { userToken, secretCode, amount, transferId }, SafeMode.True);
                return new Money(amount, transferId);
            }
            catch
            {
                throw GetUserTokenAndSecretCodeException(userToken, secretCode, "money");
            }
        }

        public Drugs GetDrugs(string userToken, string secretCode)
        {
            WaitRandomTime();
            PossiblyVomitJustBecause();
            ValidatePresenceOfUserToken(userToken);
            ValidatePresenceOfSecretCode(secretCode);

            try
            {
                var amount = random.Next(80);
                var transferId = Guid.NewGuid().ToString();
                drugs.Insert(new { userToken, secretCode, amount, transferId }, SafeMode.True);
                return new Drugs(amount, transferId);
            }
            catch
            {
                throw GetUserTokenAndSecretCodeException(userToken, secretCode, "drugs");
            }
        }

        static FaultException GetUserTokenAndSecretCodeException(string userToken, string secretCode, string what)
        {
            return new FaultException(string.Format("whoa whoa whoa dude!! you can't use the same secret code twice!!" +
                                                    " The secret code '{0}' has already been used by '{1}' to get {2}!!",
                                                    secretCode, userToken, what));
        }

        void WaitRandomTime()
        {
            Thread.Sleep(TimeSpan.FromSeconds(random.Next(5)));
        }

        void ValidatePresenceOfUserToken(string userToken)
        {
            if (database.GetCollection("userTokens").FindOne(Query.EQ("userToken", userToken)) != null) return;

            throw new FaultException(
                string.Format("User token '{0}' is invalid! Did you really get it by greeting the Drug Lord approproately?",
                    userToken));
        }

        void ValidatePresenceOfSecretCode(string secretCode)
        {
            if (database.GetCollection("secretCodes").FindOne(Query.EQ("code", secretCode)) != null) return;

            throw new FaultException(
                string.Format(
                    "Secret code '{0}' is invalid! Did you really get it by waiting patiently around the Drug Lord, as you should?",
                    secretCode));
        }

        /// <summary>
        /// Go to <see cref="http://www.network-science.de/ascii/"/> for awsumness!
        /// </summary>
        void PossiblyVomitJustBecause()
        {
            if (random.Next(3) > 0) return;

            throw new FaultException(@"
 ____            __                          ____    ______  __      __  __     
/\  _`\         /\ \                        /\  _`\ /\__  _\/\ \  __/\ \/\ \    
\ \ \L\ \     __\ \ \____  __  __    ____   \ \ \L\_\/_/\ \/\ \ \/\ \ \ \ \ \   
 \ \ ,  /   /'__`\ \ '__`\/\ \/\ \  /',__\   \ \  _\/  \ \ \ \ \ \ \ \ \ \ \ \  
  \ \ \\ \ /\  __/\ \ \L\ \ \ \_\ \/\__, `\   \ \ \/    \ \ \ \ \ \_/ \_\ \ \_\ 
   \ \_\ \_\ \____\\ \_,__/\ \____/\/\____/    \ \_\     \ \_\ \ `\___x___/\/\_\
    \/_/\/ /\/____/ \/___/  \/___/  \/___/      \/_/      \/_/  '\/__//__/  \/_/
                                                                                
");
        }
    }
}
