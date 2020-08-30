using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LPTC;

namespace LUDP
{

    public class UdpBase
    {
        protected UdpClient m_client;
        private static Type s_pidType = typeof(PIDAttribute);

        protected UdpBase()
        {
            m_client = new UdpClient();
        }
        
        public async Task<Received> Receive()
        {
            var reslut = await m_client.ReceiveAsync();
            return new Received()
            {
                msg = reslut.Buffer,
                sender = reslut.RemoteEndPoint,
            };
        }

        
        public bool C2S_Send<T>(T value) where T : IToBytes
        {
            byte[] bytes;
            bytes = value.ToBytes();
            m_client.Send(bytes, bytes.Length);
            return true;
        }

    }


}
