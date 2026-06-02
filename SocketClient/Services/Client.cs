using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient.Services
{
    internal class Client
    {
        private readonly TcpClient _client;

        public Client()
        {
            _client = new TcpClient();
        }

        public async Task ConnectAsync()
        {
            //Console.Write("Enter Server IP : ");
            //string ip = Console.ReadLine();

            //Console.Write("Enter Port : ");
            //int port = int.Parse(Console.ReadLine());

            //await _client.ConnectAsync(ip, port);

            await _client.ConnectAsync("127.0.0.1", 5000);

            Console.WriteLine("Connect To Server");
        }


        public async Task SendMessageAsync(string message)
        {
            NetworkStream stream = _client.GetStream();

            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            //await writer.WriteLineAsync(message);

            string encryptedMessage = EncryptionService.Encrypt(message);

            await writer.WriteLineAsync(encryptedMessage);

            //Console.WriteLine($"Sent : {message}");
            Console.WriteLine($"encryptedMessage Sent : {encryptedMessage}");
        }

        public async Task ReceiveResponseAsync()
        {
            NetworkStream stream = _client.GetStream();

            StreamReader reader = new StreamReader(stream);

            while (true)
            {
                string? response = await reader.ReadLineAsync();

                if (response == null)
                {
                    break;
                }
                Console.WriteLine($"Server : {response}");
                string decrypted = EncryptionService.Decrypt(response);

                Console.WriteLine($"Server decrypted : {decrypted}");

            }
        }


    }
}
