using System;

namespace Task_2
{
    public class Patient
    {
        public bool IsInfected { get; set; }
        public Patient(int InfectionChance)
        {
            if (new Random().Next(100) <= InfectionChance) IsInfected = true;
        }
    }
}