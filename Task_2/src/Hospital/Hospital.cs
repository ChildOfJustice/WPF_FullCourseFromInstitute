// Инфекционное отделение поликлиники имеет смотровую на 𝑁 человек и 𝑀 докторов, которые готовы оказать помощь
// заболевшим или проконсультировать не заболевших людей. По правилам инфекционного отделения в смотровую может зайти
// любой человек, если в ней есть свободное место или в нее может зайти не заболевший человек, если в смотровой нет
// заболевших и наоборот: при наличии свободных мест заболевший человек может войти в смотровую, если там только
// заболевшие. Как только человек вошел в смотровую он занимает свое место и ожидает доктора. Незанятый доктор выбирает
// пациента, пришедшего раньше других, и проводит прием, который длиться от 1 до T временных единиц. В особых случаях,
// доктор может попросить у другого доктора помощи, которая так же может длиться от 1 до T временных единиц.
// Если все места в смотровой заняты, то пришедшие пациенты встают в очередь. При этом в очереди спустя некоторое
// время, при наличии нездорового человека, заражается вся очередь. Количество пациентов и интервал их появления
// произволен и случаен. Напишите программу моделирующую работу инфекционного отделения с использованием средств
// синхронизации потоков .net framework. Ваша программа должна вести всю историю работы инфекционного отделения:
// пришедшие пациенты и их состояние, время работы докторов и время оказания помощи пациентам.  Обязательно сохранять
// информацию о новых заболевших в очереди. Продемонстрируйте работу вашей программы при различных значениях
// параметров. Подберите параметры так, чтоб показать особые случаи, которые могут возникнуть в инфекционном отделении.
// Срок сдачи до 30 мая. 

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Task_2
{
    public class Hospital : IDisposable
    {
        public delegate void LoggerHandler(string text);

        private readonly ConcurrentDictionary<int, Doctor> _doctors;
        private readonly ConcurrentQueue<Patient> _examinationRoom;
        private readonly ConcurrentDictionary<int, Patient> _queue;
        private readonly Random rnd = new Random();
        
        public ConcurrentQueue<String> loggerQueue;

        #region HospitalParams

        public int StatusTimeout { get; } = 500;
        public int QueueTimeout { get; } = 700;
        public int InfectionChance { get; } = 40;
        public int MaxDelayBeforePatient { get; } = 1100;
        public int DoctorConsultationChance { get; } = 10;
        public int ExaminationRoomSize { get; } = 10;
        public int ExaminationTime { get; }
        private StreamWriter LogFile { get; }
        private int DoctorsQuantity { get; }
        public bool IsDayOver { get; private set; }
        public bool HasNewPatient => _examinationRoom.Count != 0;
        public bool HasInfectionInRoom => _examinationRoom.Any(x => x.IsInfected);

        #endregion
        
        public Hospital(int doctorsQuantity, int examinationRoomSize, int examinationTime, string logFile = "")
        {
            ExaminationRoomSize = examinationRoomSize;
            ExaminationTime = examinationTime;
            LogFile = File.CreateText(logFile);
            DoctorsQuantity = doctorsQuantity;
            IsDayOver = false;

            _doctors = new ConcurrentDictionary<int, Doctor>();
            for (var i = 0; i < doctorsQuantity; i++) _doctors.GetOrAdd(i, new Doctor(this, i));

            _queue = new ConcurrentDictionary<int, Patient>();
            _examinationRoom = new ConcurrentQueue<Patient>();
            loggerQueue = new ConcurrentQueue<string>();
            
        }
        
        public void StartSimulation()
        {
            StartNewPatientDemon();
            // Thread.Sleep(10);
            StartStatusDemonAsync();
            StartQueueDemonAsync();
            StartInfectionDemonAsync();
            StartLoggingDemonAsync();

            foreach (var doctor in _doctors) doctor.Value.WorkAsync();
        }

        #region Deamons
        private async void StartNewPatientDemon()
        {
            await Task.Run(NewPatientDemon);
        }
        private void NewPatientDemon()
        {
            while (!IsDayOver)
            {
                var newId = _queue.Count == 0 ? 0 : _queue.Keys.Max() + 1;
                var p = new Patient(InfectionChance);
                _queue[newId] = p;
                loggerQueue.Enqueue($"New patient IsInfected: {p.IsInfected}\n");
                // LoggerEvent?.Invoke();
                Thread.Sleep(rnd.Next(MaxDelayBeforePatient));
            }
        }
        
        
        
        
        private async void StartLoggingDemonAsync()
        {
            await Task.Run(NewLoggingDemon);
        }
        private void NewLoggingDemon()
        {
            while (!IsDayOver)
            {
                // Peek at the first element.
                string result;
                if (!loggerQueue.TryPeek(out result))
                {
                    //Console.WriteLine("CQ: TryPeek failed when it should have succeeded");
                    //waiting for the text in queue
                }

                string localValue;
                while (loggerQueue.TryDequeue(out localValue))
                {
                    LogFile.Write(localValue);
                    LogFile.Flush();
                }
            }
        }
        
        

        
        private async void StartStatusDemonAsync()
        {
            await Task.Run(() =>
            {
                while (!IsDayOver)
                {
                    Thread.Sleep(StatusTimeout);
                    PrintHospitalState();
                }
            });
        }
        private void PrintHospitalState()
        {
            Console.Clear();
            Console.WriteLine($"Patients in the examination room {_examinationRoom.Count} in queue {_queue.Count} " +
                              $"Total patients {_queue.Count + _examinationRoom.Count}");
            Console.WriteLine("Examination room");
            foreach (var patient in _examinationRoom)
                if (patient.IsInfected)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("|-_-|");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.Write("|^_^|");
                }

            Console.WriteLine();
            Console.WriteLine("Queue");
            foreach (var patient in _queue)
                if (patient.Value.IsInfected)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("|-_-|");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.Write("|^_^|");
                }
        }
        
        
        
        private async void StartQueueDemonAsync()
        {
            await Task.Run(QueueDemon);
        }
        private void QueueDemon()
        {
            while (!IsDayOver)
            {
                Thread.Sleep(ExaminationTime);
                if (_examinationRoom.Count >= ExaminationRoomSize || _queue.Count == 0) continue;

                int queueId;
                try
                {
                    if (!HasNewPatient)
                        queueId = _queue.Keys.Min();
                    else if (HasInfectionInRoom)
                        queueId = _queue.Where(pair => pair.Value.IsInfected).Select(pair => pair.Key).Min();
                    else
                        queueId = _queue.Where(pair => !pair.Value.IsInfected).Select(pair => pair.Key).Min();
                }
                catch (InvalidOperationException)
                {
                    continue;
                }

                Patient p;
                while (!_queue.TryRemove(queueId, out p))
                {
                }
                
                _examinationRoom.Enqueue(p);
                
            }
        }

        
        
        
        private async void StartInfectionDemonAsync()
        {
            await Task.Run(InfectionDemon);
        }
        private void InfectionDemon()
        {
            while (!IsDayOver)
            {
                Thread.Sleep(StatusTimeout);
                // var previous = new Patient();
                // previous.IsInfected = false;
                var keys = _queue.Keys.ToArray();
                for (var i = 0; i < keys.Length; i++)
                    try
                    {
                        if (!_queue[keys[i]].IsInfected) continue;
                        if (i > 0)
                        {
                            _queue[keys[i - 1]].IsInfected = true;
                            loggerQueue.Enqueue($"Infecting in queue patient {keys[i - 1]}\n");
                        }

                        if (i < keys.Length - 1)
                        {
                            _queue[keys[i + 1]].IsInfected = true;
                            loggerQueue.Enqueue($"Infecting in queue patient {keys[i + 1]}\n");
                        }
                        i++;
                    }
                    catch (KeyNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
            }
        }
        
        #endregion

        #region DoctorFunctions

        

       
        public Patient BeginInspection()
        {
            while (true)
            {
                if (_examinationRoom.Count == 0) continue;
                try
                {
                    if (!_examinationRoom.TryDequeue(out var p)) continue;
                    //loggerQueue.Enqueue("took the patient from examination room \n");
                    return p;
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void EndInspection(string text)
        {
            loggerQueue.Enqueue(text);
        }
        public int GetDoctorForConsulting()
        {
            while (true)
            {
                Thread.Sleep(10);
                foreach (var doctor in _doctors)
                {
                    // if (rnd.Next(100) >= 30) continue;
                    // return doctor.Key;
                    if (!doctor.Value.IsWorking)
                    {
                        //doctor.Value.IsWorking = true;
                        //loggerQueue.Enqueue("doctor " + doctor.Key + " is now consulting");
                        return doctor.Key;
                    }
                }
            }
        }
        public void FreeDoctor(int docNum)
        {
            _doctors[docNum].IsWorking = false;
        }
        
        #endregion
        
        public void Dispose()
        {
            IsDayOver = true;
            
            string localValue;
            while (loggerQueue.TryDequeue(out localValue))
            {
                LogFile.Write(localValue);
                LogFile.Flush();
            }
            LogFile.Close();
        }
    }
}