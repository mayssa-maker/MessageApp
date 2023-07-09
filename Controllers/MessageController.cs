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
    public class MessageController : Controller
    {
        private readonly MessagerDbContext _context;

        public MessageController(MessagerDbContext context)
        {
            _context = context;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            var messagerDbContext = _context.Messages.Include(m => m.Conversation);
            return View(await messagerDbContext.ToListAsync());
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.Messages
                .Include(m => m.Conversation)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messageModel == null)
            {
                return NotFound();
            }

            return View(messageModel);
        }

        // GET: Message/Create
        public IActionResult Create()
        {
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId");
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,SenderId,ReceiverId,Content,SentAt,ConversationId")] MessageModel messageModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", messageModel.ConversationId);
            return View(messageModel);
        }

        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.Messages.FindAsync(id);
            if (messageModel == null)
            {
                return NotFound();
            }
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", messageModel.ConversationId);
            return View(messageModel);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,SenderId,ReceiverId,Content,SentAt,ConversationId")] MessageModel messageModel)
        {
            if (id != messageModel.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageModelExists(messageModel.MessageId))
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
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", messageModel.ConversationId);
            return View(messageModel);
        }

        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.Messages
                .Include(m => m.Conversation)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messageModel == null)
            {
                return NotFound();
            }

            return View(messageModel);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageModel = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(messageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageModelExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
