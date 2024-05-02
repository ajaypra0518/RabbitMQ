using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConsumer.Interface
{
    // Define an interface for message handling
    public interface IMessageHandler
    {
        void HandleMessage(string message);
    }
}
