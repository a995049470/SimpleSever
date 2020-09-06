
using System;
namespace LPTC
{
    public class LPTCHandle
    {
        public void Handle(byte[] bytes)
        {
            if(bytes?.Length < 4)
            {
                return;
            }
            ushort id = (ushort)(bytes[0] | bytes[1] << 8);
            ushort len = (ushort)(bytes[2] | bytes[3] << 8);
            if(bytes?.Length != len + 4)
            {
                return;
            }
            switch (id)
            {

                case 1:
                    C2S_StateChange value_1 = C2S_StateChange.Parse(bytes);
                    m_action_1?.Invoke(value_1);
                    break;
                case 2:
                    C2S_S2C_Talk value_2 = C2S_S2C_Talk.Parse(bytes);
                    m_action_2?.Invoke(value_2);
                    break;
                case 3:
                    C2S_BuildRoom value_3 = C2S_BuildRoom.Parse(bytes);
                    m_action_3?.Invoke(value_3);
                    break;
                case 4:
                    S2C_RoomIP value_4 = S2C_RoomIP.Parse(bytes);
                    m_action_4?.Invoke(value_4);
                    break;
                case 5:
                    C2S_StartLinkRoom value_5 = C2S_StartLinkRoom.Parse(bytes);
                    m_action_5?.Invoke(value_5);
                    break;
                case 6:
                    C2S_EndLinkRoom value_6 = C2S_EndLinkRoom.Parse(bytes);
                    m_action_6?.Invoke(value_6);
                    break;
                case 7:
                    C2S_LinkRoom value_7 = C2S_LinkRoom.Parse(bytes);
                    m_action_7?.Invoke(value_7);
                    break;
                case 8:
                    S2C_Response_LinkRoom value_8 = S2C_Response_LinkRoom.Parse(bytes);
                    m_action_8?.Invoke(value_8);
                    break;
                case 9:
                    S2C_RoomState value_9 = S2C_RoomState.Parse(bytes);
                    m_action_9?.Invoke(value_9);
                    break;
                case 10:
                    C2C_Talk value_10 = C2C_Talk.Parse(bytes);
                    m_action_10?.Invoke(value_10);
                    break;
            }
        }

        private Action<C2S_StateChange> m_action_1 = null;
        public void Handle(C2S_StateChange value)
        {
            m_action_1?.Invoke(value);
        }

        public void AddListener(Action<C2S_StateChange> action)
        {
            m_action_1 += action;
        }

        public void RemoveListener(Action<C2S_StateChange> action)
        {
            m_action_1 -= action;
        }
        private Action<C2S_S2C_Talk> m_action_2 = null;
        public void Handle(C2S_S2C_Talk value)
        {
            m_action_2?.Invoke(value);
        }

        public void AddListener(Action<C2S_S2C_Talk> action)
        {
            m_action_2 += action;
        }

        public void RemoveListener(Action<C2S_S2C_Talk> action)
        {
            m_action_2 -= action;
        }
        private Action<C2S_BuildRoom> m_action_3 = null;
        public void Handle(C2S_BuildRoom value)
        {
            m_action_3?.Invoke(value);
        }

        public void AddListener(Action<C2S_BuildRoom> action)
        {
            m_action_3 += action;
        }

        public void RemoveListener(Action<C2S_BuildRoom> action)
        {
            m_action_3 -= action;
        }
        private Action<S2C_RoomIP> m_action_4 = null;
        public void Handle(S2C_RoomIP value)
        {
            m_action_4?.Invoke(value);
        }

        public void AddListener(Action<S2C_RoomIP> action)
        {
            m_action_4 += action;
        }

        public void RemoveListener(Action<S2C_RoomIP> action)
        {
            m_action_4 -= action;
        }
        private Action<C2S_StartLinkRoom> m_action_5 = null;
        public void Handle(C2S_StartLinkRoom value)
        {
            m_action_5?.Invoke(value);
        }

        public void AddListener(Action<C2S_StartLinkRoom> action)
        {
            m_action_5 += action;
        }

        public void RemoveListener(Action<C2S_StartLinkRoom> action)
        {
            m_action_5 -= action;
        }
        private Action<C2S_EndLinkRoom> m_action_6 = null;
        public void Handle(C2S_EndLinkRoom value)
        {
            m_action_6?.Invoke(value);
        }

        public void AddListener(Action<C2S_EndLinkRoom> action)
        {
            m_action_6 += action;
        }

        public void RemoveListener(Action<C2S_EndLinkRoom> action)
        {
            m_action_6 -= action;
        }
        private Action<C2S_LinkRoom> m_action_7 = null;
        public void Handle(C2S_LinkRoom value)
        {
            m_action_7?.Invoke(value);
        }

        public void AddListener(Action<C2S_LinkRoom> action)
        {
            m_action_7 += action;
        }

        public void RemoveListener(Action<C2S_LinkRoom> action)
        {
            m_action_7 -= action;
        }
        private Action<S2C_Response_LinkRoom> m_action_8 = null;
        public void Handle(S2C_Response_LinkRoom value)
        {
            m_action_8?.Invoke(value);
        }

        public void AddListener(Action<S2C_Response_LinkRoom> action)
        {
            m_action_8 += action;
        }

        public void RemoveListener(Action<S2C_Response_LinkRoom> action)
        {
            m_action_8 -= action;
        }
        private Action<S2C_RoomState> m_action_9 = null;
        public void Handle(S2C_RoomState value)
        {
            m_action_9?.Invoke(value);
        }

        public void AddListener(Action<S2C_RoomState> action)
        {
            m_action_9 += action;
        }

        public void RemoveListener(Action<S2C_RoomState> action)
        {
            m_action_9 -= action;
        }
        private Action<C2C_Talk> m_action_10 = null;
        public void Handle(C2C_Talk value)
        {
            m_action_10?.Invoke(value);
        }

        public void AddListener(Action<C2C_Talk> action)
        {
            m_action_10 += action;
        }

        public void RemoveListener(Action<C2C_Talk> action)
        {
            m_action_10 -= action;
        }
    }
}