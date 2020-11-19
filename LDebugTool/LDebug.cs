using System.Collections.Generic;
using UnityEngine;
using System;
using UObject = UnityEngine.Object;
using System.IO;

namespace LDebugTool
{
    [ExecuteInEditMode]
    public class LDebug : MonoBehaviour
    {
        private static LDebug m_instance;
        public static LDebug Instance
        {
            get
            {
                if(m_instance == null)
                {
                    new GameObject(typeof(LDebug).Name, typeof(LDebug));
                }
                return m_instance;
            }
        }


        [SerializeField] string m_outputPath;
        [SerializeField] List<OutputLog> m_logList;
        [SerializeField] bool m_isLogEnable;
        [SerializeField] bool m_isOutputStackTrack;
        [SerializeField] bool m_isOutput;
        
        public static void Log(string msg)
        {
            Instance.Log(msg, null, null);
        }

        private void Awake()
        {
            if(m_instance == null)
            {
                m_instance = this;
                m_logList = new List<OutputLog>();
                if(Application.isPlaying)
                {
                    DontDestroyOnLoad(this.gameObject);
                    Debug.Log($"LDebug 成功初始化");
                }
            }
            else if(m_instance != this)
            {
                DestroyImmediate(this);
            }
        }

        public void OnEnable()
        {
            if (m_instance == null)
            {
                m_instance = this;
                m_logList = new List<OutputLog>();
                if (Application.isPlaying)
                {
                    DontDestroyOnLoad(this.gameObject);
                    Debug.Log("LDebug 成功初始化");
                }
            }
        }

        private void OnDestroy()
        {
            if(m_instance == this)
            {
                if(m_isOutput && Application.isPlaying)
                {
                    WirteOutputLog();
                }
                m_instance = null;
                if (Application.isPlaying)
                {
                    Debug.Log("LDebug 成功删除");
                }
            }
        }

        
        private void Log(string msg, string color, UObject context)
        {
            if(m_isLogEnable)
            {
                string logMsg = null;
                if(string.IsNullOrEmpty(color))
                {
                    logMsg = msg;
                }
                else
                {
                    logMsg = $"<color=#{color}>{msg}<color>";
                }
                Debug.Log(logMsg, context);
            }
#if UNITY_EDITOR
            if (m_isOutput)
            {
                string smsg = "";
                if(m_isOutputStackTrack)
                {
                    var st = new System.Diagnostics.StackTrace();
                    var res = st.ToString().Split('\n');
                    for (int i = 2; i < res.Length; i++)
                    {
                        smsg += $"{res[i]}\n";
                    }
                }
                var log = new OutputLog()
                {
                    time = DateTime.Now,
                    logMsg = msg,
                    stackMsg = smsg
                };
                m_logList.Add(log);
            }
#endif
        }

        [ContextMenu("输出日志文件")]
        public void WirteOutputLog()
        {
#if UNITY_EDITOR
            if(m_logList.Count == 0)
            {
                Debug.Log($"当前无日志");
                return;
            }
            bool m_isCatchError = false;
            string logname = "";
            try
            {
                string content = "";
                for (int i = 0; i < m_logList.Count; i++)
                {
                    content += m_logList[i].ToString();
                }
                string time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                logname = $"log_{time}";
                string path = $"{m_outputPath}/{logname}.txt";
                File.WriteAllText(path, content);
                UnityEditor.AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.Log(e);
                m_isCatchError = true;
            }
            if(!m_isCatchError)
            {
                m_logList.Clear();
                Debug.Log($"LDebug 成功输出日志 {logname}");
            }
#endif
        }

    }
}
