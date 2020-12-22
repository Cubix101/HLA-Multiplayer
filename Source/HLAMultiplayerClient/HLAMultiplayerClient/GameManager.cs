using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;

namespace HLAMultiplayerClient
{
    class GameManager
    {
        public static GameManager instance;

        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

        public void Awake ()
        {
            if (instance == null)
            {
                instance = this;
            }
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
                ManagePlayer();
            } catch
            {
                //idk
            }
        }

        void ManagePlayer ()
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

            string idString = consoleLog.Split(new string[] { "Vector" }, StringSplitOptions.None)[0];
            idString = idString.Split(' ')[idString.Split(' ').Length - 2];

            string vectorString = consoleLog.Split(new string[] { "Vector" }, StringSplitOptions.None)[1];
            vectorString = vectorString.Split('[')[1];
            vectorString = vectorString.Split(']')[0];

            string[] vectorComponents = vectorString.Split(' ');
            float x = float.Parse(vectorComponents[0]);
            float y = float.Parse(vectorComponents[1]);
            float z = float.Parse(vectorComponents[2]);

            players[int.Parse(idString)].position = new Vector3(x, y, z);

            string anglesString = consoleLog.Split(new string[] { "QAngles" }, StringSplitOptions.None)[1];
            vectorString = vectorString.Split('[')[1];
            vectorString = vectorString.Split(']')[0];

            string[] angleComponents = vectorString.Split(' ');
            x = float.Parse(angleComponents[0]);
            y = float.Parse(angleComponents[1]);
            z = float.Parse(angleComponents[2]);

            Console.WriteLine(vectorString);
            players[int.Parse(idString)].angles = new Vector3(x, y, z);
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
