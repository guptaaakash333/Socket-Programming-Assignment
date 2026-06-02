using SocketServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Services
{
    internal class Server
    {
        private readonly TcpListener _listener;

        public Server()
        {
            _listener = new TcpListener(IPAddress.Any, 5000);
        }

        public async Task StartAsync()
        {
            _listener.Start();

            Console.WriteLine("Server Started On Port 5000");

            while (true)
            {
                //Multi Client Support
                TcpClient client = await _listener.AcceptTcpClientAsync();

                Console.WriteLine("Client Connected");

                _ = Task.Run(() => HandleClientAsync(client));
            }
        }



        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();

                using StreamReader reader = new StreamReader(stream);

                //string? message = await reader.ReadLineAsync();
                string? encryptedMessage = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(encryptedMessage))
                {
                    Console.WriteLine("No data found");
                }

                string message = EncryptionService.Decrypt(encryptedMessage);

                Console.WriteLine($"Received Decrypt msg : {message}");

                if (string.IsNullOrWhiteSpace(message))
                {
                    return;
                }

                string[] parts = message.Split('-');

                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid Format");

                    return;
                }

                string setName = parts[0];
                string keyName = parts[1];

                Console.WriteLine($"Set Name : {setName}");
                Console.WriteLine($"Key Name : {keyName}");

                //Find Set
                if (!DataStore.Data.TryGetValue(setName, out var subset))
                {
                    Console.WriteLine("Set Not Found");

                    return;
                }

                //Find key
                if (!subset.TryGetValue(keyName, out int value))
                {
                    Console.WriteLine("Key Not Found");
                    return;
                }

                using StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                // Send current time 
                try
                {
                    for (int i = 0; i < value; i++)
                    {
                        string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                        //await writer.WriteLineAsync(currentTime);
                        string encryptedResponse = EncryptionService.Encrypt(currentTime);

                        await writer.WriteLineAsync(encryptedResponse);

                        //string encryptedMessage = EncryptionService.Encrypt(message);
                        //await writer.WriteLineAsync(encryptedMessage);

                        await Task.Delay(1000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
                finally
                {
                    client.Close();

                    Console.WriteLine("Client Disconnected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
