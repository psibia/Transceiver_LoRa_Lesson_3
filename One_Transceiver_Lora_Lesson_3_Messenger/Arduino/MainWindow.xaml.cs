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
using System.IO.Ports;

namespace Arduino
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SerialPort sp = new SerialPort();
        string[] ports = SerialPort.GetPortNames();

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
                sp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Dispatcher.Invoke(() => TextIn.Text += sp.ReadExisting());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sp.Write(Person.Text + ":  " + TextOut.Text + "\n");
            TextIn.Text += "Вы:  " + TextOut.Text + "\n";
            TextOut.Clear();
        }
    }
}
