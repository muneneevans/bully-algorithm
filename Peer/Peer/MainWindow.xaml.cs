using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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

namespace Peer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int port;
        private string serverip = "127.0.0.1";
        public MainWindow()
        {
            InitializeComponent();


            Random r = new Random();
            
            Server s = new Server();
            port = r.Next(100, 300);
            Thread serverthread = new Thread(
                () =>
                {
                    s.Run(port);
                }
                );
            serverthread.Start();
            MyPortTextBox.Text = port.ToString();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (TcpClient client = new TcpClient(serverip, Convert.ToInt32(PortTextBox.Text)))
                {
                    MemoryStream ms = new MemoryStream();
                    //client.Connect(ip);
                    NetworkStream ns = client.GetStream();
                    var data = Serialize(MessageTextBox.Text, ms);
                    ns.Write(data, 0, data.Length);
                }
            }
            catch (Exception Exp)
            {                
            }
        }

        public byte[] Serialize(object data, MemoryStream ms)
        {
            BinaryFormatter fm = new BinaryFormatter();
            fm.Serialize(ms, data);
            return ms.ToArray();
        }
    }
}


