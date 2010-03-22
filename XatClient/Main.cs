using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace XatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            // Faig un objecte de la classe client li paso la ip del servidor del cole
            // Esta es mi ip
			Client client = new Client("192.168.130.81", 9898);
			
			if (client.ConnectToServer())
			{
				while (true)
				{
                    try
                    {
                        client.WriteLine("Hola! Sóc el client enviant un missatge!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("S'ha desconectat el servidor");
                        Console.ReadKey(true);
                    }
                        
                }
			}
		}
	}
	
	public class Client
	{
		private NetworkStream netStream;
		private StreamReader readerStream;
		private StreamWriter writerStream;
		private IPEndPoint server_endpoint;
		private TcpClient tcpClient;
		
		public Client(string ip, int port)
		{
			IPAddress address = IPAddress.Parse(ip);
            // Lloc per agrupar la ip i el port conjuntament
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
		
		public bool ConnectToServer()
		{
			try
			{
				// tcpClient = new TcpClient(server_endpoint);
                // Aqui pongo la ip donde me quiero conectar
                tcpClient = new TcpClient("192.168.130.75", 9898);
				
				netStream = tcpClient.GetStream();
				readerStream = new StreamReader(netStream);
				writerStream = new StreamWriter(netStream);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				Console.WriteLine(e.Message);
				return false;
			}

			Console.WriteLine("M'he connectat amb el servidor");

			return true;		
		}
	}
}
