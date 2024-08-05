using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Net.Security;


namespace Network_Application_Development_C_
{
    //Добавляем многопоточность в чат позволяя серверной части получать сообщения сразу от нескольких респондентов.Перепишем многопоточность с помощью Task
    internal class Program
    {
        static void Main(string[] args) { 


        //{
        //    Server server = new Server;
        //    server.StartServer();


        Message message = new Message() { Text = "Hi", DateTime = DateTime.Now, NicknameFrom = "Amy", NicknameTo = "All" };
        string json = message.SerializeMessageToJson();
        Console.WriteLine(json);
            //Message? msgDeserialize = Message.DeserializeMessageFromJson(json);
            Console.ReadKey();
        }



    public static void Server(string name)
    {

        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint IPEndPoint = new IPEndPoint(IPAddress.Any, 0);


        Console.WriteLine("Server is waiting");

        while (true)
        {
            byte[] buffer = udpClient.Receive(ref IPEndPoint);
            var messageText = Encoding.UTF8.GetString(buffer);


            //Task.Run(() =>
            //{
            ThreadPool.QueueUserWorkItem(obj =>
            {
                Message message = Message.DeserializeMessageFromJson(messageText);
                message.Print();

                byte[] reply = Encoding.UTF8.GetBytes("Сообщение получено");
                udpClient.Send(reply, reply.Length, IPEndPoint);
            });
            
        }

        }
    }
        
}