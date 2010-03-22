using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace XatServer
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hola, sóc el servidor!");
			
			Server servidor = new Server("192.168.130.81", 6969);
			
			if (!servidor.Start())
			{
				Console.WriteLine("No puc engegar el servidor!");
			}
			
			if (servidor.WaitForAClient())
			{
				// Escribim tot el que ens envii el client
				while (true)
				{
                    try
                    {
                        Console.WriteLine("El client diu: " + servidor.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("S'ha desconectat el client del servidor");
                        // Volvemos a llamar al metodo para que cuando un cliente se desconecte no se salga y siga escuhando el servidor
                        servidor.WaitForAClient();
                    }
					
				}
				
				// server.WriteLine("Hi!"); 
			}
		}
	}
	
	public class Server
	{
		private NetworkStream netStream;
		private StreamReader readerStream;
		private StreamWriter writerStream;
		private IPEndPoint server_endpoint;
		private TcpListener listener;
		//Constructor del Server li pasem la ip i un port
		public Server(string ip, int port)
		{
			IPAddress address = IPAddress.Parse(ip);
			server_endpoint = new IPEndPoint(address, port);
		}
		
		public string ReadLine()
		{
			return readerStream.ReadLine();
		}
		
		public void WriteLine(string str)
		{
			writerStream.WriteLine(str);
			writerStream.Flush();
		}
		
		public bool Start()
		{
			try
			{
//				listener = new TcpListener(server_endpoint);
                //Creem el TcpListener que es una classe que serveix per escoltar
				listener = new TcpListener(9898);

				listener.Start(); //start server
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				return false;
			}
		
			Console.WriteLine("Servidor engegat, escoltant a {0}:{1}", server_endpoint.Address, server_endpoint.Port);
			
			return true;
		}
		
		public bool WaitForAClient()
		{
            // AcceptSocket : Esperem una connexio d'un client
            // Esperarà fins que es conecti un client
			Socket serverSocket = listener.AcceptSocket();
			
			try
			{
				if (serverSocket.Connected)
				{
                    // Aquí creem un buffer(Stream fluxe de dades) per poder crear un writerStream i un readerStream
					netStream = new NetworkStream(serverSocket);
					// Aquí podem llegir o escriure coses
					writerStream = new StreamWriter(netStream);
					readerStream = new StreamReader(netStream);
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				return false;
			}

			Console.WriteLine("Un client s'ha connectat!");
					
			return true;
		}
	}
}
