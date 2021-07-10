using System;
using System.Collections.Generic;

namespace Task_1
{
    public class Builder<T>
    {
        private Validator<T> _validator;
        private int ruleNumber = 0;
        private Validator<T> _headPointerValidator;

        public Builder<T> AddRule(Predicate<T> rule)
        {
            if (_validator is null)
            {
                _validator = new Validator<T>(rule, ruleNumber,new List<ValidationException<T>>());
                ruleNumber++;
                _headPointerValidator = _validator;
            
            }
            else
            {
                _validator = _validator.SetNextRule(rule, ruleNumber);
                ruleNumber++;
            }
                

            return this;
        }

        public Validator<T> GetValidator()
        {
            Validator<T> readyValidator = (Validator<T>)_headPointerValidator?.Clone() ?? throw new EmptyValidatorException("No rules in this validator.");
            //Reset(); //if you comment this, it will return the built validator each time you will call the GetValidator() method
            return readyValidator;

            //Console.WriteLine("!!! " + readyValidator._exceptions.Count);
            //Console.WriteLine("!!! " + _headPointerValidator._exceptions.Count);
            
            //you will be able to change the object outside of the builder
            return _headPointerValidator ?? throw new EmptyValidatorException("No rules in this validator.");
        }

        public void Reset()
        {
            ruleNumber = 0;
            this._headPointerValidator = null;
            this._validator = null;

        }
    }
}