using System;

namespace Core.Application.Exceptions
{
    public class ItemNotFoundException: Exception
    {
        public ItemNotFoundException(string message) : base(message)
        {
        }
    }
}