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
    public class tblItemUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblItemUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblItemUnits
        public async Task<IActionResult> Index()
        {
            return View(await _context.tblItemUnit.ToListAsync());
        }

        // GET: tblItemUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblItemUnit = await _context.tblItemUnit
                .FirstOrDefaultAsync(m => m.unitId == id);
            if (tblItemUnit == null)
            {
                return NotFound();
            }

            return View(tblItemUnit);
        }

        // GET: tblItemUnits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tblItemUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("unitId,unitName")] tblItemUnit tblItemUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblItemUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblItemUnit);
        }

        // GET: tblItemUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblItemUnit = await _context.tblItemUnit.FindAsync(id);
            if (tblItemUnit == null)
            {
                return NotFound();
            }
            return View(tblItemUnit);
        }

        // POST: tblItemUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("unitId,unitName")] tblItemUnit tblItemUnit)
        {
            if (id != tblItemUnit.unitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblItemUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblItemUnitExists(tblItemUnit.unitId))
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
            return View(tblItemUnit);
        }

        // GET: tblItemUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblItemUnit = await _context.tblItemUnit
                .FirstOrDefaultAsync(m => m.unitId == id);
            if (tblItemUnit == null)
            {
                return NotFound();
            }

            return View(tblItemUnit);
        }

        // POST: tblItemUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblItemUnit = await _context.tblItemUnit.FindAsync(id);
            _context.tblItemUnit.Remove(tblItemUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblItemUnitExists(int id)
        {
            return _context.tblItemUnit.Any(e => e.unitId == id);
        }
    }
}
