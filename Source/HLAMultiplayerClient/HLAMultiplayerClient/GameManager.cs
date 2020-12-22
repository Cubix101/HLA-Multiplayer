using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

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

        public void SpawnPlayer (int _id, string _username, Vector3 position, Vector3 angles)
        {
            Console.WriteLine("Player spawned.");

            PlayerManager _player = new PlayerManager();
            _player.id = _id;
            _player.username = _username;
            players.Add(_id, _player);
        }
    }
}
