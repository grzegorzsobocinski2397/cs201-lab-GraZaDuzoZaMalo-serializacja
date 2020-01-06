using System;
using System.Runtime.Serialization;

namespace Serializacja.Models
{
    [Serializable]
    internal class GameEndException : Exception
    {
        public GameEndException()
        {
        }

        public GameEndException(string message) : base(message)
        {
        }

        public GameEndException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameEndException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}