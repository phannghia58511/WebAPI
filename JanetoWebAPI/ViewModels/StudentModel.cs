using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JanetoWebAPI.Models;
using ApiModels;
using System.ComponentModel.DataAnnotations;

namespace JanetoWebAPI.ViewModels
{
    public class StudentModel
    {
        public int Id { get; set; }
        public int MaSV { get; set; }
        public string TenSV { get; set; }
        public DateTime NTNS { get; set; }
        public string DiaChi { get; set; }       
        public int MaLop { get; set; }
        public StudentModel()
        {
        }
        public StudentModel(Student SinhVien)
        {
            this.Id = SinhVien.Id;
            this.MaSV = SinhVien.MaSV;
            this.NTNS = SinhVien.NTNS;
            this.DiaChi = SinhVien.DiaChi;
            this.TenSV = SinhVien.TenSV;
            this.MaLop = SinhVien.Lop.Id;
        }
    }
    public class TaoSV
    {
        public int StudentId { get; set; }
        public DateTime StudentBirth { get; set; }
        public string StudentAddress { get; set; }
        public string StudentName { get; set; }
        public int Class_Id { get; set; }
    }
    public class CapNhatSV : TaoSV
    {
        public int Id { get; set; }
    }
    public class XoaSV : CapNhatSV { }
}