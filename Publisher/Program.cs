using RabbitMQ.Client;
using System;
using System.Net.NetworkInformation;

namespace Publisher
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start publishing");
            var factory = new ConnectionFactory
            {
                //Uri = new Uri("amqp://guest:guest@localhost:5672"),
                UserName= "guest",
                Password="guest",
                HostName ="localhost",
                Port =5672,
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

           
            QueueProducer.Publish(channel);

            Console.WriteLine("Message send");
            Console.ReadLine();
            
        }
    }
}