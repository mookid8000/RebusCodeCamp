using System;
using System.Configuration;
using System.ServiceModel;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SoapPimp.Dtos;

namespace SoapPimp
{
    public class Service1 : IService1
    {
        readonly MongoDatabase database;
        readonly Random random = new Random();

        public Service1()
        {
            database = MongoDatabase.Create(ConfigurationManager.AppSettings["mongo"]);
        }

        public Money GetMoney(string userToken, string secretCode)
        {
            PossiblyVomitJustBecause();
            ValidatePresenceOfUserToken(userToken);
            return new Money();
        }

        public Drugs GetDrugs(string userToken, string secretCode)
        {
            PossiblyVomitJustBecause();
            ValidatePresenceOfUserToken(userToken);
            return new Drugs();
        }

        void ValidatePresenceOfUserToken(string userToken)
        {
            if (database.GetCollection("userTokens").FindOne(Query.EQ("userToken", userToken)) != null) return;

            throw new FaultException(
                string.Format("User token '{0}' is invalid! Did you really get it by greeting the Drug Lord approproately?",
                    userToken));
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
