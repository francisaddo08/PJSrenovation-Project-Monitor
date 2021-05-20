using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly PJSrenovationsWebContext _context;

        public DetailsModel(PJSrenovationsWebContext context)
        {
            _context = context;
        }
      //  public string ImageName { get; set; }
        public Project Project { get; set; }
        public List<PaintingWork> PaintingWork { get; set; }

        public Dictionary<string, string> ProjectImages = new Dictionary<string, string>();


        public List<string> ProjectColours = new List<string>();
        public List<string> WorkImages = new List<string>();
        public List<string> JobImages = new List<string>();
         public  IDictionary<string, string> JobColour =
            new Dictionary<string, string>();


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Project
                .Include(P => P.PaintingWorks).ThenInclude(p => p.Job)
                .ThenInclude(p => p.Painter)
                .Include(p => p.Property).ThenInclude(p => p.Client)
                .FirstOrDefaultAsync(m => m.ProjectID == id);

           
         
            if (Project == null)
            {
                return NotFound();
            }
           
            return Page();
        }
    }
}
