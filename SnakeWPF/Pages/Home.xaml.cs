using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace SnakeWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            if(MainWindow.mainWindow.receivingUdpClient != null) 
                MainWindow.mainWindow.receivingUdpClient.Close();
            if (MainWindow.mainWindow.tRec != null)
                MainWindow.mainWindow.tRec.Abort();
            IPAddress UserIpAddress;
            if(!IPAddress.TryParse(ip.Text, out UserIpAddress))
            {
                MessageBox.Show("Укажите IP адрес в формате X.X.X.X");
                return;
            }
            int UserPort = 0;
            if (!int.TryParse(port.Text, out UserPort))
            {
                MessageBox.Show("Укажите порт");
                return;
            }
            MainWindow.mainWindow.StartReceiver();
            MainWindow.mainWindow.viewModelUserSettings.IpAddress = ip.Text;
            MainWindow.mainWindow.viewModelUserSettings.Port = port.Text;
            MainWindow.mainWindow.viewModelUserSettings.Name = name.Text;
            MainWindow.mainWindow.Send($"/start|{JsonConvert.SerializeObject(MainWindow.mainWindow.viewModelUserSettings)}");
        }
    }
}
