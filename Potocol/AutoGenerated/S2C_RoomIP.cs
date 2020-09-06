
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(4)]
    [Serializable]
    public struct S2C_RoomIP : IToBytes
    {

        private ushort len_address;
        public byte[] address;
        public int port;

        public byte[] ToBytes()
        {
            ushort _id = 4;
            ushort _len = 0;
            var b_0 = Helper.ToBytes(_id);

            var b_10 = Helper.ToBytes(address);
            len_address = (ushort)b_10.Length;
            var b_9 = Helper.ToBytes(len_address);
            var b_20 = Helper.ToBytes(port);

            _len = (ushort)(0 + b_9.Length + b_10.Length + b_20.Length);
            var b_1 = Helper.ToBytes(_len);
            return Helper.MergeBytes(b_0, b_1, b_9, b_10, b_20); 
        }
            
        public static S2C_RoomIP Parse(byte[] bytes)
        {
            S2C_RoomIP value = new S2C_RoomIP();
            int start = 4;

            value.len_address = Helper.To_ushort(bytes, ref start);
            value.address = Helper.To_byteArray(bytes, ref start, value.len_address);
            value.port = Helper.To_int(bytes, ref start);

            return value;
        }
    }
}