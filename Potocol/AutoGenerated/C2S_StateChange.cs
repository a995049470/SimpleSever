
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(1)]
    [Serializable]
    public struct C2S_StateChange : IToBytes
    {

        public byte state;

        public byte[] ToBytes()
        {
            ushort id = 2;
            ushort len = 0;
            var b_0 = Helper.ToBytes(id);

            var b_10 = Helper.ToBytes(state);

            len = (ushort)(0 + b_10.Length);
            var b_1 = Helper.ToBytes(len);
            return Helper.MergeBytes(b_0, b_1, b_10); 
        }
            
        public static C2S_StateChange Parse(byte[] bytes)
        {
            C2S_StateChange value = new C2S_StateChange();
            int start = 4;

            value.state = Helper.To_byte(bytes, ref start);

            return value;
        }
    }
}