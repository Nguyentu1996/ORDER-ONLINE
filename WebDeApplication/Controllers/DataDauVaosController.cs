using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using WebDeApplication.Models;
using WebDeApplication.Models.Data;
using WebDeApplication.Models.Dto;
using WebDeApplication.Models.ViewModel;

namespace WebDeApplication.Controllers
{
    //[Authorize]
    [Authorize(Roles = "User")]
    //[Authorize(Roles = "Admin")]

    public class DataDauVaosController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApplicationDbContext _context;
        //private List<DataDauVao> data = new List<DataDauVao>();
        private List<EmailReaderViewModel> data = new List<EmailReaderViewModel>();

        private readonly string zohoMailUrlOauthV2 = "https://accounts.zoho.com/oauth/v2/token";
        private readonly string auth2 = "https://accounts.zoho.com/oauth/v2/auth";
        private readonly string scope = "ZohoMail.messages.READ";
        private readonly string client_id = "1000.16VT5D5DQ71NQWFD3LNR6DDUD0F8XS";
        private readonly string redirect_uri = "https://localhost:44309/Datadauvaos/Callback";
        private readonly string zohoMailUrlGetMailFolder = "https://mail.zoho.com/api/accounts/6001458000000008002/messages/view?folderId=6001458000000962001";
        private readonly string zohoMailUrlSearch = "https://mail.zoho.com/api/accounts/6001458000000008002/messages/search";
        private readonly string zohomailMaskasRead = "https://mail.zoho.com/api/accounts/6001458000000008002/updatemessage";
        private string refreshtoken;
        private Boolean isErrors = false;
        public DataDauVaosController(ApplicationDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;

        }
        public bool IsDateTime(string text)
        {
            DateTime dateTime;
            bool isDateTime = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDateTime = DateTime.TryParse(text, out dateTime);

            return isDateTime;
        }
        [HttpGet]
        public async Task<IActionResult> TotalItem(string from, string to, string name, int? page = 0)
        {
            var result = new List<TotalItem>();
            if(from != null && to != null)
            {
                var dateStr = from.Replace("-", "/");
                DateTime dt = DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
                TimeSpan epoch = (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                var unixTimestamp = (double)epoch.TotalMilliseconds;
                var dateToStr = to.Replace("-", "/");
                DateTime dto = DateTime.Parse(dateToStr, CultureInfo.InvariantCulture);
                TimeSpan epochTo = (dto - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                var unixTimestampTo = (double)epochTo.TotalMilliseconds;


                //var groupStatusEmail = (from e in _context.EmailReader
                //                   where e.ODNumber != null
                //                        && e.receivedTimeLong >= unixTimestamp && e.receivedTimeLong <= unixTimestampTo
                //                   group e by e.ODNumber into g
                //                   select new { ODNumber = g.Key, status = g.Where(e => e != null).Max(e => e.status2) }).ToList();
                var groupSelect = _context.EmailReader.Where(e => e.ODNumber != null && e.receivedTimeLong >= unixTimestamp && e.receivedTimeLong <= unixTimestampTo)
                                                       .GroupBy(e => e.ODNumber,
                                                               (key, g) => new { ODNumber = key, status = g.Where(e => e != null).Max(e => e.status2) }).ToList();
                var group1 = (from e in _context.EmailReader
                              join g in groupSelect on e.ODNumber equals g.ODNumber
                              where e.status2 != null && (e.status2 == g.status || e.status2 == "0")
                              select new EmailReader
                              {
                                  Id = e.Id,
                                  ODNumber = e.ODNumber,
                                  address = e.address,
                                  name = e.name == null ? _context.EmailReader.FirstOrDefault(em => em.ODNumber == e.ODNumber && e.status2 == "2").name : e.name,
                                  shippto = e.shippto,
                                  status = e.status,
                                  summary = e.summary,
                                  subject = e.subject,
                                  fromAddress = e.fromAddress,
                                  toAddress = e.toAddress,
                                  tracking = e.tracking,
                                  orderTotal = e.orderTotal,
                                  sentDateInGMT = e.sentDateInGMT,
                                  priority = e.receivedTime,
                                  status2 = e.status2,
                                  receivedTimeLong = e.receivedTimeLong,
                                  receivedTime = e.receivedTime != null ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(e.receivedTime)).ToLocalTime().ToString("dd-MM-yyyy hh:mm:ss tt") : String.Empty
                              }).Where(e => e.priority != null).OrderByDescending(e => e.priority).ToList();


                result = (from gr in group1
                          join i in _context.Item
                          on gr.ODNumber equals i.ODnumber
                          where gr.status2 == i.ImageUrl
                          orderby i.receiveiTime

                          group i by new
                          {
                              i.Address,
                              i.Name
                          } into item
                          select new TotalItem
                          {
                              Name = item.Key.Name,
                              Address = item.Key.Address,
                              Total = item.Sum(c => c.Quantity),
                              ItemCd = (from g in item select g.ItemCd).First()

                          }).ToList();

            } else
            {
                var groupStatus = (from e in _context.EmailReader
                                   where e.ODNumber != null
                                   group e by e.ODNumber into g
                                   select new { ODNumber = g.Key, status = g.Where(e => e != null).Max(e => e.status2) }).ToList();

                var emails = (from e in _context.EmailReader
                              join g in groupStatus on e.ODNumber equals g.ODNumber
                              where e.status2 != null && (e.status2 == g.status || e.status2 == "0")
                              select new EmailReader
                              {
                                  Id = e.Id,
                                  ODNumber = e.ODNumber,
                                  address = e.address,
                                  name = e.name == null ? _context.EmailReader.FirstOrDefault(em => em.ODNumber == e.ODNumber && e.status2 == "2").name : e.name,
                                  shippto = e.shippto,
                                  status = e.status,
                                  summary = e.summary,
                                  subject = e.subject,
                                  fromAddress = e.fromAddress,
                                  toAddress = e.toAddress,
                                  tracking = e.tracking,
                                  orderTotal = e.orderTotal,
                                  sentDateInGMT = e.sentDateInGMT,
                                  priority = e.receivedTime,
                                  receivedTimeLong = e.receivedTimeLong,
                                  status2 = e.status2,
                                  receivedTime = e.receivedTime != null ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(e.receivedTime)).ToLocalTime().ToString("dd-MM-yyyy hh:mm:ss tt") : String.Empty
                              }).Where(e => e.priority != null).OrderByDescending(e => e.priority).ToList();

                result = (from gr in emails
                          join i in _context.Item
                          on gr.ODNumber equals i.ODnumber
                          where gr.status2 == i.ImageUrl
                          orderby i.receiveiTime

                          group i by new
                          {
                              i.Address,
                              i.Name
                          } into item
                          select new TotalItem
                          {
                              Name = item.Key.Name,
                              Address = item.Key.Address,
                              Total = item.Sum(c => c.Quantity),
                              ItemCd = (from g in item select g.ItemCd).First()


                          }).ToList();
            }


            if (name != null)
            {
                name = name.Trim();
                result = result.Where(item => item.Name.Contains(name)).ToList();

            }

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
            // End
            var data = paginationItemData(start, limit, result);
            ViewData["TotalItem"] = data;
            return View();
        }

         public async Task<IActionResult> EmailReaderDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IList<Item> ListItem = new List<Item>();

            var data = await _context.EmailGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            //data.receivedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(data.receivedTime)).ToLocalTime().ToString("dd-MM-yyyy hh:mm:ss tt");
            ListItem =_context.Item.Where(item => item.MessageId == data.MessageId && item.ImageUrl == data.status2).Select(e => new Item {
                    Name = e.Name,
                    Price = _context.Item.Where(i => i.ODnumber == e.ODnumber && i.Name.Contains(e.Name) && i.Price != null).Select(it => it.Price).FirstOrDefault(),
                    Quantity = e.Quantity
                 }).ToList();
            ViewData["ItemList"] = ListItem;
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        public async Task<IActionResult> Shipped(int? id, int? page, int idOrder)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emGroup = _context.EmailGroup.Where(e => e.Id == id).FirstOrDefault();
            emGroup.shipped = true;

            var shipped = _context.EmailDelay.Where(e => e.Id == id).FirstOrDefault();
            if(shipped != null)
            {
                shipped.shipped = true;
                _context.EmailDelay.Update(shipped);
            }
            _context.EmailGroup.Update(emGroup);
            _context.SaveChanges();
            //_context.DataDauVao.Where(d => d.stopOrder == false).ToList().ForEach(d => {
              
            //    float offset = d.tyGiaBan - d.tyGiaMua;     
            //    var totalNet = 0D;
            //    _context.EmailGroup.Where(e => e.ODParrent == d.Id && (e.shipped == true || e.status2 != "1")).ToList().ForEach(
            //        oderItem =>
            //        {
            //            float a;
            //            if (oderItem.orderTotal != null)
            //            {
            //                var b = float.TryParse(oderItem.orderTotal.Replace("$", ""), out a);
            //                if (b)
            //                {
            //                    totalNet = totalNet + (offset * a);
            //                }
            //            }

            //        }
            //    );

            //        var dprofit = _context.DataProfitOrder.Single(dp => dp.ODnumber == d.ODNumber);
            //        dprofit.NetProfit = totalNet;
            //        _context.DataProfitOrder.Update(dprofit);
            //});

            return RedirectToAction("Details", new { id = idOrder, page = page });
        }
    
        public async Task<IActionResult> ReadEmail(string Day, string Month, string Year, int? page = 0)
        {
            int dayFn;
            int monthFn;
            int yearFn;

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
            var result = _context.EmailGroup.Select(e=> e).ToList();

            ViewBag.pageCurrent = page;

            int total = result.Count;

            ViewBag.totalData = total;

            ViewBag.numberPage = numberPage(total, limit);
            // End
         
            return View(result.Skip(start).Take(limit));
        }
        public IActionResult Callback()
        {
            return View();
        }
        public async Task<IActionResult> ProcessEmail(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                return RedirectToAction(nameof(ReadEmail));
                
            }
            OauthZohoResponse received = new OauthZohoResponse();
            //await Auth2Token();

            received = await getAccessToken(code, received);
            var emails = await GetAllEmail(received);

            IEnumerable<Task<EmailContent>> downloadTasksQuery =
                 //from email in emails 
                 //where !_context.EmailReader.Any(e => e.messageId == email.messageId)
                 //select ProcessUrlAsync(email, received);
            emails.Where(m => !_context.EmailReader.Any(e => e.messageId == m.messageId)).Select(e => ProcessUrlAsync(e, received));
            List<Task<EmailContent>> downloadTasks = downloadTasksQuery.ToList();
            while (downloadTasks.Any())
            {
                Task<EmailContent> finishedTask = await Task.WhenAny(downloadTasks);
                downloadTasks.Remove(finishedTask);
                if( finishedTask != null)
                {
                    var email = _context.EmailReader.Find(finishedTask.Result.data.messageId);
                    if (email != null)
                    {
                        if (finishedTask.Result.data.content == null) continue;
                        var tempData = finishedTask.Result.data.content;

                        var tempMail = tempData.Split("address");
                        if (tempMail.Length > 1)
                        {
                            var tempMail1 = tempMail[1].Split("</span>");
                            var tempMail2 = tempMail1[0].Replace("=\r\n", "");
                            var tempMail3 = tempMail2.Replace("\">", "");
                            email.address = tempMail3;
                        }
                        else
                        {
                            tempMail = tempData.Split("tact Us");
                            if (tempMail.Length < 2)
                            {
                                tempMail = tempData.Split("tac=\r\nt Us");
                                if (tempMail.Length < 2)
                                {
                                    tempMail = tempData.Split("tact=\r\n Us");

                                }
                            }
                            if (tempMail.Length < 2) continue;
                            var temp = tempMail[1].Split("All rights reserve");
                            if (temp.Length < 1)
                            {
                                continue;
                            }
                            var temp1 = Regex.Split(temp[0], "<a .?>(.*?)</a>");
                            if (temp1.Length < 1)
                            {
                                continue;
                            }
                            var shippto6 = temp1[0].Replace("=\r\n", "");
                            var shippto3 = shippto6.Split("color:#CCCCCC;\">");
                            if (shippto3.Length < 2)
                            {
                                //var shippto3 = temp1[0].Split("color:#CCCCCC;\"=\r\n>");
                                continue;
                            }
                            var shippto4 = shippto3[1].Split("</a>");
                            if (shippto4.Length < 1)
                            {
                                continue;
                            }
                            var shippto5 = shippto4[0].Replace("=\r\n", "");
                            email.address = shippto5;
                        }
                        // linkTrack

                        if (email.status.Contains("confirm"))
                        {
                            var shippto = finishedTask.Result.data.content.Split("<b>SHIP TO</b><b>:</b>");
                            if (shippto.Length < 2)
                            {
                                continue;
                            }
                            var shippto2 = shippto[1].Split("<br />\r\n\t");
                            if (shippto2.Length < 1)
                            {
                                continue;
                            }
                            var shippto3 = shippto2[0].Replace("=\r\n", "");
                            if (shippto3 == null)
                            {
                                continue;
                            }
                            var shippto4 = shippto3.Split("<br />");
                            if (shippto4.Length < 1)
                            {
                                continue;
                            }
                            email.name = shippto4[0].Replace("<br>", "").Trim();
                            if (email.name.Contains("<span"))
                            {
                                email.name = email.name.Split(">")[1].Trim();
                            }
                            if (shippto.Length > 0)
                            {
                                for (int index = 1; index < shippto.Length; index++)
                                {
                                    email.shippto += shippto[index];

                                }
                            }
                            email.shippto = shippto4[1] + shippto4[2];
                            // Order Total
                            var orderTotal = finishedTask.Result.data.content.Replace("=\r\n", "");
                            var estimateTime = orderTotal.Split("Estimated Delivery Date:</b>");
                            if (estimateTime.Length > 1)
                            {
                                var estimateTime1 = estimateTime[1].Split("\r\n</span></td>\r\n</tr>\r\n");
                                var estimateTime2 = estimateTime1[0].Trim();
                                try
                                {
                                    DateTime dt = DateTime.Parse(estimateTime2, CultureInfo.InvariantCulture);
                                    email.estimateDilivery = dt;

                                    //TimeSpan epoch = (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                                    //var unixTimestamp = (double)epoch.TotalMilliseconds;
                                    //email.estimateDilivery = unixTimestamp.ToString();
                                }
                                catch (Exception e)
                                {
                                    //no estimateDilivery
                                }
                            }
                            var oderTotal1 = orderTotal.Split("Gift Card:");
                            if (oderTotal1.Length < 2) continue;
                            var oderTotal2 = oderTotal1[1].Split("<br /> </td>");
                            if (oderTotal2.Length < 2) continue;
                            var oderTotal3 = oderTotal2[0].Replace("=2E", ".");
                            email.orderTotal = oderTotal3;
                            // item
                            var item = orderTotal.Split("<td align=3D\"center\" width=3D\"200\"><a"); // include link
                            if (item.Length < 1) continue;
                            for (int index = 1; index < item.Length; index++)
                            {
                                var itemOD = new Item();
                                itemOD.MessageId = email.messageId;
                                itemOD.ODnumber = email.ODNumber;
                                itemOD.ImageUrl = email.status2;
                                itemOD.receiveiTime = email.receivedTime;
                                itemOD.Address = email.address;
                                var odList = item[index].Split("target=3D\"_blank\">");
                                if (odList.Length < 1) continue;
                                string[] stringArray = new string[odList.Length - 2];

                                for (int odIndex = 2; odIndex < odList.Length; odIndex++)
                                {
                                    var odItem = odList[odIndex].Split("</a></span></td>");
                                    stringArray[odIndex - 2] = odItem[0];
                                }
                                itemOD.Name = stringArray[0] + " ";
                                if (!stringArray[1].Contains("ITEM")) itemOD.Name += stringArray[1];
                                itemOD.Name.Replace("&amp", "");
                                for (int j = 2; j < stringArray.Length; j++)
                                {
                                    var prop = stringArray[j];
                                    if (prop.Contains("ITEM")) itemOD.ItemCd = prop;
                                    //if (prop.Contains("Price")) {
                                    //    itemOD.Price = stringArray[j + 1].Replace("=2E","");                                   
                                    //}
                                    if (prop.Contains("$")) itemOD.Price = prop.Replace("=2E", ".");

                                    //if (prop.Contains("SIZE")) itemOD.Name += " "+ prop;
                                    try
                                    {
                                        if (prop.Contains("Qty")) itemOD.Quantity = Int16.Parse(prop.Replace("Qty: ", ""));

                                    }
                                    catch (Exception e)
                                    {
                                        itemOD.Quantity = Int16.Parse(prop.Replace("Qty:", ""));
                                    }
                                }
                                _context.Item.Add(itemOD);
                            }
                        }
                        if (email.status.Contains("has been shipped"))
                        {
                            var shipto = finishedTask.Result.data.content.Split("SHIP TO:</b><br />");
                            if (shipto.Length < 1)
                            {
                                continue;
                            }
                            var shipto2 = Regex.Split(shipto[1], @"<span.*?>(.*?)<\\/span>");
                            shipto = shipto2[0].Split("</span></span></td>");
                            var shiptotemp = shipto[0].Replace("<span=\r\n style=3D\"color: #000000;\">", "");
                            var shippto3 = shiptotemp.Replace("=\r\n", "");
                            var shippto4 = shippto3.Split("<br />");
                            if (shippto4.Length < 1)
                            {
                                continue;
                            }
                            email.name = shippto4[0].Trim();
                            if (email.name.Contains("<span"))
                            {
                                email.name = email.name.Split(">")[1].Trim();
                            }
                            for (int index = 1; index < shippto4.Length; index++)
                            {
                                email.shippto += shippto4[index] + " ";

                            }

                            var tracking = tempData.Split("G #:");
                            if (tracking.Length < 2)
                            {
                                continue;
                            }
                            var tracking1 = tracking[1].Split("SHIPMENT");
                            if (tracking1.Length < 2)
                            {
                                continue;
                            }
                            var tracking2 = tracking1[0].Replace("=\r\n", "");
                            var tracking3 = tracking2.Split("style=3D\"color: #000000;\">");
                            if (tracking3.Length < 2)
                            {
                                continue;
                            }
                            var tracking4 = tracking3[1].Replace("</a><br /> <span", "");
                            email.tracking = tracking4;
                            // order total                      
                            var orderTotal = finishedTask.Result.data.content.Replace("=\r\n", "");
                            var oderTotal1 = orderTotal.Split("SHIPMENT ORDER TOTAL:</span>");
                            if (oderTotal1.Length < 2) continue;
                            var oderTotal2 = oderTotal1[1].Split("<br />");
                            if (oderTotal2.Length < 2) continue;
                            var oderTotal3 = oderTotal2[0].Replace("=2E", ".");
                            email.orderTotal = oderTotal3;
                            // item
                            var item = orderTotal.Split("<td align=3D\"center\" width=3D\"200\"><a"); // include link
                            if (item.Length < 1) continue;
                            for (int index = 1; index < item.Length; index++)
                            {
                                var itemOD = new Item();
                                itemOD.MessageId = email.messageId;
                                itemOD.ODnumber = email.ODNumber;
                                itemOD.ImageUrl = email.status2;
                                itemOD.receiveiTime = email.receivedTime;
                                itemOD.Address = email.address;

                                var odList = item[index].Split("target=3D\"_blank\">");
                                if (odList.Length < 1) continue;
                                string[] stringArray = new string[odList.Length - 2];

                                for (int odIndex = 2; odIndex < odList.Length; odIndex++)
                                {
                                    var odItem = odList[odIndex].Split("</a></span></td>");
                                    stringArray[odIndex - 2] = odItem[0];
                                }
                                itemOD.Name = stringArray[0];

                                if (!stringArray[1].Contains("ITEM")) itemOD.Name += " " + stringArray[1];
                                itemOD.Name.Replace("&amp", "");

                                for (int j = 2; j < stringArray.Length; j++)
                                {
                                    var prop = stringArray[j];
                                    if (prop.Contains("<")) continue;
                                    if (prop.Contains("ITEM")) itemOD.ItemCd = prop;
                                    //if(prop.Contains(""))
                                    //if (prop.Contains("Price"))
                                    //{
                                    //    itemOD.Price = stringArray[j + 1].Replace("=2E", "");
                                    //}
                                    if (prop.Contains("$"))
                                    {
                                        itemOD.Price = prop.Replace("=2E", ".");
                                    }
                                    //if (prop.Contains("SIZE")) itemOD.Name += " "+prop;
                                    try
                                    {
                                        if (prop.Contains("Qty")) itemOD.Quantity = Int16.Parse(prop.Replace("Qty: ", ""));

                                    }
                                    catch (Exception e)
                                    {
                                        itemOD.Quantity = Int16.Parse(prop.Replace("Qty:", ""));
                                    }
                                }
                                _context.Item.Add(itemOD);
                            }

                        }

                        if (email.status.Contains("is almost here!"))
                        {
                            var tempD = finishedTask.Result.data.content.Replace("=\r\n", "");
                            var shipto = tempD.Split("<b>SHIP TO:</b>");
                            if (shipto.Length < 2)
                            {

                                shipto = finishedTask.Result.data.content.Split("<b>SHIP=\r\n TO:</b>");

                            }
                            if (shipto.Length < 2)
                            {
                                email.shippto = "shipto Split error";
                            }
                            var shiptoTemp = shipto[1].Split("<br></span></div></td><");
                            if (shiptoTemp.Length < 1)
                            {
                                continue;
                            }
                            var shippto1 = shiptoTemp[0].Replace("=\r\n", "");
                            var shippto2 = shippto1.Split("<br>");
                            if (shippto2.Length < 1)
                            {
                                continue;
                            }
                            email.name = shippto2[0].Trim();
                            if (email.name.Contains("<span"))
                            {
                                email.name = email.name.Split(">")[1].Trim();
                            }
                            for (int index = 1; index < shippto2.Length; index++)
                            {
                                email.shippto += shippto2[index] + " ";

                            }
                            if (email.shippto.Contains("</span>"))
                            {
                                email.shippto = email.shippto.Replace("</span>", "");
                            }
                            if (email.shippto.Contains("</div>"))
                            {
                                var temp = email.shippto.Split("</div></td></tr></table>");
                                if (temp.Length > 0)
                                {
                                    email.shippto = temp[0];

                                }

                            }
                            var tracking1 = tempData.Replace("=\r\n", "");

                            var tracking = tracking1.Split("TRACKING #:");
                            if (tracking.Length < 2)
                            {
                                continue;
                            }
                            var tracking2 = tracking[1].Split("</a><br>ORDER DATE");
                            if (tracking2.Length < 1)
                            {
                                continue;
                            }
                            var tracking3 = tracking2[0].Split("text-decoration: none;\">");
                            if (tracking3.Length < 2)
                            {
                                continue;
                            }
                            email.tracking = tracking3[1];
                            // Order Total
                            var orderTotal = finishedTask.Result.data.content.Replace("=\r\n", "");
                            // item
                            var item = orderTotal.Split("<td align=3D\"left\" class=3D\"mobile-12\" style=3D\"font-size:0px;padding:0;word-break:break-word;\"><div style=3D\"font-family:Helvetica;font-size:12px;font-weight:700;letter-spacing:0.25;line-height:18px;text-align:left;color:#0A0A0A;\">"); // include link
                            if (item.Length < 1) continue;
                            //</ div ></ td ></ tr >< tr >< td
                            //<div style=3D\"font-family:Helvetica;font-size:12px;font-weight:400;letter-spacing:0.25;line-height:18px;text-align:center;color:#4D4D4D;\">
                            for (int index = 1; index < item.Length; index++)
                            {
                                var itemOD = new Item();
                                itemOD.MessageId = email.messageId;
                                itemOD.ODnumber = email.ODNumber;
                                itemOD.ImageUrl = email.status2;
                                itemOD.receiveiTime = email.receivedTime;
                                itemOD.Address = email.address;

                                var odList = item[index].Split("</div></td></tr><tr><td");
                                if (odList.Length < 1) continue;
                                itemOD.Name = odList[0];
                                itemOD.Name.Replace("&amp", "");

                                var qty = item[index].Split("<div style=3D\"font-family:Helvetica;font-size:12px;font-weight:400;letter-spacing:0.25;line-height:18px;text-align:center;color:#4D4D4D;\">");
                                var qty1 = qty[1].Split("</div></td></tr></table></td></tr></tbody></table></div>")[0];
                                try
                                {
                                    if (qty1 != null) itemOD.Quantity = Int16.Parse(qty1);

                                }
                                catch (Exception e)
                                {
                                    continue;
                                }
                                _context.Item.Add(itemOD);
                            }
                        }
                        if (email.status.Contains("has been partially shipped"))
                        {
                            var tempData1 = tempData.Replace("=\r\n", "");

                            var tracking = tempData1.Split("G #:");
                            if (tracking.Length < 2)
                            {
                                continue;
                            }
                            var tracking1 = tracking[1].Split("SHIPMENT");
                            if (tracking1.Length < 2)
                            {
                                continue;
                            }
                            var tracking2 = tracking1[0].Replace("=\r\n", "");
                            var tracking3 = tracking2.Split("style=3D\"color: #000000;\">");
                            if (tracking3.Length < 2)
                            {
                                continue;
                            }
                            var tracking4 = tracking3[1].Replace("</a><br /> <span", "");
                            email.tracking = tracking4;

                            // shipto
                            var shipto = tempData1.Split("<b>SHIP TO:</b>");
                            if (shipto.Length < 2) continue;

                            var shipto1 = shipto[1].Split("</span></span></td>");
                            if (shipto1.Length < 2) continue;
                            var shipto2 = shipto1[0].Split("#000000;\">");
                            if (shipto2.Length < 2) continue;
                            var shipto3 = shipto2[1].Split("<br />");
                            if (shipto3.Length < 2) continue;
                            email.name = shipto3[0].Trim();
                            for (int index = 1; index < shipto3.Length; index++)
                            {
                                email.shippto += shipto3[index];
                            }
                            //if (oderUpdateLinkTrack != null)
                            //{
                            //    oderUpdateLinkTrack.LinkTrack = oderUpdateLinkTrack.LinkTrack + tracking4;
                            //    _context.DataDauVao.Update(oderUpdateLinkTrack);
                            //}
                            // order total                      
                            var orderTotal = finishedTask.Result.data.content.Replace("=\r\n", "");
                            var oderTotal1 = orderTotal.Split("SHIPMENT ORDER TOTAL:</span>");
                            if (oderTotal1.Length < 2) continue;
                            var oderTotal2 = oderTotal1[1].Split("<br />");
                            if (oderTotal2.Length < 2) continue;
                            var oderTotal3 = oderTotal2[0].Replace("=2E", ".");
                            email.orderTotal = oderTotal3;
                            // item
                            var item = orderTotal.Split("<td align=3D\"center\" width=3D\"200\"><a"); // include link
                            if (item.Length < 1) continue;
                            for (int index = 1; index < item.Length; index++)
                            {
                                var itemOD = new Item();
                                itemOD.MessageId = email.messageId;
                                itemOD.ODnumber = email.ODNumber;
                                itemOD.ImageUrl = email.status2;
                                itemOD.receiveiTime = email.receivedTime;
                                itemOD.Address = email.address;

                                var odList = item[index].Split("target=3D\"_blank\">");
                                if (odList.Length < 1) continue;
                                string[] stringArray = new string[odList.Length - 2];

                                for (int odIndex = 2; odIndex < odList.Length; odIndex++)
                                {
                                    var odItem = odList[odIndex].Split("</a></span></td>");
                                    stringArray[odIndex - 2] = odItem[0];
                                }
                                itemOD.Name = stringArray[0] + " ";
                                if (!stringArray[1].Contains("ITEM")) itemOD.Name += stringArray[1];
                                itemOD.Name.Replace("&amp", "");

                                for (int j = 2; j < stringArray.Length; j++)
                                {
                                    var prop = stringArray[j];
                                    if (prop.Contains("ITEM")) itemOD.ItemCd = prop;
                                    //if (prop.Contains("Price"))
                                    //{
                                    //    itemOD.Price = stringArray[j + 1].Replace("=2E", "");
                                    //}
                                    if (prop.Contains("$")) itemOD.Price = prop.Replace("=2E", ".");

                                    //if (prop.Contains("SIZE")) itemOD.Name += " " + prop;
                                    try
                                    {
                                        if (prop.Contains("Qty")) itemOD.Quantity = Int16.Parse(prop.Replace("Qty: ", ""));

                                    }
                                    catch (Exception e)
                                    {
                                        itemOD.Quantity = Int16.Parse(prop.Replace("Qty:", ""));
                                    }
                                }
                                _context.Item.Add(itemOD);
                            }
                        }
                        if (email.status2 != null)
                        {
                            _context.EmailReader.Update(email);

                        }
                    }
                }
               
            }
            _context.SaveChanges();
            UpdateEmailOrder();
            UpdateEmailCancel();
            UpdateEmailGroup();
            UpdateDashboardData();
            return RedirectToAction(nameof(ReadEmail));

        }
        public void UpdateEmailOrder()
        {
            //update order
            _context.DataDauVao.Where(order => order.stopOrder != true && order.CanMua != null).ToList().ForEach(order =>
            {
                int orderNeedPay;

                bool successs = int.TryParse(order.CanMua, out orderNeedPay);

                if (order.isChecked && successs)
                {
                    var emailReaded = _context.EmailReader.Where(e => e.odParrent == order.Id && e.priority != "cancel").ToList();

                    int DAMUA;

                    bool success = int.TryParse(order.DaMua, out DAMUA);
                    if (success)
                    {
                        if (emailReaded.Count() < DAMUA)
                        {
                            var emailReader = _context.EmailReader.Where(e => e.odParrent == 0 && order.Name == e.name && order.CreateDate <= e.receivedTimeLong && e.priority != "cancel").Take(DAMUA - emailReaded.Count()).ToList();
                            emailReader.ForEach(email => email.odParrent = order.Id);
                            _context.SaveChanges();

                        }
                    }
                }
                else if (successs)
                {
                    int DAMUA;

                    bool success = int.TryParse(order.DaMua, out DAMUA);
                    if (success || DAMUA == 0)
                    {
                        var emailReader = _context.EmailReader.Where(e => e.odParrent == 0 && order.Name == e.name && order.CreateDate <= e.receivedTimeLong && e.priority != "cancel").Take(orderNeedPay - DAMUA).ToList();
                        emailReader.ForEach(email => email.odParrent = order.Id);

                        order.DaMua = (DAMUA + emailReader.Count()).ToString();
                        if (order.DaMua == order.CanMua) order.isChecked = true;
                        emailReader.Where(e => e.status2 == "0").ToList().ForEach(e =>
                        {
                            order.LinkTrack += e.tracking;
                        });
                        _context.SaveChanges();

                    }
                }
            });

        }
        public void UpdateEmailCancel()
        {
            //add or update order cancel
            var deleteEmail = _context.EmailReader.Where(e => e.status2 == "-1").Select(e => e).ToList();
            for (int i = 0; i < deleteEmail.Count; i++)
            {
                var cancelEmail = _context.EmailReader.Where(e => e.ODNumber == deleteEmail[i].ODNumber && e.status2 == "1" && e.odParrent != 0).FirstOrDefault();
                if (cancelEmail != null)
                {
                    cancelEmail.priority = "cancel";
                    var emailCancel = new EmailCancel();
                    emailCancel.Name = cancelEmail.name;
                    emailCancel.ODParrent = cancelEmail.odParrent;
                    emailCancel.ODNumber = cancelEmail.ODNumber;
                    emailCancel.Shippto = cancelEmail.shippto;
                    emailCancel.Status = cancelEmail.status;
                    emailCancel.ReceivedTime = cancelEmail.receivedTime;
                    var time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(cancelEmail.receivedTimeLong).ToLocalTime();
                    emailCancel.ReceivedTimeFD = time;

                    _context.EmailReader.Update(cancelEmail);
                    _context.EmailCancel.Add(emailCancel);
                }

            }
            _context.SaveChanges();

        }
        public void UpdateEmailGroup()
        {
            // Update email group
            _context.Database.ExecuteSqlCommand("DELETE FROM [EmailGroup]");
            var groupStatus = (from e in _context.EmailReader
                               where e.ODNumber != null && e.priority != "cancel"
                               group e by e.ODNumber into g
                               select new { ODNumber = g.Key, status = g.Where(e => e != null).Max(e => e.status2) });

            var result = (from e in _context.EmailReader
                          join g in groupStatus on e.ODNumber equals g.ODNumber
                          where e.status2 != null && (e.status2 == g.status || e.status2 == "0")
                          select new EmailGroup
                          {
                              EmailReaderId = e.Id,
                              ODNumber = e.ODNumber,
                              address = e.address,
                              name = (from em in _context.EmailReader where em.ODNumber == e.ODNumber && em.name != null && em.name != "" select em.name).FirstOrDefault(),
                              shippto = e.shippto,
                              status = e.status,
                              status2 = e.status2,
                              MessageId = e.messageId,
                              fromAddress = e.fromAddress,
                              toAddress = e.toAddress,
                              tracking = _context.EmailReader.Where(rd => rd.ODNumber == e.ODNumber && rd.tracking != null && rd.tracking != "").Select(em => em.tracking).FirstOrDefault(),
                              orderTotal = _context.EmailReader.Where(rd => rd.ODNumber == e.ODNumber && rd.orderTotal != null && rd.orderTotal != "").Select(em => em.orderTotal).FirstOrDefault(),
                              received = e.receivedTime != null ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(e.receivedTime)) : DateTime.MinValue,
                              ODParrent = _context.EmailReader.Where(rd => rd.ODNumber == e.ODNumber && rd.odParrent != 0).Select(em => em.odParrent).FirstOrDefault(),
                              shipped = e.shipped,
                              estimatime = _context.EmailReader.Where(em => em.ODNumber == e.ODNumber && em.status2 == "1").Select(em => em.estimateDilivery).FirstOrDefault(),
                              receivedTime = e.receivedTime != null ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(e.receivedTime)).ToLocalTime().ToString("dd-MM-yyyy hh:mm:ss tt") : String.Empty
                          }).ToList();

            _context.EmailGroup.AddRange(result);
            _context.SaveChanges();
        }
        public void UpdateDashboardData()
        {
            var date = DateTime.Now;
            var dataDashBoard = new DashboardData();

            dataDashBoard.Month = DateTime.Now.Month;
            dataDashBoard.Year = DateTime.Now.Year;
            dataDashBoard.SiteName = "Sephora";
            dataDashBoard.TotalCancel = _context.EmailCancel.Where(e => e.ODParrent != 0).Count();

            // Email delay        
            var emailDelays = _context.EmailGroup.Where(e => e.status.Contains("confirm") && e.estimatime <= date && e.ODParrent != 0).Select(e => new EmailDelay
            {
                //Id = e.EmailId,
                tracking = e.tracking,
                ODNumber = e.ODNumber,
                name = e.name,
                ODParrent = e.ODParrent,
                shippto = e.shippto,
                status = e.status,
                fromAddress = e.fromAddress,
                receivedTime = e.receivedTime,
                shipped = e.shipped,
                estimatime = e.estimatime,
                orderTotal = e.orderTotal,
                MessageId = e.MessageId,
                EmailGroupId = e.Id
            }).ToList();

            _context.EmailDelay.AddRange(emailDelays);
            _context.SaveChanges();
            dataDashBoard.TotalDelay = _context.EmailDelay.Where(e => e.shipped == false).Count();
            // total order
            var orderItemsCount = _context.EmailGroup.Where(e => e.received.Month == date.Month && e.received.Year == date.Year && e.ODParrent != 0).Count();


            dataDashBoard.TotalOrder = _context.EmailGroup.Count();
            var previosOrder = _context.EmailGroup.Where(e => e.received.Month == date.Month - 1 && e.received.Year == date.Year && e.ODParrent != 0).Count();
            if (previosOrder != 0)
            {
                dataDashBoard.PercentOrder = orderItemsCount * 100 / previosOrder;

            }

            _context.DataDauVao.ToList().ForEach(d => {
                // profit
                var total = 0D;
                var totalPrevios = 0D;
                float offset = d.tyGiaBan - d.tyGiaMua;
            
                float a;
                int c;
                if (d.TongUSD != null)
                {
                    var b = float.TryParse(d.TongUSD, out a);
                    var e = Int32.TryParse(d.DaMua, out c);
                    if (b && e)
                    {
                        total = offset * a * c;
                    }
                }           

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

                if (_context.DataProfitOrder.Any(dt => dt.OrderId == d.Id))
                {
                    var dprofit = _context.DataProfitOrder.Single(dp => dp.OrderId == d.Id);
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
                    _context.DataProfitOrder.Update(dprofit);

                }
                else _context.DataProfitOrder.Add(data);

            });

            dataDashBoard.TotalProfit = _context.DataProfitOrder.Sum(ord => ord.TotalProfit);
           
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
                data.SiteName = dataDashBoard.SiteName;
                _context.DashboardData.Update(data);
            }
            else
            {
                _context.DashboardData.Add(dataDashBoard);
            }
        }
        public async Task<EmailContent> ProcessUrlAsync(MailData mail, OauthZohoResponse receivied)
        {
            EmailContent content = await GetEmailContent(mail.messageId, receivied);
            var email = new EmailReader();

            string[] splitString = Regex.Split(mail.subject, @"#");
            string[] digits = new string[] { };
            string[] digitStatus = new string[] { };
            //var oderUpdateLinkTrack = new DataDauVao();

            if (splitString.Length > 1)
            {
                digits = Regex.Split(splitString[1], @"\D+");
                digitStatus = Regex.Split(splitString[1], @"\s");
            }

            var status = "";
            if (digitStatus.Length > 1)
            {

                for (int index = 1; index < digitStatus.Length; index++)
                {
                    status += digitStatus[index] + " ";

                }
            }
            email.ODNumber = !String.IsNullOrEmpty(digits[0]) ? digits[0] : "";
            email.status = String.IsNullOrEmpty(status) ? "confirm" : status;
            if (!String.IsNullOrEmpty(status))
            {
                email.status = status;
            }
            email.address = "";
            if (email.status.Contains("has been shipped")) email.status2 = "2";         
            if (email.status.Contains("is almost here!"))  email.status2 = "3";

            if (email.status.Contains("confirm"))  email.status2 = "1";

            if (email.status.Contains("has been partially shipped")) email.status2 = "0";

            if (email.status.Contains("has been cancelled"))
            {
                email.status2 = "-1";
                email.priority = "cancel";
            }

            email.ccAddress = mail.ccAddress;
            //email.estimateDilivery = "";
            email.folderId = mail.folderId;
            email.fromAddress = mail.fromAddress;
            email.Id = 0;
            email.messageId = mail.messageId;
            email.hasInline = mail.hasInline;
            email.summary = mail.summary;
            email.toAddress = mail.toAddress.Replace("@lt;", "").Replace("&gt;", "");
            email.shippto = "";
            email.sender = "";
            email.subject = mail.subject;
            email.orderDate = "";
            email.orderTotal = "";
            email.priority = "";
            email.receivedTime = mail.receivedTime;
            //todo
            email.receivedTimeLong = long.Parse(mail.receivedTime);
            email.sentDateInGMT = mail.sentDateInGMT;

            _context.EmailReader.Add(email);
            _context.SaveChanges();
            return content;
        }
        public async Task<IActionResult> ReadEmailAction(string code)
        {
            try
            {
                if (String.IsNullOrEmpty(code))
                {
                    return RedirectToAction(nameof(ReadEmail));

                }
                OauthZohoResponse received = new OauthZohoResponse();
                //await Auth2Token();

                received = await getAccessToken(code, received);

                List<MailData> mailData = new List<MailData>();
                if (received.access_token == null) { return RedirectToAction(nameof(ReadEmail)); }
                mailData = await GetEmail(received);
  
                //if (mailData.Count == 0) RedirectToAction(nameof(ReadEmail));
                var totalDem = 0;
                for (int i = mailData.Count - 1; i >= 0; --i)
                {
                    var mail = mailData[i];
                    string[] splitString = Regex.Split(mail.subject, @"#");
                    string[] digits = new string[] { };
                    string[] digitStatus = new string[] { };
                    //var oderUpdateLinkTrack = new DataDauVao();

                    if (splitString.Length > 1)
                    {
                        digits = Regex.Split(splitString[1], @"\D+");
                        digitStatus = Regex.Split(splitString[1], @"\s");
                    }
                    else
                    {
                        continue;
                    }

                    var status = "";
                    if (digitStatus.Length > 1)
                    {

                        for (int index = 1; index < digitStatus.Length; index++)
                        {
                            status += digitStatus[index] + " ";

                        }
                    }
                    var email = new EmailReader();
                    email.ODNumber = !String.IsNullOrEmpty(digits[0]) ? digits[0] : "";
                    email.status = String.IsNullOrEmpty(status) ? "confirm" : status;
                    if (!String.IsNullOrEmpty(status))
                    {
                        email.status = status;
                    }
                    email.address = "";
                    if (email.status.Contains("has been shipped"))
                    {
                        email.status2 = "2";

                    }
                    if (email.status.Contains("is almost here!"))
                    {
                        email.status2 = "3";

                    }
                    if (email.status.Contains("confirm"))
                    {
                        email.status2 = "1";

                    }
                    if (email.status.Contains("has been partially shipped"))
                    {
                        email.status2 = "0";
                    }


                    email.ccAddress = mail.ccAddress;
                    //email.estimateDilivery = "";
                    email.folderId = mail.folderId;
                    email.fromAddress = mail.fromAddress;
                    email.Id = 0;
                    email.messageId = mail.messageId;
                    email.hasInline = mail.hasInline;
                    email.summary = mail.summary;
                    email.toAddress = mail.toAddress.Replace("@lt;", "").Replace("&gt;", "");
                    email.shippto = "";
                    email.sender = "";
                    email.subject = mail.subject;
                    email.orderDate = "";
                    email.orderTotal = "";
                    email.priority = "";
                    email.receivedTime = mail.receivedTime;
                    //todo
                    email.receivedTimeLong = long.Parse(mail.receivedTime);
                    email.sentDateInGMT = mail.sentDateInGMT;
                    if (email.status.Contains("has been cancelled"))
                    {
                        email.status2 = "-1";
                        email.priority = "cancel";
                        _context.EmailReader.Add(email);
                        continue;

                    }
                    var messID = _context.EmailReader.FirstOrDefault(e => e.messageId == mail.messageId);
                    if (messID != null) continue;
                    EmailContent emailContent = await GetEmailContent(mail.messageId, received);
                    if(emailContent == null)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            received = await OauthRefreshToken(httpClient, received);
                            emailContent = await GetEmailContent(mail.messageId, received);
                        }
                    }
                    if (emailContent.data == null) continue;
                    var tempData = emailContent.data.content;

                    var tempMail = tempData.Split("address");
                    if (tempMail.Length > 1)
                    {
                        var tempMail1 = tempMail[1].Split("</span>");
                        var tempMail2 = tempMail1[0].Replace("=\r\n", "");
                        var tempMail3 = tempMail2.Replace("\">", "");
                        email.address = tempMail3;
                    }
                    else
                    {
                        tempMail = tempData.Split("tact Us");
                        if (tempMail.Length < 2)
                        {
                            tempMail = tempData.Split("tac=\r\nt Us");
                            if (tempMail.Length < 2)
                            {
                                tempMail = tempData.Split("tact=\r\n Us");

                            }
                        }
                        if (tempMail.Length < 2) continue;
                        var temp = tempMail[1].Split("All rights reserve");
                        if (temp.Length < 1)
                        {
                            continue;
                        }
                        var temp1 = Regex.Split(temp[0], "<a .?>(.*?)</a>");
                        if (temp1.Length < 1)
                        {
                            continue;
                        }
                        var shippto6 = temp1[0].Replace("=\r\n", "");
                        var shippto3 = shippto6.Split("color:#CCCCCC;\">");
                        if (shippto3.Length < 2)
                        {
                            //var shippto3 = temp1[0].Split("color:#CCCCCC;\"=\r\n>");
                            continue;
                        }
                        var shippto4 = shippto3[1].Split("</a>");
                        if (shippto4.Length < 1)
                        {
                            continue;
                        }
                        var shippto5 = shippto4[0].Replace("=\r\n", "");
                        email.address = shippto5;
                    }
                    // linkTrack

                    if (email.status.Contains("confirm"))
                    {
                        var shippto = emailContent.data.content.Split("<b>SHIP TO</b><b>:</b>");
                        if (shippto.Length < 2)
                        {
                            continue;
                        }
                        var shippto2 = shippto[1].Split("<br />\r\n\t");
                        if (shippto2.Length < 1)
                        {
                            continue;
                        }
                        var shippto3 = shippto2[0].Replace("=\r\n", "");
                        if (shippto3 == null)
                        {
                            continue;
                        }
                        var shippto4 = shippto3.Split("<br />");
                        if (shippto4.Length < 1)
                        {
                            continue;
                        }
                        email.name = shippto4[0].Replace("<br>", "").Trim();
                        if (email.name.Contains("<span"))
                        {
                            email.name = email.name.Split(">")[1].Trim();
                        }
                        if (shippto.Length > 0)
                        {
                            for (int index = 1; index < shippto.Length; index++)
                            {
                                email.shippto += shippto[index];

                            }
                        }
                        email.shippto = shippto4[1] + shippto4[2];
                        // Order Total
                        var orderTotal = emailContent.data.content.Replace("=\r\n", "");
                        var estimateTime = orderTotal.Split("Estimated Delivery Date:</b>");
                        if (estimateTime.Length > 1)
                        {
                            var estimateTime1 = estimateTime[1].Split("\r\n</span></td>\r\n</tr>\r\n");
                            var estimateTime2 = estimateTime1[0].Trim();
                            try
                            {
                                DateTime dt = DateTime.Parse(estimateTime2, CultureInfo.InvariantCulture);
                                email.estimateDilivery = dt;

                                //TimeSpan epoch = (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                                //var unixTimestamp = (double)epoch.TotalMilliseconds;
                                //email.estimateDilivery = unixTimestamp.ToString();
                            }
                            catch (Exception e)
                            {
                                //no estimateDilivery
                            }
                        }
                        var oderTotal1 = orderTotal.Split("Gift Card:");
                        if (oderTotal1.Length < 2) continue;
                        var oderTotal2 = oderTotal1[1].Split("<br /> </td>");
                        if (oderTotal2.Length < 2) continue;
                        var oderTotal3 = oderTotal2[0].Replace("=2E", ".");
                        email.orderTotal = oderTotal3;
                        // item
                        var item = orderTotal.Split("<td align=3D\"center\" width=3D\"200\"><a"); // include link
                        if (item.Length < 1) continue;
                        for (int index = 1; index < item.Length; index++)
                        {
                            var itemOD = new Item();
                            itemOD.MessageId = email.messageId;
                            itemOD.ODnumber = email.ODNumber;
                            itemOD.ImageUrl = email.status2;
                            itemOD.receiveiTime = email.receivedTime;
                            itemOD.Address = email.address;
                            var odList = item[index].Split("target=3D\"_blank\">");
                            if (odList.Length < 1) continue;
                            string[] stringArray = new string[odList.Length - 2];

                            for (int odIndex = 2; odIndex < odList.Length; odIndex++)
                            {
                                var odItem = odList[odIndex].Split("</a></span></td>");
                                stringArray[odIndex - 2] = odItem[0];
                            }
                            itemOD.Name = stringArray[0] + " ";
                            if (!stringArray[1].Contains("ITEM")) itemOD.Name += stringArray[1];
                            itemOD.Name.Replace("&amp", "");
                            for (int j = 2; j < stringArray.Length; j++)
                            {
                                var prop = stringArray[j];
                                if (prop.Contains("ITEM")) itemOD.ItemCd = prop;
                                //if (prop.Contains("Price")) {
                                //    itemOD.Price = stringArray[j + 1].Replace("=2E","");                                   
                                //}
                                if (prop.Contains("$")) itemOD.Price = prop.Replace("=2E", ".");

                                //if (prop.Contains("SIZE")) itemOD.Name += " "+ prop;
                                try
                                {
                                    if (prop.Contains("Qty")) itemOD.Quantity = Int16.Parse(prop.Replace("Qty: ", ""));

                                }
                                catch (Exception e)
                                {
                                    itemOD.Quantity = Int16.Parse(prop.Replace("Qty:", ""));
                                }
                            }
                            _context.Item.Add(itemOD);
                        }
                    }
                    if (email.status.Contains("has been shipped"))
                    {
                        var shipto = emailContent.data.content.Split("SHIP TO:</b><br />");
                        if (shipto.Length < 1)
                        {
                            continue;
                        }
                        var shipto2 = Regex.Split(shipto[1], @"<span.*?>(.*?)<\\/span>");
                        shipto = shipto2[0].Split("</span></span></td>");
                        var shiptotemp = shipto[0].Replace("<span=\r\n style=3D\"color: #000000;\">", "");
                        var shippto3 = shiptotemp.Replace("=\r\n", "");
                        var shippto4 = shippto3.Split("<br />");
                        if (shippto4.Length < 1)
                        {
                            continue;
                        }
                        email.name = shippto4[0].Trim();
                        if (email.name.Contains("<span"))
                        {
                            email.name = email.name.Split(">")[1].Trim();
                        }
                        for (int index = 1; index < shippto4.Length; index++)
                        {
                            email.shippto += shippto4[index] + " ";

                        }

                        var tracking = tempData.Split("G #:");
                        if (tracking.Length < 2)
                        {
                            continue;
                        }
                        var tracking1 = tracking[1].Split("SHIPMENT");
                        if (tracking1.Length < 2)
                        {
                            continue;
                        }
                        var tracking2 = tracking1[0].Replace("=\r\n", "");
                        var tracking3 = tracking2.Split("style=3D\"color: #000000;\">");
                        if (tracking3.Length < 2)
                        {
                            continue;
                        }
                        var tracking4 = tracking3[1].Replace("</a><br /> <span", "");
                        email.tracking = tracking4;
                        // order total                      
                        var orderTotal = emailContent.data.content.Replace("=\r\n", "");
                        var oderTotal1 = orderTotal.Split("SHIPMENT ORDER TOTAL:</span>");
                        if (oderTotal1.Length < 2) continue;
                        var oderTotal2 = oderTotal1[1].Split("<br />");
                        if (oderTotal2.Length < 2) continue;
                        var oderTotal3 = oderTotal2[0].Replace("=2E", ".");
                        email.orderTotal = oderTotal3;
                        // item
                        var item = orderTotal.Split("<td align=3D\"center\" width=3D\"200\"><a"); // include link
                        if (item.Length < 1) continue;
                        for (int index = 1; index < item.Length; index++)
                        {
                            var itemOD = new Item();
                            itemOD.MessageId = email.messageId;
                            itemOD.ODnumber = email.ODNumber;
                            itemOD.ImageUrl = email.status2;
                            itemOD.receiveiTime = email.receivedTime;
                            itemOD.Address = email.address;

                            var odList = item[index].Split("target=3D\"_blank\">");
                            if (odList.Length < 1) continue;
                            string[] stringArray = new string[odList.Length - 2];

                            for (int odIndex = 2; odIndex < odList.Length; odIndex++)
                            {
                                var odItem = odList[odIndex].Split("</a></span></td>");
                                stringArray[odIndex - 2] = odItem[0];
                            }
                            itemOD.Name = stringArray[0];

                            if (!stringArray[1].Contains("ITEM")) itemOD.Name += " " + stringArray[1];
                            itemOD.Name.Replace("&amp", "");

                            for (int j = 2; j < stringArray.Length; j++)
                            {
                                var prop = stringArray[j];
                                if (prop.Contains("<")) continue;
                                if (prop.Contains("ITEM")) itemOD.ItemCd = prop;
                                //if(prop.Contains(""))
                                //if (prop.Contains("Price"))
                                //{
                                //    itemOD.Price = stringArray[j + 1].Replace("=2E", "");
                                //}
                                if (prop.Contains("$"))
                                {
                                    itemOD.Price = prop.Replace("=2E", ".");
                                }
                                //if (prop.Contains("SIZE")) itemOD.Name += " "+prop;
                                try
                                {
                                    if (prop.Contains("Qty")) itemOD.Quantity = Int16.Parse(prop.Replace("Qty: ", ""));

                                }
                                catch (Exception e)
                                {
                                    itemOD.Quantity = Int16.Parse(prop.Replace("Qty:", ""));
                                }
                            }
                            _context.Item.Add(itemOD);
                        }

                    }

                    if (email.status.Contains("is almost here!"))
                    {
                        var tempD = emailContent.data.content.Replace("=\r\n", "");
                        var shipto = tempD.Split("<b>SHIP TO:</b>");
                        if (shipto.Length < 2)
                        {

                            shipto = emailContent.data.content.Split("<b>SHIP=\r\n TO:</b>");

                        }
                        if (shipto.Length < 2)
                        {
                            email.shippto = "shipto Split error";
                        }
                        var shiptoTemp = shipto[1].Split("<br></span></div></td><");
                        if (shiptoTemp.Length < 1)
                        {
                            continue;
                        }
                        var shippto1 = shiptoTemp[0].Replace("=\r\n", "");
                        var shippto2 = shippto1.Split("<br>");
                        if (shippto2.Length < 1)
                        {
                            continue;
                        }
                        email.name = shippto2[0].Trim();
                        if (email.name.Contains("<span"))
                        {
                            email.name = email.name.Split(">")[1].Trim();
                        }
                        for (int index = 1; index < shippto2.Length; index++)
                        {
                            email.shippto += shippto2[index] + " ";

                        }
                        if (email.shippto.Contains("</span>"))
                        {
                            email.shippto = email.shippto.Replace("</span>", "");
                        }
                        if (email.shippto.Contains("</div>"))
                        {
                            var temp = email.shippto.Split("</div></td></tr></table>");
                            if (temp.Length > 0)
                            {
                                email.shippto = temp[0];

                            }

                        }
                        var tracking1 = tempData.Replace("=\r\n", "");

                        var tracking = tracking1.Split("TRACKING #:");
                        if (tracking.Length < 2)
                        {
                            continue;
                        }
                        var tracking2 = tracking[1].Split("</a><br>ORDER DATE");
                        if (tracking2.Length < 1)
                        {
                            continue;
                        }
                        var tracking3 = tracking2[0].Split("text-decoration: none;\">");
                        if (tracking3.Length < 2)
                        {
                            continue;
                        }
                        email.tracking = tracking3[1];
                        // Order Total
                        var orderTotal = emailContent.data.content.Replace("=\r\n", "");
                        // item
                        var item = orderTotal.Split("<td align=3D\"left\" class=3D\"mobile-12\" style=3D\"font-size:0px;padding:0;word-break:break-word;\"><div style=3D\"font-family:Helvetica;font-size:12px;font-weight:700;letter-spacing:0.25;line-height:18px;text-align:left;color:#0A0A0A;\">"); // include link
                        if (item.Length < 1) continue;
                        //</ div ></ td ></ tr >< tr >< td
                        //<div style=3D\"font-family:Helvetica;font-size:12px;font-weight:400;letter-spacing:0.25;line-height:18px;text-align:center;color:#4D4D4D;\">
                        for (int index = 1; index < item.Length; index++)
                        {
                            var itemOD = new Item();
                            itemOD.MessageId = email.messageId;
                            itemOD.ODnumber = email.ODNumber;
                            itemOD.ImageUrl = email.status2;
                            itemOD.receiveiTime = email.receivedTime;
                            itemOD.Address = email.address;

                            var odList = item[index].Split("</div></td></tr><tr><td");
                            if (odList.Length < 1) continue;
                            itemOD.Name = odList[0];
                            itemOD.Name.Replace("&amp", "");

                            var qty = item[index].Split("<div style=3D\"font-family:Helvetica;font-size:12px;font-weight:400;letter-spacing:0.25;line-height:18px;text-align:center;color:#4D4D4D;\">");
                            var qty1 = qty[1].Split("</div></td></tr></table></td></tr></tbody></table></div>")[0];
                            try
                            {
                                if (qty1 != null) itemOD.Quantity = Int16.Parse(qty1);

                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                            _context.Item.Add(itemOD);
                        }
                    }
                    if (email.status.Contains("has been partially shipped"))
                    {
                        var tempData1 = tempData.Replace("=\r\n", "");

                        var tracking = tempData1.Split("G #:");
                        if (tracking.Length < 2)
                        {
                            continue;
                        }
                        var tracking1 = tracking[1].Split("SHIPMENT");
                        if (tracking1.Length < 2)
                        {
                            continue;
                        }
                        var tracking2 = tracking1[0].Replace("=\r\n", "");
                        var tracking3 = tracking2.Split("style=3D\"color: #000000;\">");
                        if (tracking3.Length < 2)
                        {
                            continue;
                        }
                        var tracking4 = tracking3[1].Replace("</a><br /> <span", "");
                        email.tracking = tracking4;

                        // shipto
                        var shipto = tempData1.Split("<b>SHIP TO:</b>");
                        if (shipto.Length < 2) continue;

                        var shipto1 = shipto[1].Split("</span></span></td>");
                        if (shipto1.Length < 2) continue;
                        var shipto2 = shipto1[0].Split("#000000;\">");
                        if (shipto2.Length < 2) continue;
                        var shipto3 = shipto2[1].Split("<br />");
                        if (shipto3.Length < 2) continue;
                        email.name = shipto3[0].Trim();
                        for (int index = 1; index < shipto3.Length; index++)
                        {
                            email.shippto += shipto3[index];
                        }
                        //if (oderUpdateLinkTrack != null)
                        //{
                        //    oderUpdateLinkTrack.LinkTrack = oderUpdateLinkTrack.LinkTrack + tracking4;
                        //    _context.DataDauVao.Update(oderUpdateLinkTrack);
                        //}
                        // order total                      
                        var orderTotal = emailContent.data.content.Replace("=\r\n", "");
                        var oderTotal1 = orderTotal.Split("SHIPMENT ORDER TOTAL:</span>");
                        if (oderTotal1.Length < 2) continue;
                        var oderTotal2 = oderTotal1[1].Split("<br />");
                        if (oderTotal2.Length < 2) continue;
                        var oderTotal3 = oderTotal2[0].Replace("=2E", ".");
                        email.orderTotal = oderTotal3;
                        // item
                        var item = orderTotal.Split("<td align=3D\"center\" width=3D\"200\"><a"); // include link
                        if (item.Length < 1) continue;
                        for (int index = 1; index < item.Length; index++)
                        {
                            var itemOD = new Item();
                            itemOD.MessageId = email.messageId;
                            itemOD.ODnumber = email.ODNumber;
                            itemOD.ImageUrl = email.status2;
                            itemOD.receiveiTime = email.receivedTime;
                            itemOD.Address = email.address;

                            var odList = item[index].Split("target=3D\"_blank\">");
                            if (odList.Length < 1) continue;
                            string[] stringArray = new string[odList.Length - 2];

                            for (int odIndex = 2; odIndex < odList.Length; odIndex++)
                            {
                                var odItem = odList[odIndex].Split("</a></span></td>");
                                stringArray[odIndex - 2] = odItem[0];
                            }
                            itemOD.Name = stringArray[0] + " ";
                            if (!stringArray[1].Contains("ITEM")) itemOD.Name += stringArray[1];
                            itemOD.Name.Replace("&amp", "");

                            for (int j = 2; j < stringArray.Length; j++)
                            {
                                var prop = stringArray[j];
                                if (prop.Contains("ITEM")) itemOD.ItemCd = prop;
                                //if (prop.Contains("Price"))
                                //{
                                //    itemOD.Price = stringArray[j + 1].Replace("=2E", "");
                                //}
                                if (prop.Contains("$")) itemOD.Price = prop.Replace("=2E", ".");

                                //if (prop.Contains("SIZE")) itemOD.Name += " " + prop;
                                try
                                {
                                    if (prop.Contains("Qty")) itemOD.Quantity = Int16.Parse(prop.Replace("Qty: ", ""));

                                }
                                catch (Exception e)
                                {
                                    itemOD.Quantity = Int16.Parse(prop.Replace("Qty:", ""));
                                }
                            }
                            _context.Item.Add(itemOD);
                        }
                    }
                    if (email.status2 != null)
                    {
                        _context.EmailReader.Add(email);

                    }

                }
                _context.SaveChanges();
               
                //update order
                _context.DataDauVao.Where(order => order.stopOrder != true && order.CanMua != null).ToList().ForEach(order =>
                {
                    int orderNeedPay;

                    bool successs = int.TryParse(order.CanMua, out orderNeedPay);

                    if (order.isChecked && successs)
                    {
                        var emailReaded = _context.EmailReader.Where(e => e.odParrent == order.Id && e.priority != "cancel").ToList();

                        int DAMUA;

                        bool success = int.TryParse(order.DaMua, out DAMUA);
                        if (success)
                        {
                            if (emailReaded.Count() < DAMUA)
                            {
                                var emailReader = _context.EmailReader.Where(e => e.odParrent == 0 && order.Name == e.name && order.CreateDate <= e.receivedTimeLong && e.priority != "cancel").Take(DAMUA - emailReaded.Count()).ToList();
                                emailReader.ForEach(email => email.odParrent = order.Id);
                                _context.SaveChanges();

                            }
                        }
                    }
                    else if (successs)
                    {
                        int DAMUA;

                        bool success = int.TryParse(order.DaMua, out DAMUA);
                        if (success || DAMUA == 0)
                        {
                            var emailReader = _context.EmailReader.Where(e => e.odParrent == 0 && order.Name == e.name && order.CreateDate <= e.receivedTimeLong && e.priority != "cancel").Take(orderNeedPay - DAMUA).ToList();
                            emailReader.ForEach(email => email.odParrent = order.Id);

                            order.DaMua = (DAMUA + emailReader.Count()).ToString();
                            if (order.DaMua == order.CanMua) order.isChecked = true;
                            emailReader.Where(e => e.status2 == "0").ToList().ForEach(e =>
                            {
                                order.LinkTrack += e.tracking;
                            });
                            _context.SaveChanges();

                        }
                    }
                });

                //add or update order cancel
                var deleteEmail = _context.EmailReader.Where(e => e.status2 == "-1").Select(e => e).ToList();
                for (int i = 0; i < deleteEmail.Count; i++)
                {
                    var cancelEmail = _context.EmailReader.Where(e => e.ODNumber == deleteEmail[i].ODNumber && e.status2 == "1" && e.odParrent != 0).FirstOrDefault();
                    if (cancelEmail != null)
                    {
                        cancelEmail.priority = "cancel";
                        var emailCancel = new EmailCancel();
                        emailCancel.Name = cancelEmail.name;
                        emailCancel.ODParrent = cancelEmail.odParrent;
                        emailCancel.ODNumber = cancelEmail.ODNumber;
                        emailCancel.Shippto = cancelEmail.shippto;
                        emailCancel.Status = cancelEmail.status;
                        emailCancel.ReceivedTime = cancelEmail.receivedTime;
                        var time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(cancelEmail.receivedTimeLong).ToLocalTime();
                        emailCancel.ReceivedTimeFD = time;

                        _context.EmailReader.Update(cancelEmail);
                        _context.EmailCancel.Add(emailCancel);
                    }

                }
                _context.SaveChanges();

                // Update email group
                _context.Database.ExecuteSqlCommand("DELETE FROM [EmailGroup]");
                var groupStatus = (from e in _context.EmailReader
                                   where e.ODNumber != null && e.priority != "cancel"
                                   group e by e.ODNumber into g
                                   select new { ODNumber = g.Key, status = g.Where(e => e != null).Max(e => e.status2) });

                var result = (from e in _context.EmailReader
                              join g in groupStatus on e.ODNumber equals g.ODNumber
                              where e.status2 != null && (e.status2 == g.status || e.status2 == "0")
                              select new EmailGroup
                              {
                                  EmailReaderId = e.Id,
                                  ODNumber = e.ODNumber,
                                  address = e.address,
                                  name = (from em in _context.EmailReader where em.ODNumber == e.ODNumber && em.name != null && em.name != "" select em.name).FirstOrDefault(),
                                  shippto = e.shippto,
                                  status = e.status,
                                  status2 = e.status2,
                                  MessageId = e.messageId,
                                  fromAddress = e.fromAddress,
                                  toAddress = e.toAddress,
                                  tracking = _context.EmailReader.Where(rd => rd.ODNumber == e.ODNumber && rd.tracking != null && rd.tracking != "").Select(em => em.tracking).FirstOrDefault(),
                                  orderTotal = _context.EmailReader.Where(rd => rd.ODNumber == e.ODNumber && rd.orderTotal != null && rd.orderTotal != "").Select(em => em.orderTotal).FirstOrDefault(),
                                  received = e.receivedTime != null ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(e.receivedTime)) : DateTime.MinValue,
                                  ODParrent = _context.EmailReader.Where(rd => rd.ODNumber == e.ODNumber && rd.odParrent != 0).Select(em => em.odParrent).FirstOrDefault(),
                                  shipped = e.shipped,
                                  estimatime = _context.EmailReader.Where(em => em.ODNumber == e.ODNumber && em.status2 == "1").Select(em => em.estimateDilivery).FirstOrDefault(),
                                  receivedTime = e.receivedTime != null ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(e.receivedTime)).ToLocalTime().ToString("dd-MM-yyyy hh:mm:ss tt") : String.Empty
                              }).ToList();

                _context.EmailGroup.AddRange(result);
                _context.SaveChanges();

                var date = DateTime.Now;
                var dataDashBoard = new DashboardData();

                dataDashBoard.Month = DateTime.Now.Month;
                dataDashBoard.Year = DateTime.Now.Year;
                dataDashBoard.SiteName = "Sephora";
                //email cancel
                //var cancelInMonth = _context.EmailCancel.Where(e => e.ReceivedTimeFD.Year == date.Year && (e.ReceivedTimeFD.Month == date.Month || e.ReceivedTimeFD.Month == date.Month - 1))
                //    .GroupBy(e => new { e.ReceivedTimeFD.Year, e.ReceivedTimeFD.Month }).Select(gr => new { MonthYear = gr.Key.Month + '/' + gr.Key.Year, Total = gr.Count() }).ToList();

                //if (cancelInMonth.Count > 0)
                //{
                //    var currentMonth = cancelInMonth.Where(e => e.MonthYear == date.Month + '/' + date.Year).FirstOrDefault();
                //    var previosMonth = cancelInMonth.Where(e => e.MonthYear != date.Month + '/' + date.Year).FirstOrDefault();
                                  
                //    if (previosMonth != null && currentMonth != null)
                //    {
                //        dataDashBoard.PercentCancel = (currentMonth.Total * 100 / previosMonth.Total) - 100;
                //    }

                //}
                dataDashBoard.TotalCancel = _context.EmailCancel.Where(e => e.ODParrent != 0).Count();
              
                // Email delay        
                var emailDelays = _context.EmailGroup.Where(e => e.status.Contains("confirm") && e.estimatime <= date && e.ODParrent != 0).Select(e => new EmailDelay
                {
                    //Id = e.EmailId,
                    tracking = e.tracking,
                    ODNumber = e.ODNumber,
                    name = e.name,
                    ODParrent = e.ODParrent,
                    shippto = e.shippto,
                    status = e.status,
                    fromAddress = e.fromAddress,
                    receivedTime = e.receivedTime,
                    shipped = e.shipped,
                    estimatime = e.estimatime,
                    orderTotal = e.orderTotal,
                    MessageId = e.MessageId,
                    EmailGroupId = e.Id
                }).ToList();

                _context.EmailDelay.AddRange(emailDelays);
                _context.SaveChanges();
                dataDashBoard.TotalDelay = _context.EmailDelay.Where(e => e.shipped == false).Count();
                // total order
                var orderItemsCount = _context.EmailGroup.Where(e => e.received.Month == date.Month && e.received.Year == date.Year && e.ODParrent != 0).Count();


                dataDashBoard.TotalOrder = _context.EmailGroup.Count();
                var previosOrder = _context.EmailGroup.Where(e => e.received.Month == date.Month - 1 && e.received.Year == date.Year && e.ODParrent != 0).Count();
                if(previosOrder != 0)
                {
                    dataDashBoard.PercentOrder = orderItemsCount * 100 / previosOrder;

                }

                _context.DataDauVao.ToList().ForEach(d => {
                    // profit
                    var total = 0D;
                    var totalPrevios = 0D;
                    float offset = d.tyGiaBan - d.tyGiaMua;
                     //_context.EmailGroup.Where(e => e.ODParrent == d.Id ).ToList().ForEach(
                     //   oderItem =>
                     //   {
                    float a;
                    int c;
                    if (d.TongUSD != null)
                    {
                        var b = float.TryParse(d.TongUSD, out a);
                            var e = Int32.TryParse(d.DaMua, out c);
                        if (b && e)
                        {
                            total = offset * a * c;
                        }
                    }                           
                        //}
                    //);
                    //var totalNet = 0D;
                    //_context.EmailGroup.Where(e => e.ODParrent == d.Id && (e.shipped == true || e.status2 != "1")).ToList().ForEach(
                    //    oderItem =>
                    //    {
                    //        float a;
                    //        if (d.TongUSD != null)
                    //        {
                    //            var b = float.TryParse(d.TongUSD, out a);
                    //            if (b)
                    //            {
                    //                totalNet = totalNet + (offset * a);
                    //            }
                    //        }
                    //    }
                    //);

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
                    //data.NetProfit = totalNet;

                    if (_context.DataProfitOrder.Any(dt => dt.OrderId == d.Id))
                    {
                        var dprofit = _context.DataProfitOrder.Single(dp => dp.OrderId == d.Id);
                        dprofit.TotalProfit = data.TotalProfit;
                        dprofit.ODnumber = d.ODNumber;
                        dprofit.Name = d.Name;
                        dprofit.NgayGui = d.NgayGui;
                        dprofit.GiaUSD = d.GiaUSD;
                        dprofit.DaMua = d.DaMua;
                        dprofit.GiaSale = d.GiaSale;
                        //dprofit.SiteName = "Sephora";
                        dprofit.TotalProfit = total;
                        dprofit.tyGiaBan = d.tyGiaBan;
                        dprofit.tyGiaMua = d.tyGiaMua;
                        dprofit.DaMua = d.DaMua;
                        dprofit.CanMua = d.CanMua;
                        dprofit.orderStop = d.stopOrder;
                        //dprofit.OrderId = d.Id;
                        //dprofit.NetProfit = totalNet;

                        _context.DataProfitOrder.Update(dprofit);

                    }
                    else _context.DataProfitOrder.Add(data);
                    
                });
                                    
                dataDashBoard.TotalProfit = _context.DataProfitOrder.Sum(ord => ord.TotalProfit);
                //if(totalPrevios > 0) {
                //    dataDashBoard.PercentProfit = (dataDashBoard.TotalProfit * 100 / totalPrevios);
                //}
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
                    data.SiteName = dataDashBoard.SiteName;
                    _context.DashboardData.Update(data);
                }
                else
                {
                    _context.DashboardData.Add(dataDashBoard);
                }
                _context.SaveChanges();                                          
                return RedirectToAction(nameof(ReadEmail));

            }
            catch (Exception e)
            {
                return Json(new { response = e, code = 1 });
            }

        }

        public async Task Auth2Token()
        {
            string UrlWithParams = auth2 + "?scope=" + scope + "&client_id=" + client_id + "&response_type=code" + "&redirect_uri=" + redirect_uri;
            using (var httpClient = new HttpClient())
            {

                var res = await httpClient.GetAsync(UrlWithParams);
                if (res.IsSuccessStatusCode)
                {
                    string mailViewResponse = await res.Content.ReadAsStringAsync();
                    //var data = JsonConvert.DeserializeObject<Root>(mailViewResponse);
                }

            }
        }
        private async Task<OauthZohoResponse> getAccessToken(string code, OauthZohoResponse received)
        {
            // Khởi tạo http client
            using (var httpClient = new HttpClient())
            {
                var form = new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"client_id", "1000.16VT5D5DQ71NQWFD3LNR6DDUD0F8XS"},
                    {"client_secret", "8cdcf3ddda5934c3c8510dc9a187741f58d96c4f0c"},
                    {"code",  code },

                };
                using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
                {
                    foreach (var keyValuePair in form)
                    {
                        formDataContent.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                    }

                    if (String.IsNullOrEmpty(refreshtoken))
                    {

                        using (var response = await httpClient.PostAsync(zohoMailUrlOauthV2, formDataContent))
                        {

                            string apiResponse = await response.Content.ReadAsStringAsync();
                            received = JsonConvert.DeserializeObject<OauthZohoResponse>(apiResponse);
                            //refreshtoken = received.refresh_token;
                        }
                    }
                    //else
                    //{
                    //    received = await OauthRefreshToken(httpClient, received);
                    //}

                }
                httpClient.Dispose();
            }
            return received;
        }

        private async Task<EmailContent> GetEmailContent(string messageID, OauthZohoResponse receivied)
        {
            //TODO FILTER GET TASK DETAIL
            var data = new EmailContent();
            var url = "https://mail.zoho.com/api/accounts/6001458000000008002/messages/" + messageID + "/originalmessage";

            using (var requestMessage =
                     new HttpRequestMessage(HttpMethod.Get, url))
            {
                // TODO: CHANGES ACCESS_TOKEN
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", receivied.access_token);
                //Uri.EscapeUriString(requestMessage.RequestUri);
                var client = _clientFactory.CreateClient();

                var res = await client.SendAsync(requestMessage);
                if (res.IsSuccessStatusCode)
                {
                    string mailContentResponse = await res.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<EmailContent>(mailContentResponse);
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        receivied = await OauthRefreshToken(httpClient, receivied);
                        if (receivied.access_token == null) return null;
                        return await GetEmailContent(messageID, receivied);
                    }
                }

            }
            return data;
        }
        private async Task<List<MailData>> GetAllEmail(OauthZohoResponse received)
        {
            int index = 1;
            int defaultLimited = 200;
            bool isReaded = false;


            List<MailData> mails = new List<MailData>();
            do
            {
                string UrlWithParams;            
                UrlWithParams = zohoMailUrlGetMailFolder + "&sortBy=date" + "&limit=" + defaultLimited + "&start=" + index;
                var requestMessage =
                         new HttpRequestMessage
                         {
                             Method = HttpMethod.Get,
                             RequestUri = new Uri(UrlWithParams)
                         };

                // TODO: CHANGES ACCESS_TOKEN
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", received.access_token);

                var client = _clientFactory.CreateClient();

                var res = await client.SendAsync(requestMessage);
                if (res.IsSuccessStatusCode)
                {
                    string mailViewResponse = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Root>(mailViewResponse);

                    if (data.data.Count == 0)
                    {
                        break;
                    }

                    mails.AddRange(data.data);
                    index = mails.Count;
                    for (int i = 0; i < data.data.Count; i++)
                    {

                        isReaded = _context.EmailReader.Any(e => e.messageId == data.data[i].messageId);
                        if (isReaded)
                        {
                            mails.RemoveRange(i, data.data.Count - i);
                            break;
                        }
                    }

                }
     
            }
            while (isReaded == false);

            return mails;
        }
        private async Task<List<MailData>> GetEmail(OauthZohoResponse received)
        {
            int index = 1;
            int defaultLimited = 200;
            bool isReaded = false;


            List<MailData> mails = new List<MailData>();
            do
            {
                //var form = new Dictionary<string, string>
                //{
                //    {"folderId", "6001458000000962001"},
                //};
                //MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                //foreach (var keyValuePair in form)
                //{
                //    formDataContent.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                //}

                string UrlWithParams;
                //if (limit >= defaultLimited)
                //{
                //    UrlWithParams = zohoMailUrlGetMailFolder  + "&sortBy=date"+ "&limit=" + defaultLimited + "&start=" + index;

                //}
                //else if (limit > 0)
                //{
                //    UrlWithParams = zohoMailUrlGetMailFolder + "&sortBy=date" + "&limit=" + limit + "&start=" + index ;

                //}
                //else
                //{
                    UrlWithParams = zohoMailUrlGetMailFolder + "&sortBy=date" + "&limit=" + defaultLimited + "&start=" + index;

                //}


                var requestMessage =
                         new HttpRequestMessage
                         {
                             Method = HttpMethod.Get,
                             RequestUri = new Uri(UrlWithParams)
                         };
                

                    // TODO: CHANGES ACCESS_TOKEN
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", received.access_token);
                   
                    var client = _clientFactory.CreateClient();

                    var res = await client.SendAsync(requestMessage);
                    if (res.IsSuccessStatusCode)
                    {
                        string mailViewResponse = await res.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<Root>(mailViewResponse);

                    if (data.data.Count == 0)
                    {
                        break;
                    }
                       
                        //mailData.AddRange(data.data);
                        mails.AddRange(data.data);
                        index = mails.Count;
                        for (int i = 0; i < data.data.Count; i++)
                        {
                            isReaded = _context.EmailReader.Any(e => e.messageId == data.data[i].messageId);
                            if (isReaded)
                            {
                                //data.data.RemoveRange(i, data.data.Count - i);
                                mails.RemoveRange(i, data.data.Count - i);
                                break;
                            }
                        }

                    }
                    //else 
                    //{
                    //    //if (isErrors) break;
                    //    //isErrors = true;
                    //    //await OauthRefreshToken(_clientFactory.CreateClient(), received);
                      
                    //    //await GetEmail(received);
                    //    ////await GetEmail(limit, received);

                    //}
                
            }
            while (isReaded == false);


            return mails;
        }
        private async Task<OauthZohoResponse> OauthRefreshToken(HttpClient httpClient, OauthZohoResponse received)
        {
            var form = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"client_id", "1000.16VT5D5DQ71NQWFD3LNR6DDUD0F8XS"},
                {"client_secret", "8cdcf3ddda5934c3c8510dc9a187741f58d96c4f0c"},
                {"refresh_token",  received.refresh_token },

            };
            using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
            {
                foreach (var keyValuePair in form)
                {
                    formDataContent.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                }
                using (var viewemail = await httpClient.PostAsync(zohoMailUrlOauthV2, formDataContent))
                {
                    string apiResponse = await viewemail.Content.ReadAsStringAsync();
                    received = JsonConvert.DeserializeObject<OauthZohoResponse>(apiResponse);

                    return received;
                }
            }

        }
        // GET: DataDauVaos/Details/5
       
        public async Task<IActionResult> Details(int? id, int? page = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataDauVao = await _context.DataDauVao
                .FirstOrDefaultAsync(m => m.Id == id);
           
            if (dataDauVao == null)
            {
                return NotFound();
            }
            var result = _context.EmailGroup.Where(e => e.ODParrent == dataDauVao.Id && e.name == dataDauVao.Name && e.status2 != "-1").Select(e => new EmailGroup
            {
                Id = e.Id,
                ODNumber = e.ODNumber,
                address = e.address,
                name = e.name == null ? _context.EmailReader.FirstOrDefault(em => em.ODNumber == e.ODNumber && e.status2 == "2").name : e.name,
                shippto = e.shippto,
                shipped = e.shipped,
                status = e.status,
                fromAddress = e.fromAddress,
                toAddress = e.toAddress,
                tracking = e.tracking,
                orderTotal = e.orderTotal,
                estimatime = e.estimatime,
                MessageId = e.MessageId,
                EmailReaderId = e.EmailReaderId,
                receivedTime = e.receivedTime,
                
            }).ToList();
            ViewData["ListODnumber"] = result.Select(e => new OdNumberStatus
            {
                OdNumber = e.ODNumber,
                Shipped = e.shipped,
                Id = e.Id,
            }).ToList();
            ViewData["ListTrack"] = result.Select(e => new TrackingViewModel
            {
                Track = e.tracking,
                Shipped = e.shipped,
                Id = e.Id,
            }).ToList();

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
            // End
            //var data = paginationEmailData(start, limit, result);

            ViewData["emailList"] = result.Skip(start).Take(limit).ToList();
            return View(dataDauVao);
        }
        public IEnumerable<EmailReaderViewModel> getDataAll()
        {
            return data;
        }
        public int totalData(List<EmailReaderViewModel> listData)
        {
            return listData.Count;
        }
        public int numberPage(float totalData, int limit)
        {
            float numberpage = totalData / limit;
            return (int)Math.Ceiling(numberpage);
        }
        public IEnumerable<EmailReaderViewModel> paginationData(int start, int limit, List<EmailReaderViewModel> listData)
        {
            var data = (from s in listData select s);
            var dataAll = data.Skip(start).Take(limit);
            return dataAll.ToList();
        }
        public IEnumerable<TotalItem> paginationItemData(int start, int limit, List<TotalItem> listData)
        {
            var data = (from s in listData select s);
            var dataAll = data.Skip(start).Take(limit);
            return dataAll.ToList();
        }
        public IEnumerable<EmailReader> paginationEmailData(int start, int limit, List<EmailReader> listData)
        {
            var data = (from s in listData select s);
            var dataAll = data.Skip(start).Take(limit);
            return dataAll.ToList();
        }
       
        // GET: DataDauVaos/Create
        [HttpGet]
        public async Task<IActionResult> Index(string Day, string Month, string Year, string Name, int? page = 0)
        {
            int dayFn;
            int monthFn;
            int yearFn;

            var joinning = (from d in _context.DataDauVao
                                //join email in _context.EmailReader on d.ODNumber equals email.ODNumber
                            orderby d.NgayGui descending
                            select new EmailReaderViewModel
                            {
                                Name = d.Name,
                                Address = d.Adress,
                                CanMua = d.CanMua,
                                DaMua = d.DaMua,
                                Debt = d.Debt,
                                GhiChu = d.GhiChu,
                                GiaSale = d.GiaSale,
                                GiaUSD = d.GiaUSD,
                                ItemInTrack = d.ItemInTrack,
                                MaSP = d.MaSP,
                                Mau = d.Mau,
                                NgayGui = d.NgayGui,
                                LinkTrack = d.LinkTrack,
                                ODNumber = d.ODNumber,
                                LinkSanPham = d.LinkSanPham,
                                //Name = email.name,
                                //Shippto = email.shippto,
                                Payment = d.Payment,
                                Rate = d.Rate,
                                //Status = String.IsNullOrEmpty(email.status) ? "check" : email.status,
                                TongUSD = d.TongUSD,
                                TongVND = d.TongVND,
                                ShipOrTax = d.ShipOrTax,
                                Size = d.Size,
                                Id = d.Id
                            }).ToList();

            if (!String.IsNullOrEmpty(Day))
            {
                dayFn = Int32.Parse(Day);
                joinning = joinning.Where(x =>
                {
                    return DateTime.ParseExact(x.NgayGui, "dd/MM/yyyy", CultureInfo.InvariantCulture).Day == dayFn;

                }).ToList();
            }
            if (!String.IsNullOrEmpty(Month))
            {
                monthFn = Int32.Parse(Month);

                joinning = joinning.Where(x => DateTime.ParseExact(x.NgayGui, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month == monthFn).ToList();
            }
            if (!String.IsNullOrEmpty(Year))
            {
                yearFn = Int32.Parse(Year);
                joinning = joinning.Where(x => DateTime.ParseExact(x.NgayGui, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year == yearFn).ToList();
            }
            if (!String.IsNullOrEmpty(Name))
            {
                joinning = joinning.Where(x => x.Name.ToLower().Contains(Name)).ToList();
            }
            //Order by Datetime
            // End
            // Pagination
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

            int total = totalData(joinning);

            ViewBag.totalData = total;

            ViewBag.numberPage = numberPage(total, limit);
            // End
            var data = paginationData(start, limit, joinning);
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        // POST: DataDauVaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgayGui,MaSP,LinkSanPham,Mau,Size,CanMua,DaMua,GiaUSD,GiaSale,ShipOrTax,TongUSD,Rate,TongVND,GhiChu,ODNumber,ItemInTrack,LinkTrack,Payment,Debt,Adress,Name, tyGiaMua, tyGiaBan")] DataDauVao dataDauVao)
        {
            if (ModelState.IsValid)
            {
                var dateStr = dataDauVao.NgayGui.Replace("-", "/");
                DateTime dt = DateTime.ParseExact(dateStr, "yyyy/mm/dd", CultureInfo.InvariantCulture);
               
                var unixTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                dataDauVao.NgayGui = dt.ToString("dd/mm/yyyy");
                var userId = User.FindFirstValue(ClaimTypes.Name);
                dataDauVao.Name = userId;
                dataDauVao.CreateDate = unixTimestamp;
                dataDauVao.CreateDateFD = DateTime.Now;
                _context.Add(dataDauVao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataDauVao);
        }

        // GET: DataDauVaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataDauVao = await _context.DataDauVao.FindAsync(id);
            if (dataDauVao == null)
            {
                return NotFound();
            }
            return View(dataDauVao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmail(int id, EmailGroup email)
        {
            if (id != email.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    (from e in _context.EmailGroup
                     where e.Id == email.Id
                     select e).ToList().ForEach(x => x.shipped = email.shipped);

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                   

                    if (!_context.EmailGroup.Any(e => e.Id == email.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("EmailReaderDetails", new { id = email.Id });
            }
            return View(email);
        }
        // POST: DataDauVaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgayGui,MaSP,LinkSanPham,Mau,Size,CanMua,DaMua,GiaUSD,GiaSale,ShipOrTax,TongUSD,Rate,TongVND,GhiChu,ODNumber,ItemInTrack,LinkTrack,Payment,Debt,Adress,Name, stopOrder, tyGiaBan, tyGiaMua")] DataDauVao dataDauVao)
        {
            if (id != dataDauVao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //var dateStr = dataDauVao.NgayGui.Replace("-", "/");
                    //DateTime dt = DateTime.ParseExact(dateStr, "yyyy/mm/dd", CultureInfo.InvariantCulture);
                    //dataDauVao.NgayGui = dt.ToString("dd/mm/yyyy");
                    _context.Update(dataDauVao);
                    _context.SaveChanges();
                    _context.DataDauVao.ToList().ForEach(d => {
                        // profit
                        var total = 0D;
                        var totalPrevios = 0D;
                        float offset = d.tyGiaBan - d.tyGiaMua;                  
                        float a;
                        int c;
                        if (d.TongUSD != null)
                        {
                            var b = float.TryParse(d.TongUSD, out a);
                            var e = Int32.TryParse(d.DaMua, out c);
                            if (b && e)
                            {
                                total = offset * a * c;
                            }
                        }
                       

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
                        //data.NetProfit = totalNet;

                        if (_context.DataProfitOrder.Any(dt => dt.OrderId == d.Id))
                        {
                            var dprofit = _context.DataProfitOrder.Single(dp => dp.OrderId == d.Id);
                            dprofit.TotalProfit = data.TotalProfit;
                            dprofit.ODnumber = d.ODNumber;
                            dprofit.Name = d.Name;
                            dprofit.NgayGui = d.NgayGui;
                            dprofit.GiaUSD = d.GiaUSD;
                            dprofit.DaMua = d.DaMua;
                            dprofit.GiaSale = d.GiaSale;
                            //dprofit.SiteName = "Sephora";
                            dprofit.TotalProfit = total;
                            dprofit.tyGiaBan = d.tyGiaBan;
                            dprofit.tyGiaMua = d.tyGiaMua;
                            dprofit.DaMua = d.DaMua;
                            dprofit.CanMua = d.CanMua;
                            dprofit.orderStop = d.stopOrder;
                            //dprofit.OrderId = d.Id;
                            //dprofit.NetProfit = totalNet;

                            _context.DataProfitOrder.Update(dprofit);

                        }
                        //else _context.DataProfitOrder.Add(data);

                    });
                    var dataDas = _context.DashboardData.OrderByDescending(d => d.Id).FirstOrDefault();
                    dataDas.TotalProfit = _context.DataProfitOrder.Sum(ord => ord.TotalProfit);
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataDauVaoExists(dataDauVao.Id))
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
            return View(dataDauVao);
        }

        // GET: DataDauVaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataDauVao = await _context.DataDauVao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataDauVao == null)
            {
                return NotFound();
            }

            return View(dataDauVao);
        }

        // POST: DataDauVaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataDauVao = await _context.DataDauVao.FindAsync(id);
            _context.DataDauVao.Remove(dataDauVao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Ajax_delete_data(int id)
        {
            var dataDauVao = _context.DataDauVao.FirstOrDefault(x => x.Id == id);
            _context.DataDauVao.Remove(dataDauVao);
            _context.SaveChangesAsync();
            Response.WriteAsync("1");
            return null;
        }
        private bool DataDauVaoExists(int id)
        {
            return _context.DataDauVao.Any(e => e.Id == id);
        }
       
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile postedFile)
        {

            if (postedFile == null || postedFile.Length <= 0)
            {
                return RedirectToAction(nameof(Index));

            }

            if (!Path.GetExtension(postedFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction(nameof(Index));

            }

            //var list = new List<UserInfo>();

            using (MemoryStream stream = new MemoryStream())
            {
                await postedFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        int i = 1;
                        DataDauVao dataDauVao = new DataDauVao();
                        Console.WriteLine(" worksheet.Cells[row, i]", worksheet.Cells[row, i].Value);
                        if (worksheet.Cells[row, i].Value == null)
                        {
                            break;
                        }
                        DateTime ngayGuiFd;
                        try
                        {
                            var dateString = worksheet.Cells[row, i].Value?.ToString().Trim();

                            if (dateString.Length > 9)
                            {
                                dateString = dateString.Substring(0, 10);
                            }
                            else if (dateString.Length == 9)
                            {
                                dateString = dateString.Substring(0, 9);
                            }
                            else
                            {
                                continue;
                            }
                            ngayGuiFd = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            continue;
                        }
                        dataDauVao.NgayGui = ngayGuiFd.ToString("dd/MM/yyyy");
                        dataDauVao.MaSP = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.LinkSanPham = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.Mau = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.Size = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.CanMua = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.DaMua = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.GiaUSD = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.GiaSale = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.ShipOrTax = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.TongUSD = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.Rate = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.TongVND = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.GhiChu = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.ODNumber = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.ItemInTrack = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.LinkTrack = worksheet.Cells[row, ++i].Value?.ToString().Trim(); ;
                        dataDauVao.Payment = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.Debt = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.Adress = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.Name = worksheet.Cells[row, ++i].Value?.ToString().Trim();
                        dataDauVao.Status = "check";
                        DateTime today = DateTime.Now;
                        long unixTimestamp = (long)(today.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                        dataDauVao.CreateDate = unixTimestamp;
                        _context.Add(dataDauVao);
                        //list.Add(new UserInfo
                        //{
                        //    UserName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                        //    Age = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                        //});
                    }
                    await _context.SaveChangesAsync();

                }
            }

            // add list to db ..  
            // here just read and return  

            return RedirectToAction(nameof(Index));
        }
    }
}
