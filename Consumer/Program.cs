using RabbitMQ.Client;
using System;

namespace Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                //Uri = new Uri("amqp://guest:guest@localhost:5672"),
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                Port = 5672,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            QueueConsumer.Consume(channel);

            Console.WriteLine("Consumer End");
            Console.ReadLine();

        }
    }
}
