using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_Systems.Models;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    public class tblAccountHeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblAccountHeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblAccountHeads
        public async Task<IActionResult> Index()
        {
            return View(await _context.tblAccountHead.ToListAsync());
        }

        // GET: tblAccountHeads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccountHead = await _context.tblAccountHead
                .FirstOrDefaultAsync(m => m.accountHeadId == id);
            if (tblAccountHead == null)
            {
                return NotFound();
            }

            return View(tblAccountHead);
        }

        // GET: tblAccountHeads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tblAccountHeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("accountHeadId,accountHeadName,account_Head_Code")] tblAccountHead tblAccountHead)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblAccountHead);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblAccountHead);
        }

        // GET: tblAccountHeads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccountHead = await _context.tblAccountHead.FindAsync(id);
            if (tblAccountHead == null)
            {
                return NotFound();
            }
            return View(tblAccountHead);
        }

        // POST: tblAccountHeads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("accountHeadId,accountHeadName,account_Head_Code")] tblAccountHead tblAccountHead)
        {
            if (id != tblAccountHead.accountHeadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblAccountHead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblAccountHeadExists(tblAccountHead.accountHeadId))
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
            return View(tblAccountHead);
        }

        // GET: tblAccountHeads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccountHead = await _context.tblAccountHead
                .FirstOrDefaultAsync(m => m.accountHeadId == id);
            if (tblAccountHead == null)
            {
                return NotFound();
            }

            return View(tblAccountHead);
        }

        // POST: tblAccountHeads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblAccountHead = await _context.tblAccountHead.FindAsync(id);
            _context.tblAccountHead.Remove(tblAccountHead);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblAccountHeadExists(int id)
        {
            return _context.tblAccountHead.Any(e => e.accountHeadId == id);
        }
    }
}
