namespace Seminar3HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => { Server.ServerWork(); });
        }
    }
}
