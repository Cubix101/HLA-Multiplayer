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

            Directory.CreateDirectory(Program.tempPath);
            File.WriteAllLines(Path.Combine(Program.tempPath, id.ToString() + ".txt"), playerInfo);

            if (isLocal)
            {
                ClientSend.PlayerMovement(position, angles);
            }
        }
    }
}
