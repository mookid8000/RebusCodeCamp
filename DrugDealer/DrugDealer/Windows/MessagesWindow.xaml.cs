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
        ObservableCollection<string> listBoxItems;

        public MessagesWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        public ObservableCollection<string> ListBoxItems
        {
            get { return listBoxItems; }
            set
            {
                listBoxItems = value;
                OnPropertyChanged("ListBoxItems");
            }
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
                                   new MessageItem("someone", "Hell o world!!!"),
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
