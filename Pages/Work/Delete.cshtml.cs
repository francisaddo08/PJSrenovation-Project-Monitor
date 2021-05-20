using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager.Pages.Work
{
    public class DeleteModel : PageModel
    {
        private readonly PJSrenovation_Project_Manager.PJSrenovationsWebContext _context;

        public DeleteModel(PJSrenovation_Project_Manager.PJSrenovationsWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaintingWork PaintingWork { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaintingWork = await _context.PaintingWorks
                .Include(p => p.Project).FirstOrDefaultAsync(m => m.WorkID == id);

            if (PaintingWork == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaintingWork = await _context.PaintingWorks.FindAsync(id);

            if (PaintingWork != null)
            {
                _context.PaintingWorks.Remove(PaintingWork);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
