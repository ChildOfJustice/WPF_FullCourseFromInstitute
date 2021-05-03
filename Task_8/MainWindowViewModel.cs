using System.Collections.Concurrent;
using System.Collections.Generic;
using Task_3.ViewModels;
using Task_8.AsyncCypher;

namespace Task_8
{
    public class MainWindowViewModel : BaseViewModel
    {
        public LinkedList<TaskManager> TasksList = new LinkedList<TaskManager>();
        
    }
}