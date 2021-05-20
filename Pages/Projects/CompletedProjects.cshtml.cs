using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PJSrenovation_Project_Manager.Models;
using PJSrenovation_Project_Manager;

namespace PJSrenovation_Project_Manager.Pages.Projects
{
    public class CompletedProjectsModel : PageModel
    {
        private readonly PJSrenovationsWebContext _context;
        public CompletedProjectsModel( PJSrenovationsWebContext context)
        {
            this._context = context;
        }
        public IList<Project> Project { get; set; }
        public async Task OnGet()
        {
            Project = await _context.Project.Include(p => p.Property)
                .Where(p => p.ActualEndDate != null)
                .OrderByDescending(p => p.ActualEndDate)
                .Take(10)
                .ToListAsync();
        }
    }
}
