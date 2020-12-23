using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;

namespace HLAMultiplayerClient
{
    public enum DataTypes
    {
        playerData
    }

    class GameManager
    {
        public static GameManager instance;

        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

        public delegate void DataHandler(string[] args);
        public static Dictionary<DataTypes, DataHandler> dataHandlers = new Dictionary<DataTypes, DataHandler>();

        public void Awake ()
        {
            if (instance == null)
            {
                instance = this;
            }

            dataHandlers = new Dictionary<DataTypes, DataHandler>()
            {
                { DataTypes.playerData, ManagePlayer }
            };
        }

        public void ManagePlayer (string[] args)
        {
            int playerID = int.Parse(args[0]);
            Vector3 pos = new Vector3(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
            Vector3 angles = new Vector3(float.Parse(args[4]), float.Parse(args[5]), float.Parse(args[6]));

            players[playerID].Move(pos, angles);
        }

        public void Update ()
        {
            string[] gameInfo = {

                "\"game\"",
                "{",
                $"	\"playerCount\"	\"{players.Count}\"",
                "}"

            };

            Directory.CreateDirectory(Program.tempPath);
            File.WriteAllLines(Path.Combine(Program.tempPath, "GameInfo.txt"), gameInfo);

            foreach (PlayerManager player in players.Values)
            {
                player.Update();
            }

            try
            {
                ManageData();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling server message: " + ex.Message);
            }
        }

        void ManageData ()
        {
            string consoleLog = "";
            using (FileStream stream = File.Open(Path.Combine(Program.hlaPath, "console.log"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    consoleLog = reader.ReadToEnd();
                }
            }

            consoleLog = consoleLog.Split('\n')[consoleLog.Split('\n').Length - 2];

            if (consoleLog.ToLower().Contains("servermessage"))
            {
                consoleLog = consoleLog.Substring(consoleLog.IndexOf('s'), consoleLog.Length - consoleLog.IndexOf('s'));

                int dataType = int.Parse(consoleLog.Split('[')[0].ToCharArray()[15].ToString());

                consoleLog = consoleLog.Split('[')[1];
                consoleLog = consoleLog.Split(']')[0];

                string[] args = consoleLog.Split(",");

                dataHandlers[(DataTypes)dataType](args);
            }
        }

        public void SpawnPlayer (int _id, string _username, Vector3 position, Vector3 angles)
        {
            Console.WriteLine("Player spawned.");

            PlayerManager _player = new PlayerManager();
            _player.id = _id;
            _player.username = _username;
            if (_id == Client.myId)
            {
                _player.isLocal = true;
            }
            players.Add(_id, _player);
        }
    }
}
