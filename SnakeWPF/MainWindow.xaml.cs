using Common;
using Newtonsoft.Json;
using SnakeWPF.Pages;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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
        public UdpClient receivingUdpClient;
        public Home Home = new Home();
        public Game Game = new Game();

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
        }
        public void StartReceiver()
        {
            tRec = new Thread(new ThreadStart(Receiver));
            tRec.Start();
        }
        public void Receiver()
        {
            receivingUdpClient = new UdpClient(int.Parse(viewModelUserSettings.Port));
            IPEndPoint RemoteIpEndPoint = null;
            try
            {
                while (true)
                {
                    byte[] receiverBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                    string returnData = Encoding.UTF8.GetString(receiverBytes);
                    if(viewModelGames == null)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            OpenPage(Game);
                        });
                    }
                    viewModelGames = JsonConvert.DeserializeObject<ViewModelGames>(returnData.ToString());
                    if (viewModelGames.SnakesPlayers.GameOver == true)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            OpenPage(new Pages.EndGame());
                        });
                    }
                    else
                    {
                        Game.CreateUI();
                    }
                }
            }
            catch(Exception exp)
            {
                Debug.WriteLine($"Возникло исключение: {exp.Message}");
            }
        }
        public void Send(string datagramm)
        {
            UdpClient client = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, Port);
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(datagramm);
                client.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception exp)
            {
                Debug.WriteLine($"Возникло исключение: {exp.Message}");
            }
            finally
            {
                client.Close();
            }
        }
        public void OpenPage(Page OpenPage)
        {
            frame.Navigate(OpenPage);
        }
    }
}
