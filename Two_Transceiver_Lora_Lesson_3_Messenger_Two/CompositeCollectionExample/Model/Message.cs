using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArduinoMessenger.Model
{
    public class Message
    {
        public string TextMessage { set; get; }
        public string ReceivedTime { set; get; }
        public string Person { get; set; }
        public bool SendOut { set; get; }

        public static ObservableCollection<Message> GetInboundMessages(string text, string dt,string person, bool SendOut)
        {
            ObservableCollection<Message> Messages = new ObservableCollection<Message>();
            Messages.Add(new Message {TextMessage = text, ReceivedTime = dt, Person = person, SendOut = SendOut });
            return Messages;
        }
    }
}
