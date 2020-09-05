
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID(5)]
    [Serializable]
    public struct C2S_StartLinkRoom : IToBytes
    {


        public byte[] ToBytes()
        {
            ushort id = 2;
            ushort len = 0;
            var b_0 = Helper.ToBytes(id);


            len = (ushort)(0);
            var b_1 = Helper.ToBytes(len);
            return Helper.MergeBytes(b_0, b_1); 
        }
            
        public static C2S_StartLinkRoom Parse(byte[] bytes)
        {
            C2S_StartLinkRoom value = new C2S_StartLinkRoom();
            int start = 4;


            return value;
        }
    }
}