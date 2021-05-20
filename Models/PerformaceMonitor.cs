using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJSrenovation_Project_Manager.Models
{
    public abstract class PerformaceMonitor
    {
      public  int Target { get; set; }
        public int Actual { get; set; }
    }
}
