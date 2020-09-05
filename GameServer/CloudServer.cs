using System;
using System.Collections.Generic;
using LUDP;
using LPTC;
using System.Net;
using System.Threading.Tasks;

namespace GameServer
{
    class CloudServer
    {
        private List<IPEndPoint> m_roomIPList;
        private List<IPEndPoint> m_waitIPList;
        private Task m_task;
        private UdpListener Listener;
        public CloudServer()
        {
            m_roomIPList = new List<IPEndPoint>();
            Listener = new UdpListener();
            m_task = Task.Run(async () =>
            {
                while (true)
                {
                    var recived = await Listener.Receive();
                    Handle(recived);
                }
            });
        }

        public void Handle(Received received)
        {
            LPTCType type = (LPTCType)(received.msg[0] | received.msg[1] << 8);
            switch (type)
            {
                case LPTCType.C2S_BuildRoom:
                    Handle_C2S_BuildRoom(C2S_BuildRoom.Parse(received.msg));
                    break;
                case LPTCType.C2S_StartLinkRoom:
                    Handle_C2S_StartLinkRoom(received.sender);                 
                    break;
                case LPTCType.C2S_EndLinkRoom:
                    Handle_C2S_EndLinkRoom(received.sender);
                    break;
            }
        }

        public void LinkRoom()
        {
            int min = Math.Min(m_waitIPList.Count, m_roomIPList.Count);
            for (int i = 0; i < min; i++)
            {
                var roomIP = m_roomIPList[0];
                var waitIP = m_waitIPList[0];
                m_roomIPList.RemoveAt(0);
                m_waitIPList.RemoveAt(0);
                S2C_RoomIP value = new S2C_RoomIP()
                {
                    address = roomIP.Address.GetAddressBytes(),
                    port = roomIP.Port
                };
                Listener.S2C_Send(waitIP, value);
            }
        }

        public void Handle_C2S_BuildRoom(C2S_BuildRoom value)
        {
            var ip = new IPEndPoint(new IPAddress(value.address), value.port);
            if(!m_roomIPList.Contains(ip))
            {
                m_roomIPList.Add(ip);
                LinkRoom();
            }
        }

        public void Handle_C2S_StartLinkRoom(IPEndPoint ip)
        {
            if(!m_waitIPList.Contains(ip))
            {
                m_waitIPList.Add(ip);
                LinkRoom();
            }
        }

        public void Handle_C2S_EndLinkRoom(IPEndPoint ip)
        {
            if(m_waitIPList.Contains(ip))
            {
                m_waitIPList.Remove(ip);
            }
        }

       

       

    }
}
