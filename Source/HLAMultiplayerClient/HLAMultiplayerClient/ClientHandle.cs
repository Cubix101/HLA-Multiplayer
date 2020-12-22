using System;
using System.Numerics;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace HLAMultiplayerClient
{
    class ClientHandle
    {
        public static void Welcome (Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Console.WriteLine($"Message from server: {_msg}");
            Client.myId = _myId;
            ClientSend.WelcomeReceived();

            Client.udp.Connect(((IPEndPoint)Client.tcp.socket.Client.LocalEndPoint).Port);
        }

        public static void SpawnPlayer (Packet _packet)
        {
            int _id = _packet.ReadInt();
            string _username = _packet.ReadString();
            Vector3 _position = _packet.ReadVector3();
            Vector3 _angles = _packet.ReadVector3();

            GameManager.instance.SpawnPlayer(_id, _username, _position, _angles);
        }
    }
}
