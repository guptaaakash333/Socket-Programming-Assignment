using SocketClient.Services;

namespace SocketClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Client client = new Client();

            await client.ConnectAsync();

            //Console.Write("Enter Request (Example SetA-Two): ");
            //string request = Console.ReadLine();
            //await client.SendMessageAsync(request);

            await client.SendMessageAsync("SetA-Two");

            await client.ReceiveResponseAsync();

        }
    }
}
