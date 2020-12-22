using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace HLAServer
{
    class ServerHandle
    {
        public static void WelcomeReceived (int _fromClient, Packet _packet)
        {
            int _clientIdToCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully as player {_fromClient}");
            if (_fromClient != _clientIdToCheck)
            {
                Console.WriteLine($"Player {_username} ID: {_fromClient} assumed the wrong client ID, {_clientIdToCheck}! This is very bad!! Like catastrophic!! Seriously somebody really fucked up here!!!");
            }

            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void PlayerMovement (int _fromClient, Packet _packet)
        {
            Vector3 _pos = _packet.ReadVector3();
            Vector3 _angles = _packet.ReadVector3();

            Server.clients[_fromClient].player.Move(_pos, _angles);
        }
    }
}
