using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class DataProfitOrder
    {   
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ODnumber { get; set; }
        public string Name { get; set; }
        public string NgayGui { get; set; }
        public double TotalProfit { get; set; }
        public string SiteName { get; set; }
        public double tyGiaMua { get; set; }
        public double tyGiaBan { get; set; }
        public string CanMua { get; set; }
        public string DaMua { get; set; }
        public string GiaUSD { get; set; }
        public Boolean orderStop { get; set; }
        public string GiaSale { get; set; }
        public string TongUSD { get; set; }
        public double NetProfit { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
