using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager.Pages.Job
{
    public class CreateModel : PageModel
    {
        private readonly PJSrenovation_Project_Manager.PJSrenovationsWebContext _context;

        public CreateModel(PJSrenovation_Project_Manager.PJSrenovationsWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SelfEmployedPainterID"] = new SelectList(_context.Painters, "PainterID", "PainterID");
        ViewData["WorkID"] = new SelectList(_context.PaintingWorks, "WorkID", "WorkID");
            return Page();
        }

        [BindProperty]
        public PaintingJob PaintingJob { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PaintingJobs.Add(PaintingJob);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
