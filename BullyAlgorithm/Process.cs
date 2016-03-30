using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BullyAlgorithm
{
    public class Process : INotifyPropertyChanged
    {
        public bool _status;
        public bool status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("status");
            }
        }

        private List<int> _processlist;
        public List<int> processlist
        {
            get
            {
                return _processlist;
            }
            set
            {
                _processlist = value;
                NotifyPropertyChanged("processlist");
            }
        }

        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("id");
            }
        }


        public Process()
        {
            status = true;

        }
        public void Execute()
        {
            //
            Random r = new Random();
            int c = 0; 
            while (true)
            {
               // Debug.WriteLine(id +"sending to " +r.Next(1,10) );
                if (c == 200)
                {
                    //Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate
                    {
                        ((App)System.Windows.Application.Current).vm.sendMessage(AppDomain.GetCurrentThreadId());
                    }));
                }
                c++;                 
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

            if (propertyName == "status")
            {
                //do stuff
            }
        }
    }
}
