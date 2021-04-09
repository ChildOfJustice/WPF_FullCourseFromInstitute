using System;

namespace Task_1
{
    public class EmptyValidatorException : Exception
    {
        public EmptyValidatorException() { }

        public EmptyValidatorException(string message) : base(message) { }
    }
}