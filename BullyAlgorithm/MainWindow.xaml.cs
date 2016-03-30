using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace BullyAlgorithm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            List<Thread> tlist = new List<Thread>();

            InitializeComponent();

            this.DataContext = ((App)App.Current).vm;
        }

        private void NewProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ProcessListView.Visibility = Visibility.Visible;

                ((App)App.Current).vm.AddProcess();
                
                
            }
            catch (Exception Exp)
            {
                Debug.WriteLine(Exp.InnerException);
                throw;
            }
        }
    }
}
