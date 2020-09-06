
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(7)]
    [Serializable]
    public struct C2S_LinkRoom : IToBytes
    {


        public byte[] ToBytes()
        {
            ushort _id = 7;
            ushort _len = 0;
            var b_0 = Helper.ToBytes(_id);


            _len = (ushort)(0);
            var b_1 = Helper.ToBytes(_len);
            return Helper.MergeBytes(b_0, b_1); 
        }
            
        public static C2S_LinkRoom Parse(byte[] bytes)
        {
            C2S_LinkRoom value = new C2S_LinkRoom();
            int start = 4;


            return value;
        }
    }
}