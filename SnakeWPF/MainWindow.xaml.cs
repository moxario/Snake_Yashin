using Common;
using SnakeWPF.Pages;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace SnakeWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public ViewModelUserSettings viewModelUserSettings = new ViewModelUserSettings();
        public ViewModelGames viewModelGames = null;
        public static IPAddress remoteIPAddress = IPAddress.Parse("127.0.0.1");
        public static int Port = 5001;
        public Thread tRec;
        public UdpClient recivingUdpClient;
        public Home Home = new Home();
        public Game Game = new Game();

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
        }
    }
}
