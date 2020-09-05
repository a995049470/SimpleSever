
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(3)]
    [Serializable]
    public struct C2S_BuildRoom : IToBytes
    {

        private ushort len_address;
        public byte[] address;
        public int port;

        public byte[] ToBytes()
        {
            ushort id = 2;
            ushort len = 0;
            var b_0 = Helper.ToBytes(id);

            var b_10 = Helper.ToBytes(address);
            len_address = (ushort)b_10.Length;
            var b_9 = Helper.ToBytes(len_address);
            var b_20 = Helper.ToBytes(port);

            len = (ushort)(0 + b_9.Length + b_10.Length + b_20.Length);
            var b_1 = Helper.ToBytes(len);
            return Helper.MergeBytes(b_0, b_1, b_9, b_10, b_20); 
        }
            
        public static C2S_BuildRoom Parse(byte[] bytes)
        {
            C2S_BuildRoom value = new C2S_BuildRoom();
            int start = 4;

            value.len_address = Helper.To_ushort(bytes, ref start);
            value.address = Helper.To_byteArray(bytes, ref start, value.len_address);
            value.port = Helper.To_int(bytes, ref start);

            return value;
        }
    }
}