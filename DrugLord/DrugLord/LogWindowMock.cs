namespace DrugLord
{
    public class LogWindowMock
    {
        public LogWindowMock()
        {
            LogText = @"2012-10-18 20:22:19,136 [5] INFO  DC.WindowsService.WindowsService - Configuring container...
2012-10-18 20:22:20,039 [5] INFO  Rebus.Bus.RebusBus - Rebus bus created
2012-10-18 20:22:20,114 [5] INFO  Rebus.Bus.RebusBus - Initializing bus with 2 workers
2012-10-18 20:22:20,126 [5] INFO  Rebus.Bus.Worker - Worker Rebus 1 worker 1 created and inner thread started
2012-10-18 20:22:20,129 [5] INFO  Rebus.Bus.Worker - Starting worker thread Rebus 1 worker 1
2012-10-18 20:22:20,130 [5] INFO  Rebus.Bus.Worker - Worker Rebus 1 worker 2 created and inner thread started
2012-10-18 20:22:20,130 [5] INFO  Rebus.Bus.Worker - Starting worker thread Rebus 1 worker 2
2012-10-18 20:22:20,130 [5] INFO  Rebus.Bus.RebusBus - Bus started
2012-10-18 20:22:20,150 [5] INFO  DC.RebusExtensions.AutomaticRetry - Automatic retry started - up to 100 messages will be retried from notificationcenter.error every 00:00:30
2012-10-18 20:22:20,164 [5] INFO  DC.WindowsService.WindowsService - Windows service 'd60-NotificationCenter' started
2012-10-18 20:22:52,280 [5] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:23:22,264 [5] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:23:52,294 [4] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:24:22,306 [4] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:24:52,320 [6] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:25:22,321 [4] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:25:52,314 [6] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:26:22,315 [4] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:26:52,303 [6] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:27:22,294 [6] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:27:52,292 [6] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:28:22,281 [6] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
2012-10-18 20:28:52,267 [6] INFO  Rebus.Transports.Msmq.MsmqMessageQueue - Disposing message queue .\private$\notificationcenter.error
";
        }
        public string LogText { get; set; }
    }
}