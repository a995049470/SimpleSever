
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(2)]
    [Serializable]
    public struct C2S_S2C_Talk : IToBytes
    {

        private ushort len_talk;
        public string talk;

        public byte[] ToBytes()
        {
            ushort _id = 2;
            ushort _len = 0;
            var b_0 = Helper.ToBytes(_id);

            var b_10 = Helper.ToBytes(talk);
            len_talk = (ushort)b_10.Length;
            var b_9 = Helper.ToBytes(len_talk);

            _len = (ushort)(0 + b_9.Length + b_10.Length);
            var b_1 = Helper.ToBytes(_len);
            return Helper.MergeBytes(b_0, b_1, b_9, b_10); 
        }
            
        public static C2S_S2C_Talk Parse(byte[] bytes)
        {
            C2S_S2C_Talk value = new C2S_S2C_Talk();
            int start = 4;

            value.len_talk = Helper.To_ushort(bytes, ref start);
            value.talk = Helper.To_string(bytes, ref start, value.len_talk);

            return value;
        }
    }
}