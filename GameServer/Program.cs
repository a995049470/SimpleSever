using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LUDP;
using LPTC;

namespace GameServer
{
    class Program
    {
        static UdpUser c1;
        static void Test()
        {
            bool isFirst = c1 == null;
            c1 = c1 ?? UdpUser.ConnectToServer();
            if (isFirst)
            {
                Task.Factory.StartNew(async () =>
                {
                    while(true)
                    {
                        var received = await c1.Receive();
                        var msg = received.msg;
                        var value = C2S_S2C_Talk.Parse(msg);
                        Console.WriteLine("客户端接收到了:" + value.talk);
                    }
                   
                });
            }
            string str = "今天天气不错!";
            c1.C2S_Send(new C2S_S2C_Talk()
            {
                talk = str
            });
            Console.WriteLine("发送成功");
        }

        static UdpUser c2;
        static void Test2()
        {
            bool isFirst = c2 == null;
            c2 = c2 ?? UdpUser.ConnectToServer();
            if (isFirst)
            {
                Task.Factory.StartNew(async () =>
                {
                    while (true)
                    {
                        var received = await c2.Receive();
                        var msg = received.msg;
                        var value = C2S_S2C_Talk.Parse(msg);
                        Console.WriteLine("客户端接收到了:" + value.talk);
                    }

                });
            }
            string str = "今天天气不错C2!";
            c2.C2S_Send(new C2S_S2C_Talk()
            {
                talk = str
            });
            Console.WriteLine("发送成功");
        }



        static void Main(string[] args)
        {
            var cloud = new CloudServer();
            Console.WriteLine("启动游戏服务器!");
            while (true)
            {

            }

            //start listening for messages and copy the messages back to the client
            //测试
            //Task.Factory.StartNew(async () =>
            //{
            //    while (true)
            //    {
            //        Console.WriteLine("正在接受");
            //        var received = await server.Receive();
            //        var msg = received.msg;
            //        var value = C2S_S2C_Talk.Parse(msg);
            //        Console.WriteLine($"服务器接受到了 : {value.talk}  {received.sender}");
            //        value.talk += "--来自服务器复读!";
            //        server.S2C_Send(received.sender, value);
            //    }
            //});


            //while (true)
            //{
            //    if (Console.ReadKey().Key == ConsoleKey.A)
            //    {
            //        Console.WriteLine("按下了A");
            //        Test();
            //    }
            //    else if (Console.ReadKey().Key == ConsoleKey.W)
            //    {
            //        Console.WriteLine("按下了W");
            //        Test2();
            //    }
            //}
        }

       
    }
}
