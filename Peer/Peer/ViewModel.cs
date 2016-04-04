using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        public Process Coordinator { get; set; }
        public int Coordinatorport
        {
            get
            {
                if (Coordinator == null)
                {
                    return 0;
                }
                else {
                    return Coordinator.port;
                    NotifyPropertyChanged("Coordinatorport");
                }
            }
            set { }
        }
        public ObservableCollection<Process> Peers { get; set; }

        public ViewModel()
        {
            Peers = new ObservableCollection<Process>();
        }

        public bool ProcessExisits(Process NewProcess)
        {
            var found = Peers.Where(x => x.id == NewProcess.id).ToList();
            if (found.Count == 0)
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
                if (found.Count == 0)
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
        public void SendData(int sendingport, string Message)
        {
            try
            {
                Container c = new Container();
                c.Header = Constants.Message;
                c.peer = new Process() { id = port, port = port };
                server.port = this.port;
                server.SendData(sendingport, JsonConvert.SerializeObject(c));
            }
            catch (Exception)
            {


            }
        }
        public void FindProcesses()
        {
            try
            {
                Peers.Add(new Process() { id = port, port = port });
                NotifyPropertyChanged("Peers");
                server.FindProcesses();

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
                if (Highest == null)
                {

                }
                if (Highest.port == port)
                {
                    WinnerFound(new Process() { id = port, port = port });

                    foreach (Process p in Peers)
                    {
                        Container c = new Container();
                        c.Header = Constants.IWon;
                        c.peer = new Process() { id = port, port = port };
                        server.SendData(p.port, JsonConvert.SerializeObject(c));
                    }
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
                FindProcesses();
            }
        }
        public void WinnerFound(Process WinnerProcess)
        {
            try
            {
                Coordinator = WinnerProcess;

                NotifyPropertyChanged("Coordinator");
                NotifyPropertyChanged("Coordinatorport");
            }
            catch (Exception)
            {
            }
        }

        public void ICrashed()
        {
            foreach (Process p in Peers)
            {
                Container c = new Container();
                c.Header = Constants.Crash;
                c.peer = new Process() { id = port, port = port };
                server.SendData(p.port, JsonConvert.SerializeObject(c));
            }
        }
        public void ProcessCrashed(Process CrashedProcess)
        {
            try
            {
                Process p = Peers.Where(x => x.port == CrashedProcess.port).FirstOrDefault();
                Peers.Remove(p);
                NotifyPropertyChanged("Peers");
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
