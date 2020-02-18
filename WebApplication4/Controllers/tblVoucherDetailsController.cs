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
    public class tblVoucherDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblVoucherDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblVoucherDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tblVoucherDetail.Include(t => t.TblVoucher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: tblVoucherDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVoucherDetail = await _context.tblVoucherDetail
                .Include(t => t.TblVoucher)
                .FirstOrDefaultAsync(m => m.voucherdetailId == id);
            if (tblVoucherDetail == null)
            {
                return NotFound();
            }

            return View(tblVoucherDetail);
        }

        // GET: tblVoucherDetails/Create
        public IActionResult Create()
        {
            ViewData["voucherId"] = new SelectList(_context.tblVoucher, "voucherId", "voucherCode");
            return View();
        }

        // POST: tblVoucherDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("voucherdetailId,voucherId,Narration,debitAmount,creditAmount")] tblVoucherDetail tblVoucherDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblVoucherDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["voucherId"] = new SelectList(_context.tblVoucher, "voucherId", "voucherCode", tblVoucherDetail.voucherId);
            return View(tblVoucherDetail);
        }

        // GET: tblVoucherDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVoucherDetail = await _context.tblVoucherDetail.FindAsync(id);
            if (tblVoucherDetail == null)
            {
                return NotFound();
            }
            ViewData["voucherId"] = new SelectList(_context.tblVoucher, "voucherId", "voucherCode", tblVoucherDetail.voucherId);
            return View(tblVoucherDetail);
        }

        // POST: tblVoucherDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("voucherdetailId,voucherId,Narration,debitAmount,creditAmount")] tblVoucherDetail tblVoucherDetail)
        {
            if (id != tblVoucherDetail.voucherdetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblVoucherDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblVoucherDetailExists(tblVoucherDetail.voucherdetailId))
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
            ViewData["voucherId"] = new SelectList(_context.tblVoucher, "voucherId", "voucherCode", tblVoucherDetail.voucherId);
            return View(tblVoucherDetail);
        }

        // GET: tblVoucherDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVoucherDetail = await _context.tblVoucherDetail
                .Include(t => t.TblVoucher)
                .FirstOrDefaultAsync(m => m.voucherdetailId == id);
            if (tblVoucherDetail == null)
            {
                return NotFound();
            }

            return View(tblVoucherDetail);
        }

        // POST: tblVoucherDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblVoucherDetail = await _context.tblVoucherDetail.FindAsync(id);
            _context.tblVoucherDetail.Remove(tblVoucherDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblVoucherDetailExists(int id)
        {
            return _context.tblVoucherDetail.Any(e => e.voucherdetailId == id);
        }
    }
}
