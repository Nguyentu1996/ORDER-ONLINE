using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDeApplication.Models;
using WebDeApplication.Models.Data;
using WebDeApplication.Models.ViewModel.Dashboard;

namespace WebDeApplication.Controllers
{    
    [Authorize(Roles = "Admin"+","+ "User")]

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
           
            string role = User.FindFirstValue(ClaimTypes.Role);
            if("Admin".Equals(role))
            {
                ViewData["TotalDelay"] = _context.EmailDelay.Where(e => e.shipped == false).Count();
                ViewData["TotalCancel"] = _context.EmailCancel.Count();

                ViewData["TotalOrder"] = _context.EmailReader.GroupBy(e => e.ODNumber).Count();

                ViewData["TotalProfit"] = _context.DataDauVao.Sum(ord => ord.TotalProfit) + _context.SubProfitOrder.Sum(ord => ord.TotalProfit);
                ViewData["TotalNetProfit"] = _context.DataDauVao.Sum(ord => ord.NetProfit) + _context.SubProfitOrder.Sum(ord => ord.NetProfit);
            } else
            {
                ViewData["TotalDelay"] = (from e in _context.EmailDelay
                                          join data in _context.DataDauVao on e.ODParrent equals data.Id
                                          select e).Count();

                ViewData["TotalCancel"] = (from e in _context.EmailCancel
                                           join d in _context.DataDauVao on e.ODParrent equals d.Id
                                           where d.UserCreate == User.FindFirstValue(ClaimTypes.Name)
                                           select e).Count();

                ViewData["TotalOrder"] = (from e in _context.EmailReader
                                          join d in _context.DataDauVao on e.odParrent equals d.Id
                                          where d.UserCreate == User.FindFirstValue(ClaimTypes.Name)
                                          select e).Count();


                ViewData["TotalProfit"] = _context.DataDauVao.Where(e => e.UserCreate == User.FindFirstValue(ClaimTypes.Name)).Sum(ord => ord.TotalProfit) +
                                           (from sub in _context.SubProfitOrder
                                            join data in _context.DataDauVao on sub.OrderId equals data.Id
                                            where data.UserCreate == User.FindFirstValue(ClaimTypes.Name)
                                            select sub).Sum(ord => ord.TotalProfit);
                ViewData["TotalNetProfit"] = _context.DataDauVao.Where(e => e.UserCreate == User.FindFirstValue(ClaimTypes.Name)).Sum(ord => ord.NetProfit) +
                                         (from sub in _context.SubProfitOrder
                                          join data in _context.DataDauVao on sub.OrderId equals data.Id
                                          where data.UserCreate == User.FindFirstValue(ClaimTypes.Name)
                                          select sub).Sum(ord => ord.NetProfit);
            }
           

      
            return View();
        }
        public void UpdateDashBoardData()
        {

             var dataDashBoard = _context.DashboardData.OrderByDescending(u => u.Id).FirstOrDefault();
                _context.DataDauVao.Where(d => d.stopOrder == false).ToList().ForEach(d => {
                    // profit
                    var total = 0D;
                    var totalPrevios = 0D;
                    double offset = d.tyGiaBan - d.tyGiaMua;

                    float totalUsdData = 0f;
                    int payedData;
                    if (d.TongUSD != null)
                    {
                        var b = float.TryParse(d.TongUSD, out totalUsdData);
                        var e = Int32.TryParse(d.DaMua, out payedData);
                        if (b && e)
                        {
                            total = offset * totalUsdData * payedData;
                        }
                    }
                    // profit      

                    var count = _context.EmailGroup.Where(e => e.ODParrent == d.Id && (e.shipped == true || e.status2 != "1")).Count();
                    var totalNet = count * offset * totalUsdData;

                    var data = new DataProfitOrder();
                    data.ODnumber = d.ODNumber;
                    data.Name = d.Name;
                    data.NgayGui = d.NgayGui;
                    data.GiaUSD = d.GiaUSD;
                    data.DaMua = d.DaMua;
                    data.GiaSale = d.GiaSale;
                    data.SiteName = "Sephora";
                    data.TotalProfit = total;
                    data.tyGiaBan = d.tyGiaBan;
                    data.tyGiaMua = d.tyGiaMua;
                    data.DaMua = d.DaMua;
                    data.CanMua = d.CanMua;
                    data.orderStop = d.stopOrder;
                    data.OrderId = d.Id;
                    data.NetProfit = totalNet;
                    data.CreateDate = d.CreateDateFD;

                    if (_context.DataProfitOrder.Any(dt => dt.OrderId == d.Id))
                    {
                        var dprofit = _context.DataProfitOrder.Where(dp => dp.OrderId == d.Id).FirstOrDefault();
                        var subProfit = _context.SubProfitOrder.Where(sub => sub.OrderId == d.Id).OrderByDescending(sub => sub.Id).FirstOrDefault();
                        if (subProfit != null)
                        {
                            int payedInt = 0;
                            var totalUsd = 0f;

                            var payed = Int32.TryParse(d.DaMua, out payedInt);
                            var totalSuccess = float.TryParse(subProfit.TongUSD, out totalUsd);

                            if (payed && totalSuccess)
                            {
                                var payNum = payedInt - subProfit.Payed;

                                if (payNum > 0)
                                {
                                    subProfit.TotalProfit = (subProfit.tyGiaBan - subProfit.tyGiaMua) * totalUsd * payNum;
                                    var subCount = _context.EmailGroup.Where(e => e.ODParrent == d.Id && e.received > subProfit.DateUpdate && (e.shipped == true || e.status2 != "1")).Count();
                                    subProfit.NetProfit = (offset * totalUsd * subCount);
                                    subProfit.Payed = payedInt;

                                    subProfit.Id = 0;
                                    _context.SubProfitOrder.Add(subProfit);

                                }

                            }
                        }
                        else
                        {
                            dprofit.TotalProfit = data.TotalProfit;
                            dprofit.ODnumber = d.ODNumber;
                            dprofit.Name = d.Name;
                            dprofit.NgayGui = d.NgayGui;
                            dprofit.GiaUSD = d.GiaUSD;
                            dprofit.DaMua = d.DaMua;
                            dprofit.GiaSale = d.GiaSale;
                            dprofit.TotalProfit = total;
                            dprofit.tyGiaBan = d.tyGiaBan;
                            dprofit.tyGiaMua = d.tyGiaMua;
                            dprofit.DaMua = d.DaMua;
                            dprofit.CanMua = d.CanMua;
                            dprofit.orderStop = d.stopOrder;
                            data.NetProfit = totalNet;
                            _context.DataProfitOrder.Update(dprofit);
                        }

                    }
                    else
                    {
                        _context.DataProfitOrder.Add(data);
                    }

                });

                dataDashBoard.TotalProfit = _context.DataProfitOrder.Sum(ord => ord.TotalProfit) + _context.SubProfitOrder.Sum(ord => ord.TotalProfit);
                dataDashBoard.TotalNetProfit = _context.DataProfitOrder.Sum(ord => ord.NetProfit) + _context.SubProfitOrder.Sum(ord => ord.NetProfit);


                if (_context.DashboardData.Count() > 0)
                {
                    var data = _context.DashboardData.OrderByDescending(d => d.Id).FirstOrDefault();
                    data.Month = dataDashBoard.Month;
                    data.Year = dataDashBoard.Year;
                    data.PercentCancel = dataDashBoard.PercentCancel;
                    data.PercentDelay = dataDashBoard.PercentDelay;
                    data.PercentOrder = dataDashBoard.PercentOrder;
                    data.PercentProfit = dataDashBoard.PercentProfit;
                    data.TotalCancel = dataDashBoard.TotalCancel;
                    data.TotalDelay = dataDashBoard.TotalDelay;
                    data.TotalOrder = dataDashBoard.TotalOrder;
                    data.TotalProfit = dataDashBoard.TotalProfit;
                    data.TotalNetProfit = dataDashBoard.TotalProfit;
                    data.SiteName = dataDashBoard.SiteName;
                    _context.DashboardData.Update(data);
                }
                else
                {
                    _context.DashboardData.Add(dataDashBoard);
                }
                _context.SaveChanges();
           
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
        public async  Task<IActionResult> Profit( string from, string to, int? page = 0)
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

            int total = 0;
            string role = User.FindFirstValue(ClaimTypes.Role);

            if ("Admin".Equals(role))
            {
                 total = await _context.DataDauVao.CountAsync();
            } else
            {
                total = await _context.DataDauVao.Where(e => e.UserCreate == User.FindFirstValue(ClaimTypes.Name)).CountAsync(); 
            }
            ViewBag.totalData = total;
           
            ViewBag.numberPage = numberPage(total, limit);
   
            if (from != null && to != null)
            {
             
                var dateStr = from.Replace("-", "/");
                DateTime df = DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
            
                var dateToStr = to.Replace("-", "/");
                DateTime dto = DateTime.Parse(dateToStr, CultureInfo.InvariantCulture);
                if ("Admin".Equals(role))
                {
                    var result = _context.DataDauVao.Select(e => new DataOrderViewModel
                    {
                        CanMua = e.CanMua,
                        DaMua = e.DaMua,
                        Name = e.Name,
                        stopOrder = e.stopOrder,
                        Adress = e.Adress,
                        CreateDateFD = e.CreateDateFD,
                        NgayGui = e.NgayGui,
                        Id = e.Id,
                        GiaSale = e.GiaSale,
                        GiaUSD = e.GiaUSD,
                        ODNumber = e.ODNumber,
                        tyGiaBan = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaBan : e.tyGiaBan,
                        tyGiaMua = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaMua : e.tyGiaMua,
                        TongUSD = e.TongUSD,
                        NetProfit =  _context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.NetProfit) + e.NetProfit : e.NetProfit,
                                    
                        TotalProfit = _context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.TotalProfit) + e.TotalProfit : e.TotalProfit
                    }).Where(e => e.CreateDateFD >= df && e.CreateDateFD <= dto).Skip(start).Take(limit).ToList();


                    total = result.Count;

                    ViewBag.totalData = total;

                    ViewBag.numberPage = numberPage(total, limit);
                    return View(result);

                }
                else
                {
                    var result = _context.DataDauVao.Where(e => e.UserCreate == User.FindFirstValue(ClaimTypes.Name)).Select(e => new DataOrderViewModel
                    {
                        CanMua = e.CanMua,
                        DaMua = e.DaMua,
                        Name = e.Name,
                        stopOrder = e.stopOrder,
                        Adress = e.Adress,
                        CreateDateFD = e.CreateDateFD,
                        NgayGui = e.NgayGui,
                        Id = e.Id,
                        GiaSale = e.GiaSale,
                        GiaUSD = e.GiaUSD,
                        ODNumber = e.ODNumber,
                        tyGiaBan = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaBan : e.tyGiaBan,
                        tyGiaMua = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaMua : e.tyGiaMua,
                        TongUSD = e.TongUSD,
                        NetProfit = _context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.NetProfit) + e.NetProfit : e.NetProfit,

                        TotalProfit = _context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.TotalProfit) + e.TotalProfit : e.TotalProfit
                    }).Where(e => e.CreateDateFD >= df && e.CreateDateFD <= dto).Skip(start).Take(limit).ToList();


                    total = result.Count;

                    ViewBag.totalData = total;

                    ViewBag.numberPage = numberPage(total, limit);
                    return View(result);

                }
            }

            if ("Admin".Equals(role))
            {
                var result1 = _context.DataDauVao.Select(e => new DataOrderViewModel
                {
                    CanMua = e.CanMua,
                    DaMua = e.DaMua,
                    Name = e.Name,
                    stopOrder = e.stopOrder,
                    Adress = e.Adress,
                    CreateDateFD = e.CreateDateFD,
                    NgayGui = e.NgayGui,
                    Id = e.Id,
                    GiaSale = e.GiaSale,
                    GiaUSD = e.GiaUSD,
                    ODNumber = e.ODNumber,
                    tyGiaBan = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaBan : e.tyGiaBan,
                    tyGiaMua = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaMua : e.tyGiaMua,
                    TongUSD = e.TongUSD,
                    NetProfit = e.NetProfit + (_context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.NetProfit) : 0),
                    TotalProfit = e.TotalProfit + (_context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.TotalProfit) : 0),
                }).Skip(start).Take(limit).ToList();
                return View(result1);

            }
            else
            {
                var result1 = _context.DataDauVao.Where(e => e.UserCreate == User.FindFirstValue(ClaimTypes.Name)).Select(e => new DataOrderViewModel
                {
                    CanMua = e.CanMua,
                    DaMua = e.DaMua,
                    Name = e.Name,
                    stopOrder = e.stopOrder,
                    Adress = e.Adress,
                    CreateDateFD = e.CreateDateFD,
                    NgayGui = e.NgayGui,
                    Id = e.Id,
                    GiaSale = e.GiaSale,
                    GiaUSD = e.GiaUSD,
                    tyGiaBan = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaBan : e.tyGiaBan,
                    tyGiaMua = _context.SubProfitOrder.Any(dp => dp.OrderId == e.Id) ? _context.SubProfitOrder.Where(dp => dp.OrderId == e.Id).OrderByDescending(sub => sub.Id).FirstOrDefault().tyGiaMua : e.tyGiaMua,
                    TongUSD = e.TongUSD,
                    NetProfit = e.NetProfit + (_context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.NetProfit) : 0),
                    TotalProfit = e.TotalProfit + (_context.SubProfitOrder.Any(sub => sub.OrderId == e.Id) ? _context.SubProfitOrder.Where(sub => sub.OrderId == e.Id).Sum(ord => ord.TotalProfit) : 0),
                }).Skip(start).Take(limit).ToList();
                return View(result1);

            }

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
            string role = User.FindFirstValue(ClaimTypes.Role);
            var result = new List<EmailCancel>();
            if ("Admin".Equals(role))
            {
                result =  _context.EmailCancel.ToList();
            }
            else
            {
                result = (from e in _context.EmailCancel
                    join d in _context.DataDauVao on e.ODParrent equals d.Id
                    where d.UserCreate == User.FindFirstValue(ClaimTypes.Name)
                    select e).ToList();
            }
               
            //_context.EmailCancel.ToList();
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
            var result = new List<EmailDelay>();
            string role = User.FindFirstValue(ClaimTypes.Role);
            if ("Admin".Equals(role))
            {

                result = _context.EmailDelay.ToList();
            } else
            {
                 result =
                  (from e in _context.EmailDelay
                   join data in _context.DataDauVao on e.ODParrent equals data.Id
                   select e).ToList();
            }
              
            //_context.EmailDelay.Where(e => e.shipped == false).ToList();
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
         
            var emReader = _context.EmailReader.Where(e => e.Id == id).FirstOrDefault();

            if (emReader != null)
            {
                emReader.shipped = true;
                _context.EmailReader.Update(emReader);

            }
            _context.EmailDelay.Update(shipped);

            _context.SaveChanges();
            if (emReader != null && emReader.shipped == true) return RedirectToAction("Delay", new { page = page });

            var order = _context.DataDauVao.Where(d => d.Id == idOrder && d.stopOrder == false).FirstOrDefault();
            if (order != null)
            {
           
                order.NetProfit = order.NetProfit + ((order.tyGiaBan - order.tyGiaMua) * float.Parse(order.TongUSD));
         
                _context.SaveChanges();

            }

            return RedirectToAction("Delay", new { page = page });


        }
    }
}
