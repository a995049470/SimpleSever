using UnityEngine;
using System;

namespace LDebugTool
{
    struct OutputLog
    {
        public DateTime time;
        public string logMsg;
        public string stackMsg;

        public override string ToString()
        {
            string str = $"日志:\n{logMsg}\n堆栈信息:\n{stackMsg}\n\n";
            return str;
        }
    }
}
