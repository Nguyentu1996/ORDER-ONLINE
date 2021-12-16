using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class DataProfitOrder
    {   
        [Key]
        public int Id { get; set; }
        public string ODnumber { get; set; }
        public string Name { get; set; }
        public string NgayGui { get; set; }
        public float TotalProfit { get; set; }
        public string SiteName { get; set; }
        public int tyGiaMua { get; set; }
        public int tyGiaBan { get; set; }
        public string CanMua { get; set; }
        public string DaMua { get; set; }
        public string GiaUSD { get; set; }
        public Boolean orderStop { get; set; }

        public string GiaSale { get; set; }
        public string TongUSD { get; set; }
    }
}
