using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullyAlgorithm
{
    class ViewModel : INotifyPropertyChanged
    {
        List<Process> AllProcesses;


        public ViewModel()
        {
            try
            {
                AllProcesses = new List<Process>();
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

            }
            catch (Exception Exp)
            {
                Debug.WriteLine(Exp.InnerException);
                throw;
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
