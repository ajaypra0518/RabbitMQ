using GenericConsumer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConsumer.Handlers
{
    public class Queue3MessageHandler : IMessageHandler
    {
        public void HandleMessage(string message)
        {
            Console.WriteLine("Queue3MessageHandler");
        }
    }
}
