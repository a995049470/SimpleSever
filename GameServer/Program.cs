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
        static UdpUser client;
        static void Test()
        {
            bool isFirst = client == null;
            client = client ?? UdpUser.ConnectTo("127.0.0.1", 10186);
            if (isFirst)
            {
                Task.Factory.StartNew(async () =>
                {
                    while(true)
                    {
                        var received = await client.Receive();
                        var msg = received.msg;
                        var value = C2S_S2C_Talk.Parse(msg);
                        Console.WriteLine("客户端接收到了:" + value.talk);
                    }
                   
                });
            }
            string str = "今天天气不错!";
            client.C2S_Send(new C2S_S2C_Talk()
            {
                talk = str
            });
            Console.WriteLine("发送成功");


        }

        static void Main(string[] args)
        {

            var server = new UdpListener();

            //start listening for messages and copy the messages back to the client
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    Console.WriteLine("正在接受");
                    var received = await server.Receive();
                    var msg = received.msg;
                    var value = C2S_S2C_Talk.Parse(msg);
                    Console.WriteLine("服务器接受到了 : "+ value.talk);
                    value.talk += "--来自服务器复读!";
                    server.S2C_Send(received.sender, value);
                }
            });


            while (true)
            {
                //if (Console.ReadKey().Key == ConsoleKey.A)
                //{
                //    Console.WriteLine("按下了A");
                //    Test();
                //}
            }
        }

       
    }
}
