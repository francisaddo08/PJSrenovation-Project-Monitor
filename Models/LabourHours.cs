using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJSrenovation_Project_Manager.Models
{
    public class LabourHours
    {
        public string Address { get; set; }
        public int Size { get; set; }
        public int ExpectedHours { get; set; }
        public int ActualHours { get; set; }

    }
}
