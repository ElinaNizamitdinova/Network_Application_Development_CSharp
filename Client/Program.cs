using Network_Application_Development_C_;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {

                SentMessage("Liza",i);
            }

        }

        public static void SentMessage(string From, int i, string ip = "127.0.0.1")
        {
            //   Message message = new Message() { Text = text, DateTime = DateTime.Now, NicknameFrom = From, NicknameTo = "Server" };
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
            //  string json = message.SerializeMessageToJson();

           
            

                string messageText = "Hello"+i;
                //do
                //{
                //    Console.WriteLine("Enter your message");
                //    messageText = Console.ReadLine();
                //}
                //while (string.IsNullOrEmpty(messageText));
                Message message = new Message() { Text = messageText, DateTime = DateTime.Now, NicknameFrom = From, NicknameTo = "Server" };
                string json = message.SerializeMessageToJson();


                byte[] data = Encoding.UTF8.GetBytes(json);
                udpClient.Send(data, data.Length, iPEndPoint);

                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                var answer = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(answer);
            

        }
    }
}
