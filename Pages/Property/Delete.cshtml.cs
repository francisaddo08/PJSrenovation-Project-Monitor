using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager.Pages.Property
{
    public class DeleteModel : PageModel
    {
        private readonly PJSrenovation_Project_Manager.PJSrenovationsWebContext _context;

        public DeleteModel(PJSrenovation_Project_Manager.PJSrenovationsWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PJSrenovation_Project_Manager.Models.Property Property { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Property = await _context.Properties
                .Include(c => c.Client).FirstOrDefaultAsync(m => m.PropertyID == id);

            if (Property == null)
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

            Property = await _context.Properties.FindAsync(id);

            if (Property != null)
            {
                _context.Properties.Remove(Property);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
