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
    public class DetailsModel : PageModel
    {
        private readonly CoreWiki.RazorPages.Models.ApplicationDbContext _context;

        public DetailsModel(CoreWiki.RazorPages.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string topicName)
        {
            topicName = topicName ?? "HomePage";

            if (topicName == null)
            {
                return NotFound();
            }

            Article = await _context.Articles.SingleOrDefaultAsync(m => m.Topic == topicName);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
