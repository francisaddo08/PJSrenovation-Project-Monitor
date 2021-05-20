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
using PJSrenovation_Project_Manager.Models;
using PJSrenovation_Project_Manager;

namespace PJSrenovation_Project_Manager.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly PJSrenovationsWebContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
      

        public CreateModel(IWebHostEnvironment environment, PJSrenovationsWebContext context)
        {
            this._context = context;
            this._hostEnvironment = environment;
        }
        [BindProperty]
        public IFormFile ProjectImage { get; set; }
        [BindProperty]
        public IFormFile ProjectScope { get; set; }
        [BindProperty(SupportsGet = true)]
        public Project Project { get; set; }
        public string PropertyID { get; set; }
        public string EntryDate { get; set; }
        public IActionResult OnGet(string EntryDate,  string ProID)
        {
            var proj = _context.Project
                 .Where(p => p.PropertyID == ProID)
                 .FirstOrDefault();
            if (proj != null)
            {
            
                return RedirectToPage("./Details", new { id = proj.ProjectID });

            }
                ViewData["PropertyID"] = new SelectList(_context.Properties
                    .OrderByDescending(p => p.EntryDate), "PropertyID", "PropertyID");
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
            if (this.ProjectImage != null) { 

            var ImageName = FileUploads.GetFileName(this.ProjectImage.FileName);
            var uploadImages = Path.Combine(_hostEnvironment.WebRootPath, "ProjectImages");
            var imagePath = Path.Combine(uploadImages, ImageName);
            this.ProjectImage.CopyTo(new FileStream(imagePath, FileMode.Create));
            this.Project.ProjectImage = ImageName; // Set the file name
        }
            if(this.ProjectScope != null)
            {
                var document = FileUploads.GetFileName(this.ProjectScope.FileName);
                var documentsFolder = Path.Combine(_hostEnvironment.WebRootPath, "documents");
                var documentPath = Path.Combine(documentsFolder, document);
                this.ProjectScope.CopyTo(new FileStream(documentPath, FileMode.Create));
                this.Project.ProjectScope = document; // Set the file name
            }
            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
     
    }
}
