using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager.Pages.Painters
{
    public class IndexModel : PageModel
    {
        private readonly PJSrenovation_Project_Manager.PJSrenovationsWebContext _context;

        public IndexModel(PJSrenovation_Project_Manager.PJSrenovationsWebContext context)
        {
            _context = context;
        }

        public IList<Painter> Painter { get;set; }

        public async Task OnGetAsync()
        {
            Painter = await _context.Painters.ToListAsync();
        }
    }
}
