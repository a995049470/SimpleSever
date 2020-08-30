using LPTC;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace LUDP
{
    public class UdpListener : UdpBase
    {
        private IPEndPoint m_listenIP;
        public UdpListener() : this(new IPEndPoint(IPAddress.Any, 10186))
        {

        }

        public UdpListener(IPEndPoint endPoint)
        {
            this.m_listenIP = endPoint;
            m_client = new UdpClient(endPoint);
        }

        public bool S2C_Send<T>(IPEndPoint endPoint, T value) where T : IToBytes
        {
            byte[] bytes;
            bytes = value.ToBytes();
            m_client.Send(bytes, bytes.Length, endPoint);
            return true;
        }

    }
}
