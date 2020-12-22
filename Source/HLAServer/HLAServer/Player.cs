using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace HLAServer
{
    class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Vector3 angles;

        public Player (int _id, string _username)
        {
            id = _id;
            username = _username;
            position = Vector3.Zero;
            angles = Vector3.Zero;
        }

        public void Move (Vector3 _pos, Vector3 _angles)
        {
            position = _pos;
            angles = _angles;

            Console.WriteLine(_pos.ToString());
        }
    }
}
