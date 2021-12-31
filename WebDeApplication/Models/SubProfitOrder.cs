using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class SubProfitOrder
    {   
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ODnumber { get; set; }
        public string Name { get; set; }
        public DateTime DateUpdate { get; set; }
        public double TotalProfit { get; set; }
        public double NetProfit { get; set; }
        public double tyGiaMua { get; set; }
        public double tyGiaBan { get; set; }
        public Boolean orderStop { get; set; }
        public string GiaSale { get; set; }
        public string TongUSD { get; set; }
        public string GiaUSD { get; set; }
        public int Payed { get; set; }
    }
}
