using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace HLAMultiplayerClient
{
    class ClientSend
    {
        private static void SendTCPData (Packet _packet)
        {
            _packet.WriteLength();
            Client.tcp.SendData(_packet);
        }

        private static void SendUDPData(Packet _packet)
        {
            _packet.WriteLength();
            Client.udp.SendData(_packet);
        }

        #region Packets
        public static void WelcomeReceived ()
        {
            using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(Client.myId);
                _packet.Write("Player");

                SendTCPData(_packet);
            }
        }

        public static void PlayerMovement (Vector3 position, Vector3 angles)
        {
            using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
            {
                _packet.Write(position);
                _packet.Write(angles);

                SendUDPData(_packet);
            }
        }
        #endregion
    }
}
