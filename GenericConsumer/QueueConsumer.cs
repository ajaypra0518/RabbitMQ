using GenericConsumer.Handlers;
using GenericConsumer.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericConsumer
{
    // Define a consumer class
    public class QueueConsumer:IDisposable
    {
        private readonly IConnection _connection;
        private readonly Dictionary<string, IMessageHandler> _messageHandlers;
        private readonly IModel _channel;

        public QueueConsumer(IConnection connection, Dictionary<string, IMessageHandler> messageHandlers)
        {
            _connection = connection;
            _messageHandlers = messageHandlers;
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }

        public void StartConsuming()
        {

            
                foreach (var queueName in _messageHandlers.Keys)
                {
                    //channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //    channel.QueueDeclare(queueName,
                    //durable: true,
                    //exclusive: false,
                    //autoDelete: false,
                    //arguments: null);

                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Task.Run(() =>{
                            try
                            {
                                Thread t = Thread.CurrentThread;

                                Console.WriteLine("Thread Name: {0}", t.ManagedThreadId);
                                // Get the message handler for the current queue
                                if (_messageHandlers.TryGetValue(queueName, out var handler))
                                {
                                    Console.WriteLine("Handler start Thread : {0}", t.ManagedThreadId);
                                    handler.HandleMessage(message+ t.ManagedThreadId);
                                    Console.WriteLine("Handler end Thread : {0}", t.ManagedThreadId);
                                }
                                else
                                {
                                    Console.WriteLine($"No message handler found for queue: {queueName}. Using default handler.");
                                    // If no specific handler is found, use the default handler
                                    var defaultHandler = new DefaultMessageHandler();
                                    defaultHandler.HandleMessage(message);
                                }
                                _channel.BasicAck(ea.DeliveryTag, false);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing message: {ex.Message}");
                                // Reject (Nack) the message if processing fails
                                _channel.BasicNack(ea.DeliveryTag, false, true);
                            }
                        });
                        

                        
                    };

                    //channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    _channel.BasicConsume(queueName, false, consumer);
                }
                //Console.ReadLine();
            //Console.WriteLine("Waite for 5 seconnd");
        }
    }
}
