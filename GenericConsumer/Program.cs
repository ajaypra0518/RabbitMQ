using GenericConsumer.Handlers;
using GenericConsumer.Interface;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace GenericConsumer
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
            {
                //using var channel = connection.CreateModel();

                var messageHandlers = new Dictionary<string, IMessageHandler>
                {
                    { "demo-queue", new Queue1MessageHandler() },
                    { "demo-queue-one", new Queue2MessageHandler() },
                    { "queue3", new Queue3MessageHandler() }
                    // Add more mappings as needed
                };



                // Create a consumer with the message handlers dictionary
                var consumer = new QueueConsumer(connection, messageHandlers);

                // Start consuming messages from each queue
                consumer.StartConsuming();

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }

        }
    }
}
