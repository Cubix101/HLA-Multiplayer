using System;
using System.Threading;
using System.IO;
using System.Reflection;

namespace HLAMultiplayerClient
{
    class Program
    {
        private static bool isRunning = false;
        public static string appPath;
        public static string tempPath;
        public static string hlaPath = "";

        static void Main(string[] args)
        {
            Console.Title = "HLA Multiplayer Client";
            appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            tempPath = Path.Combine("C:", "temp-client");
            Console.WriteLine($"App is located at {appPath}, utilising temp-path {tempPath}");

            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Please enter your hlvr folder path (not Half-Life: Alyx, hlvr, for example E:/Steam/steamapps/common/Half-Life Alyx/game/hlvr): ");
                hlaPath = Console.ReadLine();
                validInput = Directory.Exists(hlaPath);
            }

            isRunning = true;

            new GameManager().Awake();

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Client.Start();
            Console.WriteLine("Press any key to connect to server.");

            Client.instance.ConnectToServer();
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SECOND} ticks per second");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        public static void Update ()
        {
            GameLogic.Update();
            GameManager.instance.Update();
        }
    }
}
