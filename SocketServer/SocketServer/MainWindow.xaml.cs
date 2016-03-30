using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace SocketServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static byte[] _buffer = new byte[1024];
        public static List<Socket> _clientSocket = new List<Socket>();
        public static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public MainWindow()
        {
            InitializeComponent();
            SetUpServer();
        }


        public static void SetUpServer()
        {
            try
            {
                Debug.WriteLine("Setting up");
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 224));
                _serverSocket.Listen(6);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                Debug.WriteLine("Set up");
            }
            catch (Exception Exp)
            {
                Debug.WriteLine(Exp.InnerException);
                ;
            }
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                Socket socket = _serverSocket.EndAccept(AR);
                _clientSocket.Add(socket);
                Debug.WriteLine("client ");
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception)
            {

                ;
            }
        }


        private static void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                Socket socket = (Socket)AR.AsyncState;
                int received = socket.EndReceive(AR);
                byte[] databuff = new byte[received];
                Array.Copy(_buffer, databuff, received);
                Debug.WriteLine("Received:" + Encoding.ASCII.GetString(databuff));


                byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

                
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                
                ;
            }

        }


        
        private static void SendCallback(IAsyncResult AR)
        {
            try
            {
                
                Socket socket = (Socket)AR.AsyncState;
                socket.EndSend(AR);
            }
            catch (Exception Exp)
            {
                Debug.WriteLine(Exp.InnerException);
                
            }
        }
    }
}
