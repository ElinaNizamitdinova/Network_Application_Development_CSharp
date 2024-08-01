using System.Text.Json;

namespace Seminar3HomeWork
{

    public class Message
    {
       
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string NicknameFrom { get; set; }
        public string NicknameTo { get; set; }
        public string SerializeMessageToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Message? DeserializeMessageFromJson(string json) => JsonSerializer.Deserialize<Message>(json);

        public void Print()
        {
            Console.WriteLine($"{this.DateTime} Received a message from  {this.NicknameFrom}: " +
                $"{this.Text}");
        }

        public override string ToString()
        {
            return $"{this.DateTime} Received a message from {this.NicknameFrom}:" +
                $"{this.Text}";
        }
    }

}

