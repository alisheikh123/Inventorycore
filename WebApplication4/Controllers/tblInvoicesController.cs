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
    public class tblInvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblInvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblInvoices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tblInvoice.Include(t => t.Customer).Include(t => t.TblAccount).Include(t => t.tblCompany);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: tblInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInvoice = await _context.tblInvoice
                .Include(t => t.Customer)
                .Include(t => t.TblAccount)
                .Include(t => t.tblCompany)
                .FirstOrDefaultAsync(m => m.invoiceId == id);
            if (tblInvoice == null)
            {
                return NotFound();
            }

            return View(tblInvoice);
        }

        // GET: tblInvoices/Create
        public IActionResult Create()
        {
            ViewData["customerName"] = new SelectList(_context.Customer, "CustomerId", "Name");
            ViewData["accountId"] = new SelectList(_context.tblAccount, "accountId", "accountTitle");
            ViewData["CompanyName"] = new SelectList(_context.tblCompany, "CompanyId", "Name");
            return View();
        }

        // POST: tblInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("invoiceId,invoice_Code,Invoice_type,payment_Mode,invoice_Date,Due_Date,CompanyName,accountId,customerName,Created_Date")] tblInvoice tblInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["customerName"] = new SelectList(_context.Customer, "CustomerId", "CustomerCode", tblInvoice.customerName);
            ViewData["accountId"] = new SelectList(_context.tblAccount, "accountId", "Address", tblInvoice.accountId);
            ViewData["CompanyName"] = new SelectList(_context.tblCompany, "CompanyId", "CompanyrCode", tblInvoice.CompanyName);
            return View(tblInvoice);
        }

        // GET: tblInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInvoice = await _context.tblInvoice.FindAsync(id);
            if (tblInvoice == null)
            {
                return NotFound();
            }
            ViewData["customerName"] = new SelectList(_context.Customer, "CustomerId", "CustomerCode", tblInvoice.customerName);
            ViewData["accountId"] = new SelectList(_context.tblAccount, "accountId", "Address", tblInvoice.accountId);
            ViewData["CompanyName"] = new SelectList(_context.tblCompany, "CompanyId", "CompanyrCode", tblInvoice.CompanyName);
            return View(tblInvoice);
        }

        // POST: tblInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("invoiceId,invoice_Code,Invoice_type,payment_Mode,invoice_Date,Due_Date,CompanyName,accountId,customerName,Created_Date")] tblInvoice tblInvoice)
        {
            if (id != tblInvoice.invoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblInvoiceExists(tblInvoice.invoiceId))
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
            ViewData["customerName"] = new SelectList(_context.Customer, "CustomerId", "CustomerCode", tblInvoice.customerName);
            ViewData["accountId"] = new SelectList(_context.tblAccount, "accountId", "Address", tblInvoice.accountId);
            ViewData["CompanyName"] = new SelectList(_context.tblCompany, "CompanyId", "CompanyrCode", tblInvoice.CompanyName);
            return View(tblInvoice);
        }

        // GET: tblInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInvoice = await _context.tblInvoice
                .Include(t => t.Customer)
                .Include(t => t.TblAccount)
                .Include(t => t.tblCompany)
                .FirstOrDefaultAsync(m => m.invoiceId == id);
            if (tblInvoice == null)
            {
                return NotFound();
            }

            return View(tblInvoice);
        }

        // POST: tblInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblInvoice = await _context.tblInvoice.FindAsync(id);
            _context.tblInvoice.Remove(tblInvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblInvoiceExists(int id)
        {
            return _context.tblInvoice.Any(e => e.invoiceId == id);
        }
    }
}
