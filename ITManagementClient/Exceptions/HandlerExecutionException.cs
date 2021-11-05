using System;

namespace ITManagementClient.Exceptions
{
    public class HandlerExecutionException : Exception
    {
        public HandlerExecutionException() { }

        public HandlerExecutionException(string message) : base(message) { }
    }
}