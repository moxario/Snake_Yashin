using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Yashin
{
    class Program
    {
        public static List<Leaders> Leaders = new List<Leaders>();
        public static List<ViewModelUserSettings> remoteIPAddress = new List<ViewModelUserSettings>();
        public static List<ViewModelGame> viewModelGames = new List<ViewModelGame>();
        public static int LocalPort = 5001;
        public static int Speed = 15;
        static void Main(string[] args)
        {
        }
    }
}
