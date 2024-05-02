using GenericConsumer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConsumer.Handlers
{
    // Implement a default message handler
    public class DefaultMessageHandler : IMessageHandler
    {
        public void HandleMessage(string message)
        {
            Console.WriteLine($"Default message handler received message: {message}");
        }
    }
}
