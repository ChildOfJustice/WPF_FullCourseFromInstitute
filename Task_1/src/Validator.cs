using System;
using System.Collections.Generic;

namespace Task_1
{
    public class Validator<T>: AbstractHandler, ICloneable
    {
        Predicate<T> _rule;
        private int ruleNumber;
        List<ValidationException<T>> _exceptions;
        
        public object Clone()
        {
            var clone = (Validator<T>)this.MemberwiseClone();
            clone._rule = new Predicate<T>(this._rule);
            clone._exceptions = new List<ValidationException<T>>();
            foreach (var exception in this._exceptions)
            {
                clone._exceptions.Add(exception);
            }
            return clone;
        }
        
        
        
        
        
        
        
        
        public void Validate(object requestData)
        {
            Handle(requestData);
        }
        
        public override object Handle(object requestData)
        {
            if (requestData is T requestDataT)
            {
                Console.WriteLine("Trying rule: " + _rule.Invoke(requestDataT));
                
                if (!_rule.Invoke(requestDataT))
                {
                    _exceptions.Add(new ValidationException<T>("Rule number " + ruleNumber, _rule));
                }


                if (base.Handle(requestData) is null)//the last rule
                {
                    if (_exceptions.Capacity == 0)
                    {
                        return new object();
                    }

                    throw new AggregateException(_exceptions);
                }
                return new object();
            }
            else
            {
                return base.Handle(requestData);
            }
        }
        
        public Validator(Predicate<T> rule, List<ValidationException<T>> exceptions) { _rule = rule; _exceptions = exceptions; }
        
        internal Validator<T> SetNextRule(Predicate<T> rule, int ruleNumber)
        {
            this.ruleNumber = ruleNumber;
            return this.SetNext(new Validator<T>(rule, this._exceptions)) as Validator<T>;
        }
    }
}