using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Network_Application_Development_C_
{
    public enum Commands
    {
        Register,
        Delete
    }
    public class Message
    {
        public Commands command {  get; set; }
        public string Text { get; set; }
        public DateTime  DateTime { get; set; }
        public string NicknameFrom { get; set; }
        public string NicknameTo { get; set; }
        public string SerializeMessageToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Message? DeserializeMessageFromJson(string message) => JsonSerializer.Deserialize<Message>(message);

        public void Print()
        {
            Console.WriteLine($"{this.DateTime} Received a message from  {this.NicknameFrom}: " +
                $"{this.Text}");        }

        public override string ToString()
        {
            return $"{this.DateTime} Received a message from {this.NicknameFrom}:" +
                $"{this.Text}";
        }
    }


}
