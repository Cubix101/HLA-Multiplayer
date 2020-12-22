using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
