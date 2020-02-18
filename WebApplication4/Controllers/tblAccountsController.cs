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
    public class tblAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tblAccount.Include(t => t.TblAccountHead);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: tblAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.tblAccount
                .Include(t => t.TblAccountHead)
                .FirstOrDefaultAsync(m => m.accountId == id);
            if (tblAccount == null)
            {
                return NotFound();
            }

            return View(tblAccount);
        }

        // GET: tblAccounts/Create
        public IActionResult Create()
        {
            ViewData["accountHeadId"] = new SelectList(_context.Set<tblAccountHead>(), "accountHeadId", "accountHeadName");
            return View();
        }

        // POST: tblAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("accountId,accountHeadId,accountCode,accountTitle,PhoneNo,MobileNo,Email,Address")] tblAccount tblAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["accountHeadId"] = new SelectList(_context.Set<tblAccountHead>(), "accountHeadId", "accountHeadName", tblAccount.accountHeadId);
            return View(tblAccount);
        }

        // GET: tblAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.tblAccount.FindAsync(id);
            if (tblAccount == null)
            {
                return NotFound();
            }
            ViewData["accountHeadId"] = new SelectList(_context.Set<tblAccountHead>(), "accountHeadId", "accountHeadName", tblAccount.accountHeadId);
            return View(tblAccount);
        }

        // POST: tblAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("accountId,accountHeadId,accountCode,accountTitle,PhoneNo,MobileNo,Email,Address")] tblAccount tblAccount)
        {
            if (id != tblAccount.accountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblAccountExists(tblAccount.accountId))
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
            ViewData["accountHeadId"] = new SelectList(_context.Set<tblAccountHead>(), "accountHeadId", "accountHeadName", tblAccount.accountHeadId);
            return View(tblAccount);
        }

        // GET: tblAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.tblAccount
                .Include(t => t.TblAccountHead)
                .FirstOrDefaultAsync(m => m.accountId == id);
            if (tblAccount == null)
            {
                return NotFound();
            }

            return View(tblAccount);
        }

        // POST: tblAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblAccount = await _context.tblAccount.FindAsync(id);
            _context.tblAccount.Remove(tblAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblAccountExists(int id)
        {
            return _context.tblAccount.Any(e => e.accountId == id);
        }
    }
}
