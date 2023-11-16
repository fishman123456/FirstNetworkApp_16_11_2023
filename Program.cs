using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstNetworkApp_16_11_2023
{
    internal class Program
    {

     static void RunServer(string serverIpStr, int serverPort)
        {
            // 0 конфигурация сервера
            //string serverIpStr = "25.45.70.14"; // ip адрес сокета сервера
            //int serverPort = 2620; // порт сокета сервера
            // 1 подготовить endpoint для работы сервера 
            IPAddress serverIp = IPAddress.Parse(serverIpStr);
            IPEndPoint serverEndpoint = new IPEndPoint(serverIp, serverPort);
            // 2 создать сокет сервера и присоединить его к endpoint
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(serverEndpoint); // связывание с сервером и подключение к нему
            // 3 перевести сокет в режим прослушивания входящих подключений
            server.Listen(1);
            // 4 начать ожидание входящего подключения
            Console.WriteLine("server>Ожидание входящего подключения ....");
            Socket client = server.Accept();
            Console.WriteLine($"server>Произошло подключение: {client.RemoteEndPoint}");
            Console.ReadLine();
            // 5 далее осуществляется работа с подключеным клиентом telnet 25.41.112.239 1024   ------telnet  25.45.70.14 2620
            // отправим сообщение клиенту
            //string message = $"Helloy from server, connections time {DateTime.Now}";
            //client.Send(Encoding.ASCII.GetBytes(message));
            //Console.WriteLine($"отправлено сообщение {message}");
            //Console.ReadLine();
            Console.WriteLine("server>Завершение работы сервера...");
            Console.ReadLine();
        }
        // Процедура запуска линейного алгоритма работы клиента (активного сокета)
        static void RunClient(string serverIpStr, int serverPort)
        {
            // СОЗДАДИМ ПРОСТЕЙШИЙ КЛИЕНТ
            // Клиент инициирует подключение к серверу
            // Вывод сообщение о том, что произошло подключение

            // ХОД РАБОТЫ:
            
            // 1. Создадим сокет клиента 
            Socket client = new Socket (AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            
            // 2. Подготовить endpoint  сервера
            IPAddress serverIp = IPAddress.Parse(serverIpStr);
            IPEndPoint serverEndpoint = new IPEndPoint(serverIp, serverPort);

            // 3. Подключиться к серверу
            Console.WriteLine("client>Подключение к серверу...");
            client.Connect(serverEndpoint);
            Console.WriteLine($"client>Произошло подключение ...{client.LocalEndPoint}-> {client.RemoteEndPoint}");
            Console.WriteLine("client>Завершение работы клиента...");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            // Запустим сервер и клиент в разных потоках
            string serverIpStr = "25.45.70.14"; // ip адрес сокета сервера
            int serverPort = 2620; // порт сокета сервера
            Thread serverThread = new Thread(() => RunServer(serverIpStr, serverPort));
            Thread clientThread = new Thread(() => RunClient(serverIpStr, serverPort));
            serverThread.Start();
            clientThread.Start();

            serverThread.Join();
            clientThread.Join();
        }
    }
}
