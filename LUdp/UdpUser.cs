using System;
using System.Collections.Generic;

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

    }
}
