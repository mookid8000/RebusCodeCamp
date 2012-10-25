using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace DrugDealer.Windows
{
    /// <summary>
    /// Interaction logic for MessagesWindow.xaml
    /// </summary>
    public partial class MessagesWindow : INotifyPropertyChanged
    {
        readonly ObservableCollection<MessageItem> listBoxItems = new ObservableCollection<MessageItem>();

        public MessagesWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ((App)Application.Current).MessageReceived += MessageReceived;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            ((App)Application.Current).MessageReceived -= MessageReceived;
        }

        void MessageReceived(MessageItem messageItem)
        {
            Dispatcher.Invoke(() => AddItem(messageItem));
        }

        void AddItem(MessageItem messageItem)
        {
            listBoxItems.Insert(0, messageItem);
        }

        public ObservableCollection<MessageItem> ListBoxItems
        {
            get { return listBoxItems; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MessagesWindowMock
    {
        public MessagesWindowMock()
        {
            ListBoxItems = new ObservableCollection<MessageItem>
                               {
                                   new MessageItem("someone", "Hell o world!!!"),
                                   new MessageItem("someone.else", "Hell o world!!!"),
                                   new MessageItem("just.some.sender", "Hell o world!!!"),
                                   new MessageItem("someone", "Hello there, how u doin?"),
                                   new MessageItem("someone", "Hell o world!!!"),
                                   new MessageItem("someone", "Hell o world!!! This is a very long text, at least compared to the other texts.... Hell o world!!! This is a very long text, at least compared to the other texts.... Hell o world!!! This is a very long text, at least compared to the other texts...."),
                                   new MessageItem("someone", "Hell o world!!!"),
                                   new MessageItem("someone", "Hell o world!!!"),
                                   new MessageItem("someone", "Hell o world!!!"),
                               };
        }

        public ObservableCollection<MessageItem> ListBoxItems { get; set; }
    }

    public class MessageItem
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }

        public MessageItem(string sender, string message)
        {
            Sender = sender;
            Message = message;
            var timeOfDay = DateTime.Now.TimeOfDay;
            Time = string.Format("{0:00}:{1:00}", timeOfDay.Hours, timeOfDay.Minutes);
        }
    }
}
