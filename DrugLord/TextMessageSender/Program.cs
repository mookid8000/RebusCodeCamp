using System;
using DrugLord.Messages;
using Rebus;
using Rebus.Configuration;
using Rebus.Transports.Msmq;
using TextMessageSender.ServiceReference1;
using Drugs = DrugLord.Messages.Drugs;
using Money = DrugLord.Messages.Money;

namespace TextMessageSender
{
    class Program
    {
        static void Main()
        {
            using(var adapter = new BuiltinContainerAdapter())
            {
                adapter.Register(() => new TradeCodesForMoneyAndDrugsSaga(adapter.Bus));
                adapter.Register(() => new WebServiceHandler(adapter.Bus));

                Configure.With(adapter)
                    .Transport(t => t.UseMsmqAndGetInputQueueNameFromAppConfig())
                    .DetermineEndpoints(d => d.FromRebusConfigurationSection())
                    .CreateBus().Start();

                adapter.Bus.Subscribe<SecretCodeHasBeenGenerated>();

                while(true)
                {
                    Console.Write("> ");
                    var text = Console.ReadLine();
                    if (string.IsNullOrEmpty(text)) break;
                    adapter.Bus.Send(text);
                }
            }
        }
    }

    class WebServiceHandler : IHandleMessages<MoneyRequest>, IHandleMessages<DrugRequest>
    {
        readonly IBus bus;

        public WebServiceHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(MoneyRequest message)
        {
            var client = new DrugLordClient();
            var money = client.GetMoney("textMessageClient.input@MHG-PC/c1a882", message.SecretCode);
            bus.Reply(new MoneyReply {CorrId = message.CorrId, Money = new Money(money.Amount, money.TransferId)});
        }

        public void Handle(DrugRequest message)
        {
            var client = new DrugLordClient();
            var drugs = client.GetDrugs("textMessageClient.input@MHG-PC/c1a882", message.SecretCode);
            bus.Reply(new DrugReply() { CorrId = message.CorrId,Drugs=new Drugs(drugs.Amount, drugs.TransferId) });
        }
    }
}
