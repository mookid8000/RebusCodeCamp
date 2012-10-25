using System;
using System.Configuration;
using System.ServiceModel;
using MongoDB.Driver;
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

        public Money GetMoney(string secretCode)
        {
            PossiblyVomitJustBecause();
            return new Money();
        }

        public Drugs GetDrugs(string secretCode)
        {
            PossiblyVomitJustBecause();
            return new Drugs();
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
