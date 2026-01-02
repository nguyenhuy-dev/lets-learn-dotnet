using ChatProtocol;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            int clientId = 1;

            var endPoint = new IPEndPoint(IPAddress.Loopback, Constants.DefaultChatPort);
            var serverSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(endPoint);

            Console.WriteLine($"Listening... (port {endPoint.Port})");

            serverSocket.Listen();

            var clientHandlers = new List<Task>();

            while (true)
            {
                var clientSocket = await serverSocket.AcceptAsync();
                var t = HandleClientRequestAsync(clientSocket, clientId++);
                clientHandlers.Add(t);
            }

            // Task.WaitAll(clientHandlers);
        }

        private static async Task HandleClientRequestAsync(Socket clientSocket, int clientId)
        {
            Console.WriteLine($"Client {clientId} connected!");

            var welcomeBytes = Encoding.UTF8.GetBytes(Constants.WelcomeText);
            await clientSocket.SendAsync(welcomeBytes);

            var buffer = new byte[1024];

            while (true)
            {
                var r = await clientSocket.ReceiveAsync(buffer);
                var msg = Encoding.UTF8.GetString(buffer, 0, r);

                if (msg.Equals(Constants.CommandShutdown))
                {
                    CloseConnection(clientSocket);
                    Console.WriteLine($"Client {clientId}: disconnected");
                    break;
                }

                Console.WriteLine($"Client {clientId}: {msg}");
            }       
        }

        private static void CloseConnection(Socket clientSocket)
        {
            clientSocket.Close();
        }
    }
}
