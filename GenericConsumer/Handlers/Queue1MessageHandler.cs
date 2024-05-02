using GenericConsumer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConsumer.Handlers
{
    public class Queue1MessageHandler : IMessageHandler
    {
        public void HandleMessage(string message)
        {
            Console.WriteLine("Queue1MessageHandler: "+ message);
        }
    }
}
