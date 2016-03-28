using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
