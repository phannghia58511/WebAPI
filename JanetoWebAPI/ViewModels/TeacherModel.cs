using ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JanetoWebAPI.ViewModels
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public string MaGV { get; set; }
        public string TenGV { get; set; }
        public string DiaChi { get; set; }
        public TeacherModel()
        {
        }

        public TeacherModel(Teacher GV)
        {
            Id = GV.Id;
            MaGV = GV.MaGV;
            TenGV = GV.TenGV;           
        }
    }
    public class TaoGV
    {
        public string MaGV { get; set; }
        public string TenGV { get; set; }
        public string DiaChi { get; set; }
    }
    public class CapNhatGV : TaoGV
    {
        public int Id { get; set; }
    }
    public class XoaGV : CapNhatGV { }
}