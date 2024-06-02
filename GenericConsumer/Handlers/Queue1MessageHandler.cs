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


            if (message.Contains("10") || message.Contains("19"))
            {
                throw new Exception("throw exception self");

            }
            else
            {
                Console.WriteLine("Queue1MessageHandler: " + message);
            }
        }
    }
}
