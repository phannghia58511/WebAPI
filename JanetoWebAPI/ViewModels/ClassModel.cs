using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JanetoWebAPI.Models;
using ApiModels;

namespace JanetoWebAPI.ViewModels
{
    public class ClassModel
    {
        public int Id { get; set; }
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public DateTime LichDay { get; set; }
        public int ChuNhiem { get; set; }
        public ClassModel()
        {

        }
        public ClassModel(Class Lop)
        {
            this.Id = Lop.Id;
            this.MaLop = Lop.MaLop;
            this.TenLop = Lop.TenLop;
            this.LichDay = Lop.LichDay;
            this.ChuNhiem = Lop.ChuNhiem.Id;
        }
    }
    public class TaoLop
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public DateTime LichDay { get; set; }
        public int ChuNhiem { get; set; }
    }
    public class CapNhatLop : TaoLop
    {
        public int Id { get; set; }
    }
    public class XoaLop : CapNhatLop { }
}
