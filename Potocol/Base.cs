using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace LPTC
{
    //[Serializable]
    //public struct Send 
    //{
    //    public ushort id;
    //    public ushort len;
    //    public byte[] msg;

  
    //}
    [Serializable]
    public struct Received
    {
        public IPEndPoint sender;
        public byte[] msg;
    }

    public interface IToBytes
    {
        byte[] ToBytes();
    }

}
