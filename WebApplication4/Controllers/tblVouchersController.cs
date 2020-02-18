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
    public class tblVouchersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblVouchersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblVouchers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tblVoucher.Include(t => t.TblInvoice);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: tblVouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVoucher = await _context.tblVoucher
                .Include(t => t.TblInvoice)
                .FirstOrDefaultAsync(m => m.voucherId == id);
            if (tblVoucher == null)
            {
                return NotFound();
            }

            return View(tblVoucher);
        }

        // GET: tblVouchers/Create
        public IActionResult Create()
        {
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName");
            return View();
        }

        // POST: tblVouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("voucherId,voucherCode,voucherDate,invoiceId,userId,createdDate,voucher_Type")] tblVoucher tblVoucher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblVoucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName", tblVoucher.invoiceId);
            return View(tblVoucher);
        }

        // GET: tblVouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVoucher = await _context.tblVoucher.FindAsync(id);
            if (tblVoucher == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName", tblVoucher.invoiceId);
            return View(tblVoucher);
        }

        // POST: tblVouchers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("voucherId,voucherCode,voucherDate,invoiceId,userId,createdDate,voucher_Type")] tblVoucher tblVoucher)
        {
            if (id != tblVoucher.voucherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblVoucher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblVoucherExists(tblVoucher.voucherId))
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
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName", tblVoucher.invoiceId);
            return View(tblVoucher);
        }

        // GET: tblVouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVoucher = await _context.tblVoucher
                .Include(t => t.TblInvoice)
                .FirstOrDefaultAsync(m => m.voucherId == id);
            if (tblVoucher == null)
            {
                return NotFound();
            }

            return View(tblVoucher);
        }

        // POST: tblVouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblVoucher = await _context.tblVoucher.FindAsync(id);
            _context.tblVoucher.Remove(tblVoucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblVoucherExists(int id)
        {
            return _context.tblVoucher.Any(e => e.voucherId == id);
        }
    }
}
