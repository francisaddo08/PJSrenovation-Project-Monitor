using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJSrenovation_Project_Manager.Models
{
    public class PaintingSurfaceLabourAnalysis
    {
        public string SurfaceType { get; set; }
        public double ExpectedHours { get; set; }
        public double ActualHours { get; set; }
        public string Painter { get; set; }
    }
}
