
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(8)]
    [Serializable]
    public struct S2C_Response_LinkRoom : IToBytes
    {

        public int id;

        public byte[] ToBytes()
        {
            ushort _id = 8;
            ushort _len = 0;
            var b_0 = Helper.ToBytes(_id);

            var b_10 = Helper.ToBytes(id);

            _len = (ushort)(0 + b_10.Length);
            var b_1 = Helper.ToBytes(_len);
            return Helper.MergeBytes(b_0, b_1, b_10); 
        }
            
        public static S2C_Response_LinkRoom Parse(byte[] bytes)
        {
            S2C_Response_LinkRoom value = new S2C_Response_LinkRoom();
            int start = 4;

            value.id = Helper.To_int(bytes, ref start);

            return value;
        }
    }
}