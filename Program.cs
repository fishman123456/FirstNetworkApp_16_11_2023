using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FirstNetworkApp_16_11_2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 0 конфигурация сервера
            string serverIpStr = "25.45.70.14"; // ip адрес сокета сервера
            int serverPort = 2620; // порт сокета сервера
            // 1 подготовить endpoint для работы сервера (
            IPAddress serverIp = IPAddress.Parse(serverIpStr);
            IPEndPoint serverEndpoint = new IPEndPoint(serverIp, serverPort);
            // 2 создать сокет сервера и присоединить его к endpoint
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(serverEndpoint); // связывание с сервером и подключение к нему
            // 3 перевести сокет в режим прослушивания входящих подключений
            server.Listen(1);
            // 4 начать ожидание входящего подключения
            Console.WriteLine("Ожидание входящего подключения ....");
            Socket client = server.Accept();
            Console.WriteLine($"Произошло подключение: {client.RemoteEndPoint}");
            Console.ReadLine();
            // 5 далее осуществляется работа с подключеным клиентом telnet 25.41.112.239 1024   ------telnet  25.45.70.14 2620
            // отправим сообщение клиенту
            string message = $"Helloy from server, connections time {DateTime.Now}";
            client.Send(Encoding.ASCII.GetBytes(message));
            Console.WriteLine($"отправлено сообщение {message}");
            Console.ReadLine();
        }
    }
}
