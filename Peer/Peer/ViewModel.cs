using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer
{
    public class ViewModel : INotifyPropertyChanged
    {
        Server server = new Server();
        public bool isActive { get; set; }
        public bool isCoordinator { get; set; }
        public int port { get; set; }
        public int id { get; set; }
        public ObservableCollection<int> Peers { get; set; }

        public ViewModel()
        {

        }

        public void SetPort(int newport)
        {
            try
            {
                port = newport;
            }
            catch (Exception)
            {                
            }
        }
        public void SendData(int sendingport ,string Message)
        {
            try
            {
                server.port = this.port;
                server.SendData(sendingport ,Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void FindProcesses()
        {
            try
            {
                server.FindProcesses();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
