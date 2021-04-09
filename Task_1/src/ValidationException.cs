using System;

namespace Task_1
{
    public class ValidationException<T> : Exception
    {
        public Predicate<T> Rule { get; }

        public ValidationException() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(Predicate<T> notPassedRule) { Rule = notPassedRule; }

        public ValidationException(string message, Predicate<T> notPassedRule) : base(message) { Rule = notPassedRule; }
    }
}