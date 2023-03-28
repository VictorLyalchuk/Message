using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    static string address = "127.0.0.1";
    static int port = 7777;
    private static void Main(string[] args)
    {
        string[] arr = new string[] { "Hello", "I'm busy", "I want to play on sony play station 4", "Give me your homework", "LOL" }; 
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port); // прийом повідомлень
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0); // відповідь на отримання
        UdpClient listener = new UdpClient(ipPoint);

        try
        {
            Console.WriteLine("Server started! Waiting for connection...");
            while (true)
            {
                // прийом повідомлень
                byte[] data = listener.Receive(ref remoteEndPoint);
                string msg = Encoding.Unicode.GetString(data);
                Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");


                // відповідь на отримання
                //string message = "Message was send!";
                Random random = new Random();
                string message = arr[random.Next(5)];

                data = Encoding.Unicode.GetBytes(message);
                listener.Send(data, data.Length, remoteEndPoint);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        listener.Close();
    }
}