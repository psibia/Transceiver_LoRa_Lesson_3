using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArduinoMessenger.Controls
{
    public partial class MessageBubble : UserControl
    {
        public MessageBubble()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextMessageProperty = DependencyProperty.Register("TextMessage", typeof(string), typeof(MessageBubble), new UIPropertyMetadata(string.Empty));
        public string TextMessage
        {
            get { return (string)GetValue(TextMessageProperty); }
            set { SetValue(TextMessageProperty, value); }
        }

        public static readonly DependencyProperty TimeStampProperty = DependencyProperty.Register("TimeStamp", typeof(string), typeof(MessageBubble), new UIPropertyMetadata(string.Empty));
        public string TimeStamp
        {
            get { return (string)GetValue(TimeStampProperty); }
            set { SetValue(TimeStampProperty, value); }
        }
        public static readonly DependencyProperty PersonNameProperty = DependencyProperty.Register("PersonName", typeof(string), typeof(MessageBubble), new UIPropertyMetadata(string.Empty));
        public string PersonName
        {
            get { return (string)GetValue(PersonNameProperty); }
            set { SetValue(PersonNameProperty, value); }
        }


        public static readonly DependencyProperty SendOutProperty = DependencyProperty.Register("SendOut", typeof(bool), typeof(MessageBubble), new UIPropertyMetadata(false));    
        public bool SendOut
        {
            get { return (bool)GetValue(SendOutProperty); }
            set { SetValue(SendOutProperty, value); }
        }


        public static readonly DependencyProperty MessageBubbleFontSizeProperty = DependencyProperty.Register("MessageBubbleFontSize", typeof(string), typeof(MessageBubble), new PropertyMetadata("", MessageBubbleFontSizeChanged));

        public string MessageBubbleFontSize
        {
            get { return (string)GetValue(MessageBubbleFontSizeProperty); }
            set
            {
                SetValue(MessageBubbleFontSizeProperty, value);
            }
        }
        private static void MessageBubbleFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageBubble instance = d as MessageBubble;
            if (instance == null)
            {
                return;
            }
            instance.lblTimeStamp.Height = (Convert.ToDouble(instance.MessageBubbleFontSize) / 3) + 4;
            instance.lblTimeStamp.FontSize = (Convert.ToDouble(instance.MessageBubbleFontSize) / 3) + 2;
        }
    }
}
