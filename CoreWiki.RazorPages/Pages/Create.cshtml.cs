using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoreWiki.RazorPages.Models;
using NodaTime;

namespace CoreWiki.RazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CoreWiki.RazorPages.Models.ApplicationDbContext _context;
        private readonly IClock _clock;

        public CreateModel(CoreWiki.RazorPages.Models.ApplicationDbContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Article.Published = _clock.GetCurrentInstant();

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return Redirect($"/{Article.Topic}");
        }
    }
}