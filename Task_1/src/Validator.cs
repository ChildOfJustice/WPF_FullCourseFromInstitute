using System;
using System.Collections.Generic;

namespace Task_1
{
    public class Validator<T>: AbstractHandler, ICloneable
    {
        public Predicate<T> _rule;
        public int ruleNumber = -1;
        public List<ValidationException<T>> _exceptions;
        
        public object Clone()
        {
            var clone = (Validator<T>)this.MemberwiseClone();
            //clone._rule = _rule;
           //clone._exceptions = new List<ValidationException<T>>();
            //clone._nextHandler = _nextHandler.MemberwiseClone();
            // foreach (var exception in this._exceptions)
            // {
            //     System.Console.WriteLine("PPP");
            //     clone._exceptions.Add(exception);
            // }
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
                    //System.Console.WriteLine(_exceptions.Count);
                    this._exceptions.Add(new ValidationException<T>("Rule number " + ruleNumber, _rule));
                    //System.Console.WriteLine(_exceptions.Count);
                }


                if (base.Handle(requestData) is null)//the last rule
                {
                    if (_exceptions.Count == 0)
                    {
                       
                        return new object();
                    }

                    //Console.WriteLine(_exceptions.Count);
                    throw new AggregateException(_exceptions);
                }
                return new object();
            }
            else
            {
                return base.Handle(requestData);
            }
        }
        
        public Validator(Predicate<T> rule, int _ruleNumber, List<ValidationException<T>> exceptions) { _rule = rule; _exceptions = exceptions;
            ruleNumber = _ruleNumber;
        }
        
        internal Validator<T> SetNextRule(Predicate<T> rule, int ruleNumber)
        {
            return this.SetNext(new Validator<T>(rule, ruleNumber, this._exceptions)) as Validator<T>;
        }
    }
}