using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDeApplication.Models;
using WebDeApplication.Models.Data;

namespace WebDeApplication.Controllers
{    
    [Authorize(Roles = "Admin")]

    public class DashboardDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DashboardDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.DashboardData.ToListAsync());
        }

        // GET: DashboardDatas/Details/5
        public async Task<IActionResult> Details(int? page = 0)
        {
            //Pagination
            int limit = 50;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;

            ViewBag.pageCurrent = page;

            //int total = totalData(joinning);
            int total = await _context.EmailDelay.CountAsync();

            ViewBag.totalData = total;

            ViewBag.numberPage = numberPage(total, limit);

            return View(await _context.EmailDelay.Skip(start).Take(limit).ToListAsync());
        }

        // GET: DashboardDatas/Create
        public async  Task<IActionResult> Profit(int? page = 0)
        {
            //Pagination
            int limit = 50;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;

            ViewBag.pageCurrent = page;

            //int total = totalData(joinning);
            int total = await _context.DataProfitOrder.CountAsync();

            ViewBag.totalData = total;
           
            ViewBag.numberPage = numberPage(total, limit);
        
            return View(await _context.DataProfitOrder.Skip(start).Take(limit).ToListAsync());
        }
        public int numberPage(float totalData, int limit)
        {
            float numberpage = totalData / limit;
            return (int)Math.Ceiling(numberpage);
        }

        // POST: DashboardDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Month,TotalProfit,PercentProfit,TotalOrder,PercentOrder,TotalCancel,PercentCancel,TotalDelay,PercentDelay,SiteName")] DashboardData dashboardData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dashboardData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dashboardData);
        }

        // GET: DashboardDatas/Edit/5
        public async Task<IActionResult> Cancel(int? page = 0)
        {

            //Pagination
            int limit = 50;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;

            ViewBag.pageCurrent = page;

            int total = await _context.EmailCancel.CountAsync();

            ViewBag.totalData = total;

            ViewBag.numberPage = numberPage(total, limit);

            return View(await _context.EmailCancel.Skip(start).Take(limit).ToListAsync());
          
        }

        // POST: DashboardDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Month,TotalProfit,PercentProfit,TotalOrder,PercentOrder,TotalCancel,PercentCancel,TotalDelay,PercentDelay,SiteName")] DashboardData dashboardData)
        {
            if (id != dashboardData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dashboardData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashboardDataExists(dashboardData.Id))
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
            return View(dashboardData);
        }

        // GET: DashboardDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboardData = await _context.DashboardData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dashboardData == null)
            {
                return NotFound();
            }

            return View(dashboardData);
        }

        // POST: DashboardDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dashboardData = await _context.DashboardData.FindAsync(id);
            _context.DashboardData.Remove(dashboardData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DashboardDataExists(int id)
        {
            return _context.DashboardData.Any(e => e.Id == id);
        }
    }
}
