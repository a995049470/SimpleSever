using System;
using System.Collections.Generic;
using System.Net;

namespace LUDP
{
    public class UdpUser : UdpBase
    {

        private UdpUser() : base()
        {
            
        }

        public static UdpUser ConnectTo(string hostname, int port)
        {
            var connection = new UdpUser();
            connection.m_client.Connect(hostname, port);
            return connection;
        }

        public static UdpUser ConnectTo(IPEndPoint ip)
        {
            var connection = new UdpUser();
            connection.m_client.Connect(ip);
            return connection;
        }

        public static UdpUser ConnectToServer()
        {
            return ConnectTo("192.168.1.16", 10186);
        }

        public void Dispose()
        {
            m_client.Close();
        }

    }
}
