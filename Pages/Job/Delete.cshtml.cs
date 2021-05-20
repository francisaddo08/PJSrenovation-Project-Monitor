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
    public class DeleteModel : PageModel
    {
        private readonly PJSrenovation_Project_Manager.PJSrenovationsWebContext _context;

        public DeleteModel(PJSrenovation_Project_Manager.PJSrenovationsWebContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaintingJob = await _context.PaintingJobs.FindAsync(id);

            if (PaintingJob != null)
            {
                _context.PaintingJobs.Remove(PaintingJob);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
