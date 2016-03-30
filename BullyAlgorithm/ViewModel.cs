using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BullyAlgorithm
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Process> AllProcesses { get; set; }
        public ObservableCollection<int> Allids { get; set; }

        public ViewModel()
        {
            try
            {
                AllProcesses = new ObservableCollection<Process>();
                Allids = new ObservableCollection<int>();
            }
            catch (Exception Exp)
            {
                Debug.WriteLine(Exp.InnerException);
            }
        }

        public void AddProcess()
        {
            try
            {
                Process p = new Process()
                {
                    id = AllProcesses.Count + 1,
                    processlist = new List<int>( Allids )
                };

                Thread t = new Thread(p.Execute);
                t.Name = p.id.ToString();
                p.id = t.ManagedThreadId;
                t.Start();
                


                Allids.Add(p.id);
                AllProcesses.Add(p);

                NotifyPropertyChanged("Allids");
                NotifyPropertyChanged("AllProcesses");
            }
            catch (Exception Exp)
            {
                Debug.WriteLine(Exp.InnerException);
                throw;
            }
        }

        public void sendMessage(int sender)
        {
            //
            System.Diagnostics.Process Process = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.ProcessThreadCollection Threadcollection = Process.Threads;
            

            foreach (ProcessThread t in Threadcollection)
            {
                if (t.Id == sender)
                {
                    Debug.WriteLine(t.Id);
                }                                                
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
