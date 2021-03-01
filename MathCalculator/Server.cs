using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MathCalculator
{
    class Server
    {
        public void Start()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 3001;
                IPAddress localAddress = IPAddress.Loopback;
                server = new TcpListener(localAddress, port);
                server.Start();
                while (true)
                {
                    TcpClient connectTcpClient = server.AcceptTcpClient();
                    Task.Run(() =>
                    {
                        TcpClient tempClient = connectTcpClient;
                        DoClient(tempClient);
                    });
                }
                server.Stop();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        private void DoClient(TcpClient tempClient)
        {
            Console.WriteLine("server activated");
            Stream ns = tempClient.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            string message = sr.ReadLine();
            string[] array = message.Split(' ');
            int num1 = Int32.Parse(array[1]);
            int num2 = Int32.Parse(array[2]);
            sw.WriteLine($"Result {num1+num2}");
            ns.Close();
            tempClient.Close();
        }
    }
}
