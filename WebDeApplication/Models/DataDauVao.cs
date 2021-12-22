﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models
{
    public class DataDauVao
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Ngày gửi")]
        public string NgayGui { get; set; }
        [Display(Name = "Mã SP")]
        public string MaSP { get; set; }
        [Display(Name = "Link SP")]
        public string LinkSanPham { get; set; }
        [Display(Name = "Màu")]

        public string Mau { get; set; }
        //[Display(Name = "Size")]

        public string Size { get; set; }
        [Display(Name = "Cần mua")]
        [Required]

        public string CanMua { get; set; }
        [Display(Name = "Đã mua")]

        public string DaMua { get; set; }
        [Display(Name = "Gía USD")]

        public string GiaUSD { get; set; }
        [Display(Name = "Gía sale")]

        public string GiaSale { get; set; }
        //[Display(Name = "Ship")]

        public string ShipOrTax { get; set; }
        [Display(Name = "Tổng USD")]

        public string TongUSD { get; set; }
        [Display(Name = "Đánh giá")]

        public string Rate { get; set; }
        [Display(Name = "Tổng VNĐ")]

        public string TongVND { get; set; }
        [Display(Name = "Ghi chú")]

        public string GhiChu { get; set; }
        [Display(Name = "Order number")]
        [Required]

        public string ODNumber { get; set; }

        public string ItemInTrack { get; set; }
        public string LinkTrack { get; set; }
        public string Payment { get; set; }
        [Display(Name = "Nợ")]

        public string Debt { get; set; }
        [Display(Name = "Địa chỉ")]

        public string Adress { get; set; }
        [Display(Name = "Trạng thái")]

        public string Status { get; set; }
        public Boolean isChecked { get; set; }
        public Boolean stopOrder { get; set; }
        [Display(Name = "Ngày Tạo")]

        public long CreateDate { get; set; }
        public DateTime CreateDateFD { get; set; }

        [Display(Name = "Tỷ giá mua")]
        [Required]

        public int tyGiaMua { get; set; }
        [Display(Name = "Tỷ giá bán")]
        [Required]

        public int tyGiaBan { get; set; }


    }
}
