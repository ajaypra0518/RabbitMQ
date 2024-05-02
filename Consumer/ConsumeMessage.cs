using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Consumer
{
    public static class QueueConsumer
    {

        public static void Consume(IModel channel)
        {
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("demo-queue-one",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                Console.WriteLine("channel 1");
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            var consumer2 = new EventingBasicConsumer(channel);
            consumer2.Received += (sender, e) => {
                Console.WriteLine("channel 2");
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-queue", true, consumer);
            channel.BasicConsume("demo-queue-one", true, consumer2);

            // if we attach diffrent queue to same basic consumer then it will trigger that consumer like the below code will trigger line 28 consumer.Received += 
            // channel.BasicConsume("demo-queue", true, consumer);
            // channel.BasicConsume("demo-queue-one", true, consumer);

            Console.WriteLine("Consumer started");
            Console.ReadLine();


        }
    }
}
