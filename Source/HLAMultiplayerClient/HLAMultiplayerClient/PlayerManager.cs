using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;

namespace HLAMultiplayerClient
{
    class PlayerManager
    {
        public int id;
        public Vector3 position;
        public Vector3 angles;
        public string username;
        public bool isLocal;

        public void Move (Vector3 newPos, Vector3 newAngles)
        {
            position = newPos;
            angles = newAngles;
        }

        public void Update ()
        {
            string[] playerInfo = {

                "\"Player\"",
                "{",
                $"	\"Position\"	\"{position}\"",
                $"	\"Angles\"	\"{angles}\"",
                $"	\"Username\"	\"{username}\"",
                $"	\"IsLocal\"	\"{isLocal}\"",
                "}"

            };

            foreach (string file in Directory.GetFiles(Program.tempPath))
            {
                if (file.Contains("GameInfo"))
                {
                    continue;
                }

                string[] path = file.Split((char) 92);
                string fileName = path[path.Length - 1];
                fileName = fileName.Split(".txt")[0];
                int player = int.Parse(fileName);
                if (player > GameManager.players.Count)
                {
                    File.Delete(file);
                }
            }

            Directory.CreateDirectory(Program.tempPath);
            File.WriteAllLines(Path.Combine(Program.tempPath, id.ToString() + ".txt"), playerInfo);

            if (isLocal)
            {
                ClientSend.PlayerMovement(position, angles);
            }
        }
    }
}
