
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(9)]
    [Serializable]
    public struct S2C_RoomState : IToBytes
    {

        public int state;

        public byte[] ToBytes()
        {
            ushort _id = 9;
            ushort _len = 0;
            var b_0 = Helper.ToBytes(_id);

            var b_10 = Helper.ToBytes(state);

            _len = (ushort)(0 + b_10.Length);
            var b_1 = Helper.ToBytes(_len);
            return Helper.MergeBytes(b_0, b_1, b_10); 
        }
            
        public static S2C_RoomState Parse(byte[] bytes)
        {
            S2C_RoomState value = new S2C_RoomState();
            int start = 4;

            value.state = Helper.To_int(bytes, ref start);

            return value;
        }
    }
}