using GenericConsumer.Handlers;
using GenericConsumer.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GenericConsumer
{
    // Define a consumer class
    public class QueueConsumer
    {
        private readonly IConnection _connection;
        private readonly Dictionary<string, IMessageHandler> _messageHandlers;

        public QueueConsumer(IConnection connection, Dictionary<string, IMessageHandler> messageHandlers)
        {
            _connection = connection;
            _messageHandlers = messageHandlers;
        }

        public void StartConsuming()
        {
            while (true) {
                using (var channel = _connection.CreateModel())
                {
                    foreach (var queueName in _messageHandlers.Keys)
                    {
                        //channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //    channel.QueueDeclare(queueName,
                    //durable: true,
                    //exclusive: false,
                    //autoDelete: false,
                    //arguments: null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);

                            // Get the message handler for the current queue
                            if (_messageHandlers.TryGetValue(queueName, out var handler))
                            {
                                handler.HandleMessage(message);
                            }
                            else
                            {
                                Console.WriteLine($"No message handler found for queue: {queueName}. Using default handler.");
                                // If no specific handler is found, use the default handler
                                var defaultHandler = new DefaultMessageHandler();
                                defaultHandler.HandleMessage(message);
                            }
                        };

                        //channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                        channel.BasicConsume(queueName, true, consumer);
                    }
                    //Console.ReadLine();
                }
                //Console.WriteLine("Waite for 5 seconnd");
                Thread.Sleep(1000);
            }
        }
    }
}
