using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager.Pages.Job
{
    public class EditModel : PageModel
    {
        private readonly PJSrenovation_Project_Manager.PJSrenovationsWebContext _context;

        public EditModel(PJSrenovation_Project_Manager.PJSrenovationsWebContext context)
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
           ViewData["SelfEmployedPainterID"] = new SelectList(_context.Painters, "PainterID", "PainterID");
           ViewData["WorkID"] = new SelectList(_context.PaintingWorks, "WorkID", "WorkID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PaintingJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaintingJobExists(PaintingJob.PaintDecoratingJobID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PaintingJobExists(int id)
        {
            return _context.PaintingJobs.Any(e => e.PaintDecoratingJobID == id);
        }
    }
}
