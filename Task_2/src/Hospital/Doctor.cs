using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_2
{
    public class Doctor
    {
        private readonly Hospital _hospital;
        private readonly int _number;
        private readonly Random rnd;
        public bool IsWorking { get; set; }

        public Doctor(Hospital hospital, int number)
        {
            _hospital = hospital;
            _number = number;
            rnd = new Random(_number);
        }

        public async void WorkAsync()
        {
            await Task.Run(Work);
        }

        private void Work()
        {
            while (!_hospital.IsDayOver)
            {
                if (!_hospital.HasNewPatient || IsWorking)
                {
                    Thread.Sleep(10);
                    continue;
                }
                

                IsWorking = true;
                _hospital.loggerQueue.Enqueue($"Doctor {_number} started an inspection\n");
                var patient = _hospital.BeginInspection();
                var t = DateTime.Now;
                Inspect();
                Consult();
                var time = DateTime.Now - t;
                IsWorking = false;
                _hospital.EndInspection($"Doctor {_number} has ended inspection. Time spent: {time.TotalMilliseconds}ms\n");
            }
        }

        private void Inspect()
        {
            Thread.Sleep(rnd.Next(1, _hospital.ExaminationTime + 1) * _hospital.StatusTimeout);
        }

        private void Consult()
        {
            
            if (rnd.Next(100) >= _hospital.DoctorConsultationChance) return;
            _hospital.loggerQueue.Enqueue($"Doctor {_number} is trying to find a doctor for consulting\n");
            var docNum = _hospital.GetDoctorForConsulting();
            _hospital.loggerQueue.Enqueue($"Doctor {_number} is consulting with doctor {docNum}\n");
            Thread.Sleep(rnd.Next(1, _hospital.ExaminationTime + 1) * _hospital.StatusTimeout);
            _hospital.FreeDoctor(docNum);
        }
    }
}