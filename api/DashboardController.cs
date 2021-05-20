using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager.Models;
using System.Text.Json;

namespace PJSrenovation_Project_Manager.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly PJSrenovationsWebContext _context;
        public Dictionary<string, int> Ocurrance;
        public static DateTime today = DateTime.Now;
        public readonly int pastDecade = today.Year - 10;
        public readonly int pastYear = today.Year - 1;
        public readonly int pastSixMonths = today.Month - 6;

        public DashboardController(PJSrenovationsWebContext context)
        {
            this._context = context;
        }



        //==================Weekly Performance Monitor===================================
        [HttpGet("WeeklyPerformance")]
        public JsonResult WeeklyPerformance()
        {
            var weeklyMonitoring = _context.Project
                  .Include(p => p.PaintingWorks)
                  .ThenInclude(p => p.Job)
                  .OrderByDescending(P => P.StartDate)
                  .Take(1)
                  .AsNoTracking()
                  .ToList();

            List<WeeklyPerformanceMonitor> weeklyPerformances = new List<WeeklyPerformanceMonitor>();
            foreach (var item in weeklyMonitoring)
            {

                foreach (var works in item.PaintingWorks)
                {
                    weeklyPerformances.Add(new WeeklyPerformanceMonitor
                    {
                        WeekEnding = works.Job.EndDate.Value.Date.ToShortDateString(),
                        Target = works.ExpectedHours,
                        Actual = (int)works.Job.ManHours

                    });
                }
            }
            List<WeeklyPerformanceMonitor> orderedlist = weeklyPerformances.OrderBy(o => o.WeekEnding).ToList();
            var weeklyChartData = new object[weeklyPerformances.Count + 1];
            weeklyChartData[0] = new object[] { "WeekEnd", "Target", "Actual" };
            int counter = 0;
            foreach (WeeklyPerformanceMonitor w in orderedlist)
            {
                ++counter;
                weeklyChartData[counter] = new object[] {
                    w.WeekEnding, w.Target, w.Actual
                };
            }
            var data = new object[5];
            data[0] = new object[] { "WeekEnd", "Target", "Actual" };
            data[1] = new object[] { "new project", 54, 54 };
            data[2] = new object[] { "new project", 74, 74 };
            data[3] = new object[] { "new project", 104, 104 };
            data[4] = new object[] { "new project", 150, 150 };
            if (weeklyChartData.Length < 2)
            {
                return new JsonResult(data);
            }
            return new JsonResult(weeklyChartData);
        }
        // =============Painter Performances===================================
        [HttpGet("PainterPerformance")]
        public JsonResult PainterPerformance()
        {
            var result = _context.PaintingWorks
                .Include(p => p.Job).ThenInclude(p => p.Painter)
                .Where(p => p.EntryDate.Value.Year == today.Year && p.EntryDate.Value.Month >= pastSixMonths)
                .OrderByDescending(p => p.EntryDate)
                .Select(s => new {
                    painter = s.Job.Painter.FullName,
                    target = s.ExpectedHours,
                    actual = s.Job.ManHours
                })
                .AsNoTracking()
                .ToList();
            //set of painters
            SortedSet<string> painters = new SortedSet<string>();
            foreach (var item in result)
            {

                if (!painters.Contains(item.painter))
                {
                    painters.Add(item.painter);
                }

            }
            List<PainterPerformance> listPerformances = new List<PainterPerformance>();
            foreach (var p in painters)
            {
                var tempTarget = 0;
                var tempActual = 0;
                foreach (var item in result)
                {
                    if (p == item.painter.Trim())
                    {
                        tempTarget = item.target + tempTarget;
                        tempActual = (int)item.actual + tempActual;
                    }
                }
                listPerformances.Add(new PainterPerformance
                {
                    Name = p,
                    Target = tempTarget,
                    Actual = tempActual
                });
            }
            var chartData = new object[painters.Count + 1];
            chartData[0] = new object[] { "Painters", "target", "actual" };
            var i = 0;
            foreach (PainterPerformance p in listPerformances)
            {
                i++;
                chartData[i] = new object[] { p.Name, p.Target, p.Actual };
            }

            if (chartData.Length < 3)
            {
                var defaultChartData = new object[4];
                defaultChartData[0] = new object[] { "Painters", "target", "actual" };
                defaultChartData[1] = new object[] { "no data", 10, 10 };
                defaultChartData[2] = new object[] { "no data", 10, 10 };
                defaultChartData[3] = new object[] { "no data", 10, 10 };
                return new JsonResult(defaultChartData);
            }


            return new JsonResult(chartData);

        }
        //=====================Work Surface Analysis======================
        [HttpGet("WorkSurfaceAnalysis")]
        public JsonResult WorkSurfaceAnalysis()
        {
            var workresult = _context.PaintingWorks
                .Include(p => p.Job)
                .Where(p => p.EntryDate.Value.Year == today.Year)
                .Select(s => new
                {
                    surface = s.Surface.ToString(),
                    expectedhours = s.ExpectedHours

                })
                .AsNoTracking()
                .ToList();
            SortedSet<string> surfaces = new SortedSet<string>();
            List<PaintingSurface> listPaintingSurfaces = new List<PaintingSurface>();
            foreach (var item in workresult)
            {
                if (!surfaces.Contains(item.surface))
                {
                    surfaces.Add(item.surface);
                }
            }

            foreach (var s in surfaces)
            {
                double tempHours = 0;
                foreach (var surfacedata in workresult)
                {
                    if (s == surfacedata.surface.Trim())
                    {
                        tempHours = tempHours + surfacedata.expectedhours;
                    }
                }
                listPaintingSurfaces.Add(new PaintingSurface
                {
                    Name = s,
                    Hours = tempHours
                });
            }

            return new JsonResult(listPaintingSurfaces);
        }
        //**************Project Performance*************************************
        [HttpGet("ProjectPerformance")]
        public JsonResult ProjectPerformance()
        {
            var result = _context.Project
                .Include(p => p.Property)
                .Include(p => p.PaintingWorks)
                .ThenInclude(p => p.Job)
                .OrderByDescending(p => p.StartDate)
                .Select(s => new ProjectPerformance
                {
                    Address = s.Property.Address,
                    Target = s.PaintingWorks.Sum(s => s.ExpectedHours),
                    Actual = (int)s.PaintingWorks.Sum(s => s.Job.ManHours)
                })
                .AsNoTracking()
                .Take(3).ToList();
            var chartData = new object[result.Count + 1];
            chartData[0] = new object[] { "Project", "target", "actual" };
            var i = 0;
            foreach (ProjectPerformance p in result)
            {
                i++;
                chartData[i] = new object[] { p.Address, p.Target, p.Actual };
            }

            return new JsonResult(chartData);
        }


        //*********************************************************************************      
        [HttpGet("WorkAnalysis")]
        public JsonResult WorkAnalysis()
        {
            List<PaintingSurfaceLabourAnalysis> labourHours = new List<PaintingSurfaceLabourAnalysis>();
            var result = _context.PaintingWorks
                .Include(p => p.Job)
                .ThenInclude(j => j.Painter)
                .Where(p => p.EntryDate.Value.Year == today.Year)
                .Select(s => new {
                    surfaceType = s.Surface.ToString(),
                    expectedHours = s.ExpectedHours,
                    actualHours = s.Job.ManHours,
                    painter = s.Job.Painter.FullName
                })
                .AsNoTracking()
                .ToList();

            if (result.Count != 0)
            {
                foreach (var item in result)
                {
                    labourHours.Add(new PaintingSurfaceLabourAnalysis
                    {
                        SurfaceType = item.surfaceType,
                        ExpectedHours = item.expectedHours,
                        ActualHours = (double)item.actualHours,
                        Painter = item.painter

                    });

                }
            }
            return new JsonResult(labourHours);
        }


    }
}
