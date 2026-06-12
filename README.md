SocketAssignmentSolution
│
├── SocketServer
│      Program.cs
│      Server.cs
│      DataStore.cs
│      EncryptionService.cs
│
└── SocketClient
       Program.cs
       Client.cs
       EncryptionService.cs




Before we write the client, make sure you understand:
Solution vs Project
Console Application
TCP Server
TcpListener
TcpClient
IP Address
Port Number
Dictionary
Constructor
async / await
while(true)





Q1 What is TcpClient?
Answer:
	TcpClient is a .NET class used to establish a TCP connection with a server.
	
	
Q2 What is NetworkStream?
Answer:
	NetworkStream is a stream used for sending and receiving data across a TCP connection.


Q3 Why AutoFlush?
Answer:
	AutoFlush ensures buffered data is immediately sent to the network instead of waiting in memory.


Q4 Why Task.Run?
Answer:
	Task.Run allows processing multiple client requests concurrently without blocking the server.


What Is Task.Delay?
Answer:
	Task.Delay asynchronously pauses execution without blocking the thread.


What Is 127.0.0.1?  ---->My Own Computer (Also called:localhost )
-> 127.0.0.1 is the standard IPv4 loopback address used to establish a network connection 
      to the same machine or computer being used by the end-user, 
- commonly referred to as localhost. 
- 127.0.0.1 is the loopback address used for communication with the same machine.



-------------------------------
SetA-Two
      │
      ▼
Split
      │
      ▼
SetA | Two
      │
      ▼
Find SetA
      │
      ▼
Find Two
      │
      ▼
Value = 2
      │
      ▼
Send Time 2 Times
