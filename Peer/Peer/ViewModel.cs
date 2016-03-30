using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer
{
    class ViewModel : INotifyPropertyChanged
    {
        public bool isActive { get; set; }
        public bool isCoordinator { get; set; }
        public int port { get; set; }
        public int id { get; set; }
        public ObservableCollection<int> Peers { get; set; }

        public ViewModel()
        {

        }

        public void SetIp(int newport)
        {
            try
            {
                port = newport;
            }
            catch (Exception)
            {                
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
