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
        private LPTCHandle m_handle;
        private IPEndPoint m_reciveIp;
        public CloudServer()
        {
            m_roomIPList = new List<IPEndPoint>();
            m_waitIPList = new List<IPEndPoint>();
            Listener = new UdpListener();
            m_handle = new LPTCHandle();
            m_handle.AddListener(Handle_C2S_BuildRoom);
            m_handle.AddListener(Handle_C2S_StartLinkRoom);
            m_handle.AddListener(Handle_C2S_EndLinkRoom);
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
            m_reciveIp = received.sender;
            m_handle.Handle(received.msg);
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
                Console.WriteLine($"{waitIP} 链接了房间 {roomIP}");
            }
        }

        public void Handle_C2S_BuildRoom(C2S_BuildRoom value)
        {
            var ip = new IPEndPoint(new IPAddress(value.address), value.port);
            Console.WriteLine($"{m_reciveIp} 尝试建立房间 {ip}");
            if(!m_roomIPList.Contains(ip))
            {
                m_roomIPList.Add(ip);
                LinkRoom();
            }
        }

        public void Handle_C2S_StartLinkRoom(C2S_StartLinkRoom value)
        {
            Console.WriteLine($"{m_reciveIp} 尝试连接房间");
            if (!m_waitIPList.Contains(m_reciveIp))
            {
                m_waitIPList.Add(m_reciveIp);
                LinkRoom();
            }
        }

        public void Handle_C2S_EndLinkRoom(C2S_EndLinkRoom value)
        {
            if(m_waitIPList.Contains(m_reciveIp))
            {
                m_waitIPList.Remove(m_reciveIp);
            }
        }

       

       

    }
}
