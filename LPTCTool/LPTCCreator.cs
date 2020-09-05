using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;

namespace LPTCTool
{
    public struct LPTCFiled
    {
        public string type;
        public string name;
        public bool isVarLenType;
        public bool isLen;
        //序列化时候的序号
        public int sIndex;
        //反序列化的序号
        public int index;
    }
    
    public class LPTCCreator
    {
        public static void CreateLPTCEnum(ISheet sheet, string outpath)
        {
            string content = @"
namespace LPTC
{
    public enum LPTCType
    {
{var_types}
    }
}";
            string var_types = "";
            for (int i = 0; i < sheet.LastRowNum + 1; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null)
                {
                    return;
                }
                string pid = row.GetCell(0)?.ToString();
                string type_name = row.GetCell(1)?.ToString();
                var_types += $"        {type_name} = {pid},\n";
            }
            content = content.Replace("{var_types}", var_types);
            string path = $"{outpath}\\{"LPTCType"}.cs";
            System.IO.File.WriteAllText(path, content);

        }

        private static bool IsVariableLengthType(string type)
        {
            bool isVLT = false;
            if (type == "string" || 
                type == "byte[]")
            {
                isVLT = true;
            }
            return isVLT;
        }
        public static void ExcelRowToScripts(IRow row, string outpath)
        {
            if (row == null)
            {
                return;
            }
            string pid = "";
            string type_name = "";
            string body_tobytes = "";
            string body_parse = "";
            string body_fields = "";
            string mearg_args = "";
            string body_len = "";
            string content = @"
using System;
using System.Collections.Generic;

namespace LPTC
{
    [PID({pid})]
    [Serializable]
    public struct {type_name} : IToBytes
    {

{body_fields}
        public byte[] ToBytes()
        {
            ushort id = 2;
            ushort len = 0;
            var b_0 = Helper.ToBytes(id);

{body_tobytes}
            len = (ushort)(0{body_len});
            var b_1 = Helper.ToBytes(len);
            return Helper.MergeBytes(b_0, b_1{mearg_args}); 
        }
            
        public static {type_name} Parse(byte[] bytes)
        {
            {type_name} value = new {type_name}();
            int start = 4;

{body_parse}
            return value;
        }
    }
}";
            int index = 10;
            List<LPTCFiled> list = new List<LPTCFiled>();
            for (int i = 0; i < row.LastCellNum + 1; i++)
            {

                string value = row.GetCell(i)?.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                if (i == 0)
                {
                    pid = value;
                }
                else if (i == 1)
                {
                    type_name = value;
                }
                else
                {
                    var res = value.Split(' ');
                    LPTCFiled f = new LPTCFiled();
                    f.type = res[0];
                    f.name = res[1];
                    f.isVarLenType = IsVariableLengthType(f.type);
                    f.isLen = false;
                    f.sIndex = index;
                    f.index = index;
                    index += 10;
                    list.Add(f);
                    if (f.isVarLenType)
                    {
                        f.type = "ushort";
                        f.name = "len_" + f.name;
                        f.isVarLenType = false;
                        f.isLen = true;
                        f.sIndex += 1;
                        f.index -= 1;
                        list.Add(f);
                    }
                }

            }
            //if (list.Count == 0)
            //{
            //    return;
            //}
            //按声明顺序排列
            list.Sort((x, y) =>
            {
                return x.index - y.index;
            });

            for (int i = 0; i < list.Count; i++)
            {
                var f = list[i];

                string p = f.isLen ? "private" : "public";
                body_fields += $"        {p} {f.type} {f.name};\n";
                string ex = "";
                if (f.isVarLenType)
                {
                    var last = list[i - 1];
                    ex = $", value.{last.name}";
                }
                body_parse += $"            value.{f.name} = Helper.To_{f.type.Replace("[]", "Array")}(bytes, ref start{ex});\n";
                mearg_args += $", b_{f.index}";
                body_len += $" + b_{f.index}.Length";
                
            }
            //按序列化顺序排列
            list.Sort((x, y) =>
            {
                return x.sIndex - y.sIndex;
            });

            for (int i = 0; i < list.Count; i++)
            {
                var f = list[i];
                if (f.isLen)
                {
                    var last = list[i - 1];
                    body_tobytes += $"            {f.name} = (ushort)b_{last.index}.Length;\n";
                    body_tobytes += $"            var b_{f.index} = Helper.ToBytes({f.name});\n";
                }
                else
                {
                    body_tobytes += $"            var b_{f.index} = Helper.ToBytes({f.name});\n";
                }
            }
            content = content.Replace("{pid}", pid)
                .Replace("{type_name}", type_name)
                .Replace("{body_tobytes}", body_tobytes)
                .Replace("{body_parse}", body_parse)
                .Replace("{body_fields}", body_fields)
                .Replace("{mearg_args}", mearg_args)
                .Replace("{body_len}", body_len);
            string path = $"{outpath}\\{type_name}.cs";
            System.IO.File.WriteAllText(path, content);

        }
    }
}


