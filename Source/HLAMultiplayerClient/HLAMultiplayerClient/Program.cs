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

        static void Main(string[] args)
        {
            Console.Title = "HLA Multiplayer Client";
            appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine($"App is located at {appPath}");

            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Client.Start();
            Console.WriteLine("Press any key to connect to server.");

            Client.instance.ConnectToServer();
            new GameManager().Awake();
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SECOND} ticks per second");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
