using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;


namespace PJSrenovation_Project_Manager.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly PJSrenovationsWebContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel( IWebHostEnvironment environment,  PJSrenovationsWebContext context)
        {
           this. _context = context;
            this._hostEnvironment = environment;
        }

        [BindProperty(SupportsGet =true)]
        public Project Project { get; set; }
        [BindProperty]
        public IFormFile ProjectImage { get; set; }
        [BindProperty(SupportsGet = true)]
        public IFormFile ProjectScope { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Project
                .Include(p => p.Property).FirstOrDefaultAsync(m => m.ProjectID == id);

            if (Project == null)
            {
                return NotFound();
            }
          
            
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "PropertyID");
            ViewData["StartDate"] = this.Project.StartDate;
            ViewData["EndDate"] = this.Project.EndDate;
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
            if (this.ProjectImage != null)
            {

                var ImageName = FileUploads.GetFileName(this.ProjectImage.FileName);
                var uploadImages = Path.Combine(_hostEnvironment.WebRootPath, "ProjectImages");
                var imagePath = Path.Combine(uploadImages, ImageName);
                this.ProjectImage.CopyTo(new FileStream(imagePath, FileMode.Create));
                this.Project.ProjectImage = ImageName; // Set the file name
            }
            if (this.ProjectScope != null)
            {
                var document = FileUploads.GetFileName(this.ProjectScope.FileName);
                var documentsFolder = Path.Combine(_hostEnvironment.WebRootPath, "documents");
                var documentPath = Path.Combine(documentsFolder, document);
                this.ProjectScope.CopyTo(new FileStream(documentPath, FileMode.Create));
                this.Project.ProjectScope = document; // Set the file name
            }

            _context.Attach(Project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.ProjectID))
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

        private bool ProjectExists(string id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }
        
    }
}
