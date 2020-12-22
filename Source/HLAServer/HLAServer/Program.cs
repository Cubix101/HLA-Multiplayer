using System;
using System.Threading;

namespace HLAServer
{
    class Program
    {
        private static bool isRunning = false;
        static void Main(string[] args)
        {
            Console.Title = "Half-Life: Alyx Server";
            int playerCount = 16;

            try
            {
                Console.Write("Enter player count: ");
                playerCount = int.Parse(Console.ReadLine());
            } catch
            {
                Console.WriteLine($"Invalid player count entered, defaulting to {playerCount}");
            }

            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(playerCount, 2020);

            Console.ReadLine();
        }

        private static void MainThread ()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SECOND} ticks per second");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);
                }
            }
        }
    }
}
