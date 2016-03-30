using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace SocketClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public MainWindow()
        {
            InitializeComponent();

            LoopConnect();
            SendLoop();
        }

        private void SendLoop()
        {
            try
            {
                while (true)
                {
                    byte[] req = Encoding.ASCII.GetBytes( "ssup");                     
                    _clientSocket.Send(req);

                    byte[] receivedbuf = new byte[1024];
                    int rec = _clientSocket.Receive(receivedbuf);
                    byte[] data = new byte[rec];
                    Array.Copy(receivedbuf, data, rec);
                    Debug.WriteLine("Received :" + Encoding.ASCII.GetString(data));

                }
            }
            catch (Exception)
            {                
            }         
        }

        private void LoopConnect()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++; 
                    _clientSocket.Connect(IPAddress.Loopback, 224);
                }
                catch (Exception Exp)
                {
                    Debug.WriteLine(attempts);
                    
                }
            }
            
            
        }
    }
}
