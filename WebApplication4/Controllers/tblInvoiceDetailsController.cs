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
    public class tblInvoiceDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblInvoiceDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblInvoiceDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tblInvoiceDetail.Include(t => t.TblInvoice).Include(t => t.TblItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: tblInvoiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInvoiceDetail = await _context.tblInvoiceDetail
                .Include(t => t.TblInvoice)
                .Include(t => t.TblItem)
                .FirstOrDefaultAsync(m => m.invoiceDetailId == id);
            if (tblInvoiceDetail == null)
            {
                return NotFound();
            }

            return View(tblInvoiceDetail);
        }

        // GET: tblInvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName");
            ViewData["itemId"] = new SelectList(_context.tblItem, "itemId", "ItemCode");
            return View();
        }

        // POST: tblInvoiceDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("invoiceDetailId,invoiceId,itemId,price,Quantity,amount")] tblInvoiceDetail tblInvoiceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInvoiceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName", tblInvoiceDetail.invoiceId);
            ViewData["itemId"] = new SelectList(_context.tblItem, "itemId", "ItemCode", tblInvoiceDetail.itemId);
            return View(tblInvoiceDetail);
        }

        // GET: tblInvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInvoiceDetail = await _context.tblInvoiceDetail.FindAsync(id);
            if (tblInvoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName", tblInvoiceDetail.invoiceId);
            ViewData["itemId"] = new SelectList(_context.tblItem, "itemId", "ItemCode", tblInvoiceDetail.itemId);
            return View(tblInvoiceDetail);
        }

        // POST: tblInvoiceDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("invoiceDetailId,invoiceId,itemId,price,Quantity,amount")] tblInvoiceDetail tblInvoiceDetail)
        {
            if (id != tblInvoiceDetail.invoiceDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInvoiceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblInvoiceDetailExists(tblInvoiceDetail.invoiceDetailId))
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
            ViewData["invoiceId"] = new SelectList(_context.tblInvoice, "invoiceId", "CompanyName", tblInvoiceDetail.invoiceId);
            ViewData["itemId"] = new SelectList(_context.tblItem, "itemId", "ItemCode", tblInvoiceDetail.itemId);
            return View(tblInvoiceDetail);
        }

        // GET: tblInvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInvoiceDetail = await _context.tblInvoiceDetail
                .Include(t => t.TblInvoice)
                .Include(t => t.TblItem)
                .FirstOrDefaultAsync(m => m.invoiceDetailId == id);
            if (tblInvoiceDetail == null)
            {
                return NotFound();
            }

            return View(tblInvoiceDetail);
        }

        // POST: tblInvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblInvoiceDetail = await _context.tblInvoiceDetail.FindAsync(id);
            _context.tblInvoiceDetail.Remove(tblInvoiceDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblInvoiceDetailExists(int id)
        {
            return _context.tblInvoiceDetail.Any(e => e.invoiceDetailId == id);
        }
    }
}
