using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LPTC;
using System.Threading;

namespace LUDP
{

    public class UdpBase
    {
        protected UdpClient m_client;
        private static Type s_pidType = typeof(PIDAttribute);
        public int ReceiveInterval;

        protected UdpBase()
        {
            m_client = new UdpClient();
            ReceiveInterval = 20;
        }
        
        public IPEndPoint GetRemoteEndPoint()
        {
            return m_client.Client.RemoteEndPoint as IPEndPoint;
        }

        public IPEndPoint GetLocalEndPoint()
        {
            return m_client.Client.LocalEndPoint as IPEndPoint;
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


        //public async Task<Received> Receive()
        //{
        //    byte[] buffer = null;
        //    IPEndPoint ip = null;
        //    while (m_client != null)
        //    {
        //        buffer = m_client.Receive(ref ip);
        //        if(buffer?.Length > 0)
        //        {
        //            break;
        //        }
        //        await Task.Delay(ReceiveInterval);
        //    }
        //    return new Received()
        //    {
        //        sender = ip,
        //        msg = buffer
        //    };
        //}

        public bool C2S_Send<T>(T value) where T : IToBytes
        {
            byte[] bytes;
            bytes = value.ToBytes();
            m_client.Send(bytes, bytes.Length);
            return true;
        }

    }


}
