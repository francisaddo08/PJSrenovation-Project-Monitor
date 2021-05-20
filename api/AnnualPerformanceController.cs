using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJSrenovation_Project_Manager.Models;


namespace PJSrenovation_Project_Manager.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnualPerformanceController : ControllerBase
    {
        private readonly PJSrenovationsWebContext _context;
        public Dictionary<string, int> Ocurrance;
        public static DateTime today = DateTime.Now;
        readonly int pastDecade = today.Year - 10;
        public AnnualPerformanceController(PJSrenovationsWebContext contest)
        {
            this._context = contest;
        }
    
     [HttpGet("WallFinishTrends")]
      public JsonResult WallFinishTrends()
        {
            Ocurrance = new Dictionary<string, int>();
            var finishData = _context.PaintingJobs
                .Select(s => s.Finish.ToString())
                .AsNoTracking()
                 .ToList();
            if(finishData.Count != 0)
            {
                foreach(var finishtype in finishData)
                {
                    string temp = finishtype;
                    int finishCount = finishData.Where(c => c.Equals(temp)).Count();
                    if (!Ocurrance.ContainsKey(temp))
                    {
                        Ocurrance.Add(temp, finishCount);
                    }
                }
            }
            List<FinishTrends> finishTrends = new List<FinishTrends>();
            foreach(var item in Ocurrance)
            {
                finishTrends.Add(new FinishTrends { 
                Finish = item.Key,
                Count = item.Value
                });
            }
            return new JsonResult(finishTrends);
        }
        
        [HttpGet("ColourTrend")]
        public JsonResult ColourTrend()
        {
            Ocurrance = new Dictionary<string, int>();


            var colourData = _context.PaintingJobs
                .Where(p => p.EndDate != null && p.EndDate.Value.Year >= pastDecade)
                .Select(x => x.WallColourName.Trim() + " " + x.WallColourValue.Trim())
                .AsNoTracking().ToList();

            if (colourData.Count != 0)
            {
                foreach (var item in colourData)
                {
                    string temp = item;
                    int colourCount = colourData.Where(c => c.Equals(temp)).Count();
                    if (!Ocurrance.ContainsKey(temp) )
                    {
                        Ocurrance.Add(temp, colourCount);
                    }
                }
            }
            List<ColourTrend> colourTrends = new List<ColourTrend>();
            foreach (var item in Ocurrance)
            {
                string[] k = item.Key.Split(" ");
                colourTrends.Add(new ColourTrend
                {
                    ColourName = k[0],
                    ColourValue = k[1],
                    Count = item.Value
                });

            }

           
            return new JsonResult(colourTrends);
        }
        [HttpGet("ProjectAnalysis")]
        public JsonResult ProjectAnalysis()
        {
            var projectResult = _context
                .Properties.Include(p => p.Project)
                .ThenInclude(p => p.PaintingWorks)
                .ThenInclude(p => p.Job)
                .Select(s => new
                {
                    Address = s.Address,
                    Size = s.NumberOfRooms,
                    ExpectedHours = s.Project.PaintingWorks.Sum(p => p.ExpectedHours),
                    ActualHours = s.Project.PaintingWorks.Sum(p => (int)p.Job.ManHours)


                })
              
              
                .AsNoTracking()
                  .ToList();

            return new JsonResult(projectResult);
        }

        [HttpGet("ProjectLabourAnalysis")]
        public JsonResult ProjectLabourAnalysis()
        {
            List<LabourHours> labourHours = new List<LabourHours>();
            var result = _context.Properties.Include(p => p.Project)
                .ThenInclude(p => p.PaintingWorks)
                .ThenInclude(p => p.Job)
                .Where(p => p.EntryDate.Value.Year >= this.pastDecade)
                .AsNoTracking()
                .ToList();

            if (result.Count != 0)
            {
                foreach (var item in result)
                {
                    labourHours.Add(new LabourHours
                    {
                        Address = item.Address,
                        Size = (int)item.NumberOfRooms,
                        ExpectedHours = item.Project.PaintingWorks.Sum(p => p.ExpectedHours),
                        ActualHours = item.Project.PaintingWorks.Sum(p => (int)p.Job.ManHours)
                    });

                }
            }
            return new JsonResult("project analysis");
        }
        [HttpGet("PaintingAnnualPerformance")]
        public JsonResult PainterQuartelyPerformance()
        {
            var result = _context.PaintingWorks
                .Include(p => p.Job)
                .Where(p => p.EntryDate.Value.Year >= pastDecade)
                .OrderBy(p => p.EntryDate)
                .Select(s => new {
                    year = s.EntryDate.Value.Year,
                    target = s.ExpectedHours,
                    actual = s.Job.ManHours
                })
                .AsNoTracking().ToList();
            //set of years
            SortedSet<int> years = new SortedSet<int>();
            foreach (var item in result)
            {

                if (!years.Contains(item.year))
                {
                    years.Add(item.year);
                }

            }
            List<AnnualPerformancePaintng> listPerformances = new List<AnnualPerformancePaintng>();
            foreach (var y in years)
            {
                var tempTarget = 0;
                var tempActual = 0;
                foreach (var item in result)
                {
                    if (y == item.year)
                    {
                        tempTarget = item.target + tempTarget;
                        tempActual = (int)item.actual + tempActual;
                    }
                }
                listPerformances.Add(new AnnualPerformancePaintng
                {
                    Year = y,
                    Target = tempTarget,
                    Actual = tempActual
                });
            }
            var chartData = new object[years.Count + 1];
            chartData[0] = new object[] { "year", "target", "actual" };
            var i = 0;
            foreach (AnnualPerformancePaintng p in listPerformances)
            {
                i++;
                chartData[i] = new object[] { p.Year.ToString(), p.Target, p.Actual };
            }



            return new JsonResult(chartData);
        }
    }
}
