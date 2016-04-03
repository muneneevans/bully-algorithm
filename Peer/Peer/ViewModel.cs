using Newtonsoft.Json;
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
        public ObservableCollection<Process> Peers { get; set; }

        public ViewModel()
        {
            Peers = new ObservableCollection<Process>();
        }

        public bool ProcessExisits(Process NewProcess)
        {
            var found = Peers.Where(x => x.id == NewProcess.id).ToList();
            if (found.Count == 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void AddProcess(Process NewProcess)
        {
            try
            {
                var found = Peers.Where(x => x.id == NewProcess.id).ToList();
                if (found.Count == 0 )
                {
                    Peers.Add(NewProcess);
                }
                NotifyPropertyChanged("Peers");
            }
            catch (Exception)
            {                
            }
        }
        public void SetPort(int newport)
        {
            try
            {
                port = newport;
                id = newport;
                server.port = port;
            }
            catch (Exception)
            {                
            }
        }
        public void SendData(int sendingport ,string Message)
        {
            try
            {
                Container c = new Container();
                c.Header = Constants.Message;
                c.peer = new Process() { id = port, port = port };
                server.port = this.port;
                server.SendData(sendingport ,JsonConvert.SerializeObject(c));
            }
            catch (Exception)
            {

                
            }
        }
        public void FindProcesses()
        {
            try
            {
                server.FindProcesses();
                StartElection();
            }
            catch (Exception)
            {

                
            }
        }
        public void StartElection()
        {
            try
            {
                Process Highest = Peers.OrderByDescending(x => x.port).FirstOrDefault();
                if (Highest.port == port)
                {
                    //send winning message
                }
                else
                {
                    var SendingList = Peers.Where(x => x.port > port);
                    Container c = new Container();
                    c.Header = Constants.Election;
                    foreach (Process p in SendingList)
                    {
                        server.SendData(p.port, JsonConvert.SerializeObject(c));
                    }
                }
            }
            catch (Exception)
            {                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
