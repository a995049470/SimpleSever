using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LPTC
{
    public static class Helper
    {

        public static byte[] ToBytes(ushort value)
        {
            byte[] bytes = new byte[2];
            bytes[0] = (byte)value;
            bytes[1] = (byte)(value >> 8);
            return bytes;
        }

        public static ushort To_ushort(byte[] bytes, ref int start)
        {
            ushort value = (ushort)(bytes[start] | (bytes[start + 1] << 8));
            start += 2;
            return value;
        }

        public static byte[] ToBytes(byte value)
        {
            byte[] bytes = new byte[1];
            bytes[0] = value;
            return bytes;
        }

        public static byte To_byte(byte[] bytes, ref int start)
        {
            byte value = bytes[start];
            start++;
            return value;
        }

        public static byte[] ToBytes(string value)
        {
            return UTF8Encoding.UTF8.GetBytes(value);
        }

        public static string To_string(byte[] bytes, ref int start, int len)
        {
            var value = UTF8Encoding.UTF8.GetString(bytes, start, len);
            start += len;
            return value;
        }

        public static byte[] ToBytes(long value)
        {
            byte[] bytes = new byte[8];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(value >> (i * 8));
            }
            return bytes;
        }

        public static long To_long(byte[] bytes, ref int start)
        {
            long value = 0;
            for (int i = start; i < start + 8; i++)
            {
                value |= (long)bytes[i] << ((i - start) * 8);
            }
            start += 8;
            return value;
        }

        public static byte[] ToBytes(int value)
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(value >> (i * 8));
            }
            return bytes;
        }

        public static int To_int(byte[] bytes, ref int start)
        {
            int value = 0;
            for (int i = start; i < start + 4; i++)
            {
                value |= (int)bytes[i] << ((i - start) * 8);
            }
            start += 4;
            return value;
        }
        
        public static byte[] ToBytes(byte[] bytes)
        {
            return bytes;
        }

        public static byte[] To_byteArray(byte[] bytes, ref int start, int len)
        {
            var value = new byte[len];
            for (int i = 0; i < len; i++)
            {
                value[i] = bytes[i + start];
            }
            start += len;
            return value;
        }
        
        public static byte[] MergeBytes(params byte[][] bytesArray)
        {
            int total = 0;
            for (int i = 0; i < bytesArray.Length; i++)
            {
                total += bytesArray[i].Length;
            }
            var res = new byte[total];
            int index = 0;
            for (int i = 0; i < bytesArray.Length; i++)
            {
                var bytes = bytesArray[i];
                var len = bytes.Length;
                Array.Copy(bytes, 0, res, index, len);
                index += len;
            }
            return res;
        }
        
    }
}
