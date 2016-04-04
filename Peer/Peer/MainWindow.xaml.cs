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
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ((App)Application.Current).vm;
            


            Random r = new Random();
            
            Server s = new Server();
            port = r.Next(100, 120);
            Thread serverthread = new Thread(
                () =>
                {
                    s.Run(port);
                }
                );
            serverthread.Start();
            MyPortTextBox.Text = port.ToString();
            ((App)Application.Current).vm.SetPort(port);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((App)Application.Current).vm.SendData(Convert.ToInt32(PortTextBox.Text) , MessageTextBox.Text);
            }
            catch (Exception Exp)
            {                
            }
        }

        private void FindProcessesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((App)Application.Current).vm.FindProcesses();
            }
            catch (Exception Exp)
            {

                
            }
        }

        private void StartElectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((App)Application.Current).vm.StartElection();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CrashProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((App)Application.Current).vm.ICrashed();
            }
            catch (Exception)
            {                
            }
        }
    }
}


