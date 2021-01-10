using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Deboli_AsyncSocketLib;

namespace Deboli_AsyncServer
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AsyncSocketServer mServer;

        public MainWindow()
        {
            InitializeComponent();
            mServer = new AsyncSocketServer();
        }

        private void btn_Ascolta_Click(object sender, RoutedEventArgs e)
        {
            mServer.InAscolto();
        }
    }
}
