using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MessageAppBack.Data;
using MessageAppBack.Models;

namespace MessageApp.Controllers
{
    public class ConversationController : Controller
    {
        private readonly MessagerDbContext _context;

        public ConversationController(MessagerDbContext context)
        {
            _context = context;
        }

        // GET: Conversation
        public async Task<IActionResult> Index()
        {
            var messagerDbContext = _context.Conversations.Include(c => c.User1).Include(c => c.User2);
            return View(await messagerDbContext.ToListAsync());
        }

        // GET: Conversation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .Include(c => c.User1)
                .Include(c => c.User2)
                .FirstOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // GET: Conversation/Create
        public IActionResult Create()
        {
            ViewData["User1Id"] = new SelectList(_context.Users, "UserId", "Email");
            ViewData["User2Id"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Conversation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversationId,User1Id,User2Id")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User1Id"] = new SelectList(_context.Users, "UserId", "Email", conversation.User1Id);
            ViewData["User2Id"] = new SelectList(_context.Users, "UserId", "Email", conversation.User2Id);
            return View(conversation);
        }

        // GET: Conversation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            ViewData["User1Id"] = new SelectList(_context.Users, "UserId", "Email", conversation.User1Id);
            ViewData["User2Id"] = new SelectList(_context.Users, "UserId", "Email", conversation.User2Id);
            return View(conversation);
        }

        // POST: Conversation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversationId,User1Id,User2Id")] Conversation conversation)
        {
            if (id != conversation.ConversationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.ConversationId))
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
            ViewData["User1Id"] = new SelectList(_context.Users, "UserId", "Email", conversation.User1Id);
            ViewData["User2Id"] = new SelectList(_context.Users, "UserId", "Email", conversation.User2Id);
            return View(conversation);
        }

        // GET: Conversation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .Include(c => c.User1)
                .Include(c => c.User2)
                .FirstOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversation = await _context.Conversations.FindAsync(id);
            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.ConversationId == id);
        }
    }
}
