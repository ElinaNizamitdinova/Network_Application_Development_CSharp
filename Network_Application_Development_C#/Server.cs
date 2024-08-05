using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Network_Application_Development_C_
{
    
    public enum TypeSend
    {
        ToAll,
        ToOne,
        Defaultmes
    }
    public class Server
    {
        public Dictionary<string, IPEndPoint> Users { get; set; }
        private readonly UdpClient _udpClient;
        private IPEndPoint _IPEndPoint;
        private Manager _manager;
        public Server()
        {
            _udpClient = new UdpClient(12345);
            _IPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _manager = new Manager(this);
        }
        public Message Listen()
        {

            byte[] buffer = _udpClient.Receive(ref _IPEndPoint);
            var messageText = Encoding.UTF8.GetString(buffer);
            Message message = Message.DeserializeMessageFromJson(messageText);
            return message;
        }
        public void Send(TypeSend type, Message message)
        {
            byte[] reply = Encoding.UTF8.GetBytes(message.SerializeMessageToJson());
            switch (type)
            {
                case TypeSend.ToAll:
                    foreach(var item in Users.Values)
                    {
                        _udpClient.Send(reply, reply.Length, _IPEndPoint);
                    }
                    break;
                case TypeSend.ToOne:
                   if(Users.TryGetValue(message.NicknameTo, out IPEndPoint ep ))
                        _udpClient.Send(reply,reply.Length,_IPEndPoint);
                   break;
            }
        }
        public void StartServer()
        {
            Console.WriteLine("Server is waiting");

            while (true)
            {
                var mes = Listen();
                var typesend =_manager.Execute(mes, _IPEndPoint);
                //Task.Run(() =>
                //{
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Send(typesend,mes);
                   
                });

            }

        }

    }
}
