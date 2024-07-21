using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Net.Security;


namespace Network_Application_Development_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Message message = new Message() { Text = "Hi", DateTime = DateTime.Now, NicknameFrom = "Amy", NicknameTo = "All" };
            string json = message.SerializeMessageToJson();
            Console.WriteLine(json);
            Message? msgDeserialize = Message.DeserializeMessageFromJson(json);
            Server("Hello");
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

                ThreadPool.QueueUserWorkItem(obj => {
                    Message message = Message.DeserializeMessageFromJson(messageText);
                    message.Print();

                    byte[] reply = Encoding.UTF8.GetBytes("Сообщение получено");
                    udpClient.Send(reply,reply.Length,IPEndPoint);
                });

            }
            
        }
    }
        
}