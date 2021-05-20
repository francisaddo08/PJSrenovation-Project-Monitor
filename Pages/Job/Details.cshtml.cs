using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager.Pages.Job
{
    public class DetailsModel : PageModel
    {
        private readonly PJSrenovation_Project_Manager.PJSrenovationsWebContext _context;

        public DetailsModel(PJSrenovation_Project_Manager.PJSrenovationsWebContext context)
        {
            _context = context;
        }

        public PaintingJob PaintingJob { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaintingJob = await _context.PaintingJobs
                .Include(p => p.Painter)
                .Include(p => p.Work).FirstOrDefaultAsync(m => m.PaintDecoratingJobID == id);

            if (PaintingJob == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
