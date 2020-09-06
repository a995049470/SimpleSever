
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(6)]
    [Serializable]
    public struct C2S_EndLinkRoom : IToBytes
    {


        public byte[] ToBytes()
        {
            ushort _id = 6;
            ushort _len = 0;
            var b_0 = Helper.ToBytes(_id);


            _len = (ushort)(0);
            var b_1 = Helper.ToBytes(_len);
            return Helper.MergeBytes(b_0, b_1); 
        }
            
        public static C2S_EndLinkRoom Parse(byte[] bytes)
        {
            C2S_EndLinkRoom value = new C2S_EndLinkRoom();
            int start = 4;


            return value;
        }
    }
}