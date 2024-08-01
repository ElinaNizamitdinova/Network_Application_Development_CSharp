using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Seminar3HomeWork
{
    public class Server
    {
        public static void ServerWork()
        {

            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint IPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Console.WriteLine("Server is waiting");

            while (true)
            {
                byte[] buffer = udpClient.Receive(ref IPEndPoint);
                var messageText = Encoding.UTF8.GetString(buffer);


                Task.Run(() =>
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Отправка прервана");
                        return;
                    }
                    Message message = Message.DeserializeMessageFromJson(messageText);
                    message.Print();

                    byte[] reply = Encoding.UTF8.GetBytes("Сообщение получено");
                    udpClient.Send(reply, reply.Length, IPEndPoint);
                },token);
            }
        }
    }
}

