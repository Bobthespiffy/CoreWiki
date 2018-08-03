using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreWiki.RazorPages.Models;

namespace CoreWiki.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CoreWiki.RazorPages.Models.ApplicationDbContext _context;

        public IndexModel(CoreWiki.RazorPages.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Articles.ToListAsync();
        }
    }
}
