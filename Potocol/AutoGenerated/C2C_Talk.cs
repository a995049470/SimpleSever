
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(10)]
    [Serializable]
    public struct C2C_Talk : IToBytes
    {

        public int id;
        private ushort len_talk;
        public string talk;

        public byte[] ToBytes()
        {
            ushort _id = 10;
            ushort _len = 0;
            var b_0 = Helper.ToBytes(_id);

            var b_10 = Helper.ToBytes(id);
            var b_20 = Helper.ToBytes(talk);
            len_talk = (ushort)b_20.Length;
            var b_19 = Helper.ToBytes(len_talk);

            _len = (ushort)(0 + b_10.Length + b_19.Length + b_20.Length);
            var b_1 = Helper.ToBytes(_len);
            return Helper.MergeBytes(b_0, b_1, b_10, b_19, b_20); 
        }
            
        public static C2C_Talk Parse(byte[] bytes)
        {
            C2C_Talk value = new C2C_Talk();
            int start = 4;

            value.id = Helper.To_int(bytes, ref start);
            value.len_talk = Helper.To_ushort(bytes, ref start);
            value.talk = Helper.To_string(bytes, ref start, value.len_talk);

            return value;
        }
    }
}