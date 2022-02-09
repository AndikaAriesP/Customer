using System;
using RabbitMQ
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class RMQ
{
    public ConnectionFactory connectionFactory;
    public IConnection connection;
    public IModel channel;
    private bool isReceiving = false;
    public void InitRMQConnection(string host = "localhost", int port = 5672, string user =
    "guest", string pass = "guest")
    {
        connectionFactory = new ConnectionFactory();
        connectionFactory.HostName = host;
        //connectionFactory.Port = port;
        connectionFactory.UserName = user;
        connectionFactory.Password = pass;
    }
    public void CreateRMQConnection()
    {
        connection = connectionFactory.CreateConnection();
        Console.WriteLine("Koneksi " + (connection.IsOpen ? "Berhasil!" : "Gagal!"));
    }
    public void WaitingMessage(string queue_name)
    {
        using (channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queue_name,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Pesan diterima: {0}", message);
            };
            channel.BasicConsume(queue: queue_name,
            autoAck: true,
            consumer: consumer);
            Console.WriteLine(" Tekan [enter] untuk memutus koneksi.");
            Console.ReadLine();
            Disconnect();
        }
    }
    public void Disconnect()
    {
        isReceiving = false;
        channel.Close();
        channel = null;
        Console.WriteLine("Channel ditutup!");
        if (connection.IsOpen)
        {
            connection.Close();
        }
        Console.WriteLine("Koneksi diputus!");
        connection.Dispose();
        connection = null;
    }
}
