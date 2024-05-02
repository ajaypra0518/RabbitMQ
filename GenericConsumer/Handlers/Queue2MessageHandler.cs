using GenericConsumer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConsumer.Handlers
{
    public class Queue2MessageHandler : IMessageHandler
    {
        public void HandleMessage(string message)
        {
            Console.WriteLine("Queue2MessageHandler: "+ message);
        }
    }
}
