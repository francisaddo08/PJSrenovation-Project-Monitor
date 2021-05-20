using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager.Models;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace PJSrenovation_Project_Manager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
    private readonly PJSrenovationsWebContext _context;

    public IndexModel(ILogger<IndexModel> logger, PJSrenovationsWebContext context)
    {
        _logger = logger;
        _context = context;
    }
    public Dictionary<string, int> colourOcurance = new Dictionary<string, int>();
    public Dictionary<string, int> colourNameDictionary = new Dictionary<string, int>();
    public Dictionary<string, int> SortedcolourOcurance = new Dictionary<string, int>();
    public List<string> colourValue = new List<string>();
    public List<string> colourlist = new List<string>();
    public string ColourNumberJson { get; set; }
    public string ColourNameNumberJson { get; set; }
    public string TopColourJson { get; set; }
    public DateTime today = DateTime.Now;


    public IList<Project> Project { get; set; }
    public List<PaintingJob> PaintingJobs { get; set; }
    //List<PieChartVM> pieChartVMs { get; set; }

    public async Task OnGetAsync()
    {
        Project = await _context.Project
            .Include(p => p.Property)
            .Include(p => p.PaintingWorks)
            .ThenInclude(p => p.Job)
            .Where(p => p.ActualEndDate != null && p.ActualEndDate.Value.Year == today.Year)
            .OrderByDescending(p => p.StartDate)
            .Take(10)
            .ToListAsync();

        foreach (var item in Project)
        {
            foreach (var work in item.PaintingWorks)
            {
                string c = work.Job.WallColourValue;
                c = c.Trim();
                string[] colour = c.Split(' ');
                foreach (var cd in colour)
                {
                    string cu = cd.ToUpper();
                    if (cu.StartsWith("#"))
                    {

                        colourlist.Add(cd);
                    }

                }
            }
        }
        if (colourlist.Count != 0)
        {
            foreach (var item in colourlist)
            {
                string temp = item;
                int count = colourlist.Where(c => c.Equals(temp)).Count();
                if (!colourOcurance.ContainsKey(temp) && count > 2)
                {
                    colourOcurance.Add(temp, count);
                }
                ColourNumberJson = JsonSerializer.Serialize(colourOcurance);
            }

        }

    }


}
}
