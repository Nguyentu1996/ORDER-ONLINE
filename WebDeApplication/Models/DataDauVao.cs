using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models
{
    public class DataDauVao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NgayGui { get; set; }
        public string MaSP { get; set; }
        public string LinkSanPham { get; set; }
        public string Mau { get; set; }
        public string Size { get; set; }
        public string CanMua { get; set; }
        public string DaMua { get; set; }
        public string GiaUSD { get; set; }
        public string GiaSale { get; set; }
        public string ShipOrTax { get; set; }
        public string TongUSD { get; set; }
        public string Rate { get; set; }
        public string TongVND { get; set; }
        public string GhiChu { get; set; }
        public string ODNumber { get; set; }
        public string ItemInTrack { get; set; }
        public string LinkTrack { get; set; }
        public string Payment { get; set; }
        public string Debt { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }
        public Boolean isChecked { get; set; }
        public Boolean stopOrder { get; set; }
        public long CreateDate { get; set; }
        public int tyGiaMua { get; set; }
        public int tyGiaBan { get; set; }


    }
}
