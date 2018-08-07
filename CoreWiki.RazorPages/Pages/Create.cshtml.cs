﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreWiki.RazorPages.Models;

namespace CoreWiki.RazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CoreWiki.RazorPages.Models.ApplicationDbContext _context;

        public CreateModel(CoreWiki.RazorPages.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return Redirect($"/{Article.Topic}");
        }
    }
}