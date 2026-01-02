using ChatProtocol;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, Constants.DefaultChatPort);
            var clientSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            await clientSocket.ConnectAsync(endPoint);
            var buffer = new byte[1024];

            var r = await clientSocket.ReceiveAsync(buffer);
            if (r == 0)
            {
                ShowConnectionError();
                return;
            }

            var welcomeText = Encoding.UTF8.GetString(buffer, 0, r);
            if (!Constants.WelcomeText.Equals(welcomeText))
            {
                ShowConnectionError();
                return;
            }

            Console.WriteLine(welcomeText);

            while (true)
            {
                Console.Write("Enter your message: ");
                var msg = Console.ReadLine();
                if (string.IsNullOrEmpty(msg))
                {
                    var bytes = Encoding.UTF8.GetBytes(Constants.CommandShutdown);
                    await clientSocket.SendAsync(bytes);
                    CloseConnection(clientSocket);
                    Console.WriteLine("Server is shutting down!");
                    return;
                } 
                else
                {
                    var bytes = Encoding.UTF8.GetBytes(msg);
                    await clientSocket.SendAsync(bytes);
                }
            }
        }

        private static void CloseConnection(Socket clientSocket)
        {
            clientSocket.Close();
        }

        private static void ShowConnectionError()
        {
            Console.WriteLine("Invalid protocol!");
        }
    }
}
