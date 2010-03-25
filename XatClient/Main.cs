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
            Boolean desconectar = true;

            if (client.ConnectToServer())
            {
                while ((true) && desconectar == true)
                {
                    try
                    {
                        //Aqui el cliente envia todo el rato la misma frase
                        //client.WriteLine("Hola! Sóc el client enviant un missatge!");
                        //Con el siguiente codigo el cliente escribira por teclado y el servidor verá todo lo que vea
                        //Lo que el cliente escriba que la consola lo lea:Jucto la linea de abajo
                        //client.WriteLine(Console.ReadLine());

                        //Aqui comprobamos que cuando el cliente escriba disconnect se desconecte del servidor
                        //y como el while(true) hace como un bucle infinito en el momento que lea disconnect se desconectará
                        //declararé una variable tipo string para que cuando entre por primera vez escriba algo y k luego lo compruebe

                        String cadena = Console.ReadLine();
                        if (cadena == "disconnect")
                        {
                            desconectar = false;
                        }
                        //Aqui envio el mail y lo que leo seguido
                        //client.WriteLine(mail);
                        client.WriteLine(cadena);
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
        string mail;
        //Declaramos el constructor
        public Client(string ip, int port)
        {
            IPAddress address = IPAddress.Parse(ip);
            // Lloc per agrupar la ip i el port conjuntament
            server_endpoint = new IPEndPoint(address, port);
            mail = "araceli.linares78@gamil.com";
        }

        public string ReadLine()
        {
            return readerStream.ReadLine();
        }

        public void WriteLine(string str)
        {
            //Aqui envio el mail y lo que leo seguido
            //client.WriteLine(mail);
            writerStream.WriteLine(mail);
            //Arriba en el main llamo a la funcio WriteLine y le paso x parámetro cadena
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
            catch (Exception e)
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
