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
        public string DateUpdate { get; set; }
        public double TotalProfit { get; set; }
        public double NetProfit { get; set; }
        public int tyGiaMua { get; set; }
        public int tyGiaBan { get; set; }
        public Boolean orderStop { get; set; }
        public string GiaSale { get; set; }
        public string TongUSD { get; set; }
        public string GiaUSD { get; set; }

    }
}
