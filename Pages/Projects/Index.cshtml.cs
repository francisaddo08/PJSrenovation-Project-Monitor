using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager.Models;
using PJSrenovation_Project_Manager;

namespace PJSrenovation_Project_Manager.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly PJSrenovationsWebContext _context;

        public IndexModel(PJSrenovationsWebContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }

        public async Task OnGetAsync()
        {
            Project = await _context.Project
                .Include(p => p.Property).Where(p => p.ActualEndDate == null)
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .ToListAsync();
        }
    }
}
