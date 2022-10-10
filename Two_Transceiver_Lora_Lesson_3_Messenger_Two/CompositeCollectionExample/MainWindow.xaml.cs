using ArduinoMessenger.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO.Ports;
using System.Threading;
using ArduinoMessenger.Controls;


namespace ArduinoMessenger
{   
    using ArduinoMessenger.Model;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort sp = new SerialPort();
        string[] ports = SerialPort.GetPortNames();
        CompositeCollection compositeCollection = new CompositeCollection();

        public MainWindow()
        {
            InitializeComponent();
            COM.ItemsSource = ports;
            sp.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }
        private void COM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sp.IsOpen)
                    sp.Close();
                sp.PortName = COM.SelectedItem as string;
                sp.BaudRate = 9600;
                sp.Encoding = Encoding.UTF8;
                sp.DataBits = 8;
                sp.Parity = Parity.None;
                sp.StopBits = StopBits.One;
                sp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(200);
            
                Dispatcher.Invoke(() => 
                {
                    try
                    {
                        string message = sp.ReadExisting();
                        string name = message.Substring(0, message.IndexOf('\n'));
                        message = message.Remove(0, message.IndexOf('\n'));


                        Initialize(message, DateTime.Now.ToString("HH:mm"), name, true);
                        ScrollMessage.ScrollToEnd();
                        System.Media.SystemSounds.Beep.Play();
                    }
                    catch { }
                });
            
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sp.Write(UserName.Text + "\n" +TextOut.Text + "\n");
            Initialize(TextOut.Text, DateTime.Now.ToString("HH:mm"), "Вы", false);
            TextOut.Clear();
            TextOut.Focus();
            ScrollMessage.ScrollToEnd();
            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        /*
         * str - сообщение
         * dt - дата получения/отправки сообщения (строковый формат)
         * person - имя отправителя (для входящих сообщений), для исходящих отправитель всегда "Вы"
         * SendOut - бул. переменная, если false - сообщение входящее (окрашено в один цвет), если true - исходящее (окрашено в другой цвет)
         */

        // Часть, которую опустим в видео
        private void Initialize(string messageText, string date, string person, bool sendOut)
        {
            ObservableCollection<Message> Messages = new ObservableCollection<Message>();
            Messages = Message.GetInboundMessages(messageText, date, person, sendOut);
            compositeCollection.Add(new CollectionContainer() { Collection = Messages });
            conversationList.ItemsSource = compositeCollection;
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
