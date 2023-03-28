using System;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using MaterialDesignThemes.Wpf;
using System.Runtime.InteropServices;

namespace Client_Menu
{
    public partial class MainWindow : Window
    {
        ViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new ViewModel()
            {
                address = "127.0.0.1",
                port = 7777,
            };
            DataContext = model;
        }
        void Client()
        {
            try
            {
                //1
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(model.address), model.port);
                IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);
                UdpClient client = new UdpClient();
                byte[] data = Encoding.Unicode.GetBytes(_Message.Text);
                client.Send(data, data.Length, ipPoint);

                SolidColorBrush mycolor = new SolidColorBrush(Colors.CornflowerBlue);

                AllMessage mess = new AllMessage();
                mess.color = mycolor;
                model.AddProgress(mess);
                mess.Message = _Message.Text;
                mess.Time = DateTime.Now;

                //2
                data = client.Receive(ref remoteIpPoint);
                string response = Encoding.Unicode.GetString(data);


                AllMessage mess2 = new AllMessage();
                mess2.color = new SolidColorBrush(Colors.ForestGreen);
                model.AddProgress(mess2);
                mess2.ReceivedMessage = response;
                mess2.Time = DateTime.Now;
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Client();
        }
    }

}
