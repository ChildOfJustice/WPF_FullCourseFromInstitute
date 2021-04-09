using System;

namespace Task_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // N people
            // M doctors
            // T - examination time
            // 
            var N = 10;
            var M = 5;
            var T = 10;
            using (var hospital = new Hospital(M, N, T, "logs/hospitalLogs.log"))
            {
                try
                {
                    hospital.StartSimulation();
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}