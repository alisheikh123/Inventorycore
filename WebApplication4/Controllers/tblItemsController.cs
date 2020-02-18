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
    public class tblItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tblItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tblItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tblItem.Include(t => t.Unit).Include(t => t.category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: tblItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblItem = await _context.tblItem
                .Include(t => t.Unit)
                .Include(t => t.category)
                .FirstOrDefaultAsync(m => m.itemId == id);
            if (tblItem == null)
            {
                return NotFound();
            }

            return View(tblItem);
        }

        // GET: tblItems/Create
        public IActionResult Create()
        {
            ViewData["UnitId"] = new SelectList(_context.Set<tblItemUnit>(), "unitId", "unitName");
            ViewData["catId"] = new SelectList(_context.tblItemcategory, "catId", "catName");
            return View();
        }

        // POST: tblItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("itemId,catId,UnitId,ItemCode,itemName,purchase_Price,sale_Price")] tblItem tblItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnitId"] = new SelectList(_context.Set<tblItemUnit>(), "unitId", "unitName", tblItem.UnitId);
            ViewData["catId"] = new SelectList(_context.tblItemcategory, "catId", "catName", tblItem.catId);
            return View(tblItem);
        }

        // GET: tblItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblItem = await _context.tblItem.FindAsync(id);
            if (tblItem == null)
            {
                return NotFound();
            }
            ViewData["UnitId"] = new SelectList(_context.Set<tblItemUnit>(), "unitId", "unitName", tblItem.UnitId);
            ViewData["catId"] = new SelectList(_context.tblItemcategory, "catId", "catName", tblItem.catId);
            return View(tblItem);
        }

        // POST: tblItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("itemId,catId,UnitId,ItemCode,itemName,purchase_Price,sale_Price")] tblItem tblItem)
        {
            if (id != tblItem.itemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblItemExists(tblItem.itemId))
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
            ViewData["UnitId"] = new SelectList(_context.Set<tblItemUnit>(), "unitId", "unitName", tblItem.UnitId);
            ViewData["catId"] = new SelectList(_context.tblItemcategory, "catId", "catName", tblItem.catId);
            return View(tblItem);
        }

        // GET: tblItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblItem = await _context.tblItem
                .Include(t => t.Unit)
                .Include(t => t.category)
                .FirstOrDefaultAsync(m => m.itemId == id);
            if (tblItem == null)
            {
                return NotFound();
            }

            return View(tblItem);
        }

        // POST: tblItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblItem = await _context.tblItem.FindAsync(id);
            _context.tblItem.Remove(tblItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblItemExists(int id)
        {
            return _context.tblItem.Any(e => e.itemId == id);
        }
    }
}
