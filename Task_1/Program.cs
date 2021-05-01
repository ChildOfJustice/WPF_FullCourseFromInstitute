using System;
using System.Collections.Generic;

namespace Task_1
{
    public class Program
    {

        static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            Builder<Car> carBuilder = new Builder<Car>();
            carBuilder
                .AddRule(car => car.hasEngine)              //0
                .AddRule(car => car.id > 10)                //1
                .AddRule(car => car.mark.Equals("Tesla"))   //2
                .AddRule(car => car.mark.Equals("Tesla2")); //3
            Validator<Car> carValidator = null;
            try
            {
                //run this without cloning and carValidator.SetNext will change the object inside of the Builder
                carValidator = carBuilder.GetValidator();
                //without cloning this will take the head pointer and point it to the second rule, which is written above: 
                carValidator.SetNext(new Validator<Car>(car => car.hasEngine, -1,new List<ValidationException<Car>>()));
                carValidator = carBuilder.GetValidator();
                
                
                
                //carValidator = carBuilder.GetValidator(); 
                
            }
            catch (EmptyValidatorException exception)
            {
                Console.WriteLine(exception.Message);
                System.Environment.Exit(1);
            }

            


            Car myCar = new Car();
            myCar.hasEngine = false;
            myCar.id = 20;
            myCar.mark = "Tesla";
            

            try
            {
                carValidator.Validate(myCar);
                Console.WriteLine("Everything looks good with the data.");
            }
            catch (AggregateException aggregatedException)
            {
                aggregatedException.Handle((exception) =>
                {
                    if (exception is ValidationException<Car> validationException)
                    {
                        Console.WriteLine("Validation failed for " + validationException.Message);
                        return true;
                    }

                    return false; // Let anything else stop the application.
                });
            }
        }
    }
}