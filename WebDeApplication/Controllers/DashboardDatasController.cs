using System;
using System.Collections.Generic;
using System.Globalization;
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
            return View(await _context.DashboardData.OrderByDescending(u => u.Id).FirstOrDefaultAsync());
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
        public async Task<IActionResult> Cancel(string Year, string Month, string Day, string Name,int? page = 0)
        {
            int dayFn;
            int monthFn;
            int yearFn;
            var result = _context.EmailCancel.ToList();
            if (!String.IsNullOrEmpty(Day))
            {
                dayFn = Int32.Parse(Day);
                result = _context.EmailCancel.Where(x => x.ReceivedTimeFD.Day == dayFn).ToList();
            }
            if (!String.IsNullOrEmpty(Month))
            {
                monthFn = Int32.Parse(Month);

                result = result.Where(x => x.ReceivedTimeFD.Month == monthFn).ToList();
            }
            if (!String.IsNullOrEmpty(Year))
            {
                yearFn = Int32.Parse(Year);
                result = result.Where(x => x.ReceivedTimeFD.Year == yearFn).ToList();
            }
            if (!String.IsNullOrEmpty(Name))
            {
                result = result.Where(x => x.Name.ToLower().Contains(Name)).ToList();
            }
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

            int total = result.Count;

            ViewBag.totalData = total;

            ViewBag.numberPage = numberPage(total, limit);

            return View(result.Skip(start).Take(limit).ToList());
          
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
        public async Task<IActionResult> Delay(string Year, string Month, string Day, string Name, int? page = 0)
        {
            int dayFn;
            int monthFn;
            int yearFn;
            var result = _context.EmailDelay.Where(e => e.shipped == false).ToList();
            if (!String.IsNullOrEmpty(Day))
            {
                dayFn = Int32.Parse(Day);
                result = _context.EmailDelay.Where(x => x.estimatime.Day == dayFn).ToList();
            }
            if (!String.IsNullOrEmpty(Month))
            {
                monthFn = Int32.Parse(Month);

                result = result.Where(x => x.estimatime.Month == monthFn).ToList();
            }
            if (!String.IsNullOrEmpty(Year))
            {
                yearFn = Int32.Parse(Year);
                result = result.Where(x => x.estimatime.Year == yearFn).ToList();
            }
            if (!String.IsNullOrEmpty(Name))
            {
                result = result.Where(x => x.name.ToLower().Contains(Name)).ToList();
            }
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

            int total = result.Count;

            ViewBag.totalData = total;

            ViewBag.numberPage = numberPage(total, limit);

            return View(result.Skip(start).Take(limit).ToList());

        }
        public async Task<IActionResult> Shipped(int? id, int? page, int idOrder)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipped = _context.EmailDelay.Where(e => e.Id == id).FirstOrDefault();
            shipped.shipped = true;
            var emGroup = _context.EmailGroup.Where(e => e.Id == shipped.EmailGroupId).FirstOrDefault();
            emGroup.shipped = true;
            _context.EmailDelay.Update(shipped);
            _context.EmailGroup.Update(emGroup);

            _context.SaveChanges();
            _context.DataDauVao.ToList().ForEach(d =>
            {

                float offset = d.tyGiaBan - d.tyGiaMua;
                var totalNet = 0D;
                _context.EmailGroup.Where(e => e.ODParrent == d.Id && (e.shipped == true || e.status2 != "1")).ToList().ForEach(
                    oderItem =>
                    {
                        float a;
                        if (d.TongUSD != null)
                        {
                            var b = float.TryParse(d.TongUSD, out a);
                            if (b)
                            {
                                totalNet = totalNet + (offset * a);
                            }
                        }

                    }
                );

                var dprofit = _context.DataProfitOrder.Single(dp => dp.OrderId == d.Id);
                dprofit.NetProfit = totalNet;
                _context.DataProfitOrder.Update(dprofit);

            });
            var data = _context.DashboardData.OrderByDescending(d => d.Id).FirstOrDefault();
            data.TotalDelay = _context.EmailDelay.Where(e => e.shipped == false).Count();
            _context.SaveChanges();

            return RedirectToAction("Delay", new { page = page });


        }
    }
}
