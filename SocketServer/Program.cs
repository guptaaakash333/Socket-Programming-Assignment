using SocketServer.Services;

namespace SocketServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Server server = new Server();   

            await server.StartAsync();



        }
    }
}
