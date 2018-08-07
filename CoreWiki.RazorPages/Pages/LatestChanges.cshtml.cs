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
    public class LatestChangesModel : PageModel
    {
        private readonly CoreWiki.RazorPages.Models.ApplicationDbContext _context;

        public LatestChangesModel(CoreWiki.RazorPages.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Articles.OrderByDescending(a =>
            a.Published).Take(10).ToListAsync();
        }
    }
}
