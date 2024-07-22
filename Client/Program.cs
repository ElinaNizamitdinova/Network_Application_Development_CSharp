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
            SentMessage(args[0]);

            //new Task(() => SentMessage(args[0])).RunSynchronously();

            //Console.ReadKey();

        }

        public static void SentMessage(string From, string ip = "127.0.0.1")
        {
            //Message message = new Message() { Text = text, DateTime = DateTime.Now, NicknameFrom = From, NicknameTo = "Server" };
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
            //string json = message.SerializeMessageToJson();

            bool fl= true;
            while (fl)
            {
                string messageText;
                do
                {
                    Console.WriteLine("Enter your message");
                    messageText = Console.ReadLine();
                    if (messageText.ToLower() == "exit")
                    {
                        Console.WriteLine("Shutdown");
                        fl = false;
                        Console.ReadKey();
                    }
                }
                while (string.IsNullOrEmpty(messageText));

                Message message = new Message() { Text = messageText, DateTime = DateTime.Now, NicknameFrom = From, NicknameTo = "Server" };
                string json = message.SerializeMessageToJson();



                byte[] data = Encoding.UTF8.GetBytes(json);
                udpClient.Send(data, data.Length, iPEndPoint);

                //byte[] buffer = udpClient.Receive(ref iPEndPoint);
                //var answer = Encoding.UTF8.GetString(buffer);
                //Console.WriteLine(answer);

            }
        }
    }
}
