using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;

namespace CoreWiki.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var article = await _context.Articles.SingleOrDefaultAsync(m => m.Topic.Equals("Home Page", StringComparison.CurrentCultureIgnoreCase));
            if (article == null)
                return NotFound();

            return View("Details", article);
            //return View(await _context.Articles.ToListAsync());
        }

        // GET: Articles/5
        [Route("{topicName}")]
        public async Task<IActionResult> Index(string topicName)
        {
            topicName = topicName ?? "Home Page";

            if (topicName == null)
                return NotFound();

            var article = await _context.Articles
                .SingleOrDefaultAsync(m => m.Topic == topicName);
            if (article == null)
                return NotFound();

            return View("Details", article);
        }

        // GET: Articles/Details/5
        [Route("Details")]
        public async Task<IActionResult> Details(string topicName)
        {

            // TODO: If topicName not specified, default to Home Page

            topicName = topicName ?? "Home Page";

            if (topicName == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .SingleOrDefaultAsync(m => m.Topic == topicName);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles

        // GET: Articles/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Topic,Published,Content")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.SingleOrDefaultAsync(m => m.Topic == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{article}")]
        public async Task<IActionResult> Edit(string id, [Bind("Topic,Published,Content")] Article article)
        {
            if (id != article.Topic)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Topic))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .SingleOrDefaultAsync(m => m.Topic == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(m => m.Topic == id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(string id)
        {
            return _context.Articles.Any(e => e.Topic == id);
        }
    }
}
