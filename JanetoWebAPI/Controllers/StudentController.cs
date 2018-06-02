using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using JanetoWebAPI.ViewModels;
using ApiModels;

namespace JanetoWebAPI.Controllers
{
    public class StudentController : ApiController
    {
        private ApiDBContext _db;
        public StudentController()
        {
            this._db = new ApiDBContext();
        }
        
        [HttpPost]
        public IHttpActionResult TaoSV(TaoSV SV)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            if (string.IsNullOrEmpty(SV.StudentName))
            {
                error.Add("Họ tên là bắt buộc");
            }
            if (string.IsNullOrEmpty(SV.StudentAddress))
            {
                error.Add("Địa chỉ là bắt buộc");
            }
            if (error.Errors.Count == 0)
            {
                Student sv = new Student();
                sv.TenSV = SV.StudentName;
                sv.DiaChi = SV.StudentAddress;
                sv.NTNS = SV.StudentBirth;
                sv.MaSV = SV.StudentId;
                sv.Lop= _db.Lop.FirstOrDefault(x => x.Id == SV.Class_Id);
                sv = this._db.SinhVien.Add(sv);
                this._db.SaveChanges();
                StudentModel viewModel = new StudentModel(sv);
                httpActionResult = Ok(viewModel);
            }
            else
            {
                httpActionResult = Ok(error);
            }
            return httpActionResult;
        }
        
        [HttpPut]
        public IHttpActionResult CapNhatSV(CapNhatSV SV)
        {
            IHttpActionResult httpActionresult;
            ErrorModel error = new ErrorModel();
            Student sv = this._db.SinhVien.FirstOrDefault(x => x.Id == SV.Id);
            if (sv == null)
            {
                error.Add("Không tìm thấy sinh viên");
                httpActionresult = Ok(error);
            }
            else
            {
                sv.TenSV = SV.StudentName ?? SV.StudentName;
                sv.MaSV = SV.StudentId;
                sv.NTNS = SV.StudentBirth;
                sv.DiaChi = SV.StudentAddress ?? SV.StudentAddress;
                //sv.Class.Id = model.ClassId;
                this._db.Entry(sv).State = System.Data.Entity.EntityState.Modified;
                this._db.SaveChanges();
                httpActionresult = Ok(new StudentModel(sv));
            }
            return httpActionresult;
        }
        
        [HttpGet]
        public IHttpActionResult DanhSachSV()
        {
            var lstStudent = this._db.SinhVien.Select(x => new StudentModel()
            {
                Id = x.Id,
                MaSV = x.MaSV,
                TenSV = x.TenSV,
                NTNS = x.NTNS,
                DiaChi = x.DiaChi,
                MaLop = x.Lop.Id
            });
            return Ok(lstStudent);
        }
        
        [HttpGet]
        public IHttpActionResult TimSVTheoMaSV(int maSV)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            var sv = _db.SinhVien.FirstOrDefault(x => x.MaSV == maSV);
            if (sv == null)
            {
                error.Add("Không tìm thấy sinh viên");
                httpActionResult = Ok(error);
            }
            else
            {
                httpActionResult = Ok(new StudentModel(sv));
            }
            return httpActionResult;
        }
        
        [HttpDelete]
        public IHttpActionResult XoaSV(int maSV)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            var sv = _db.SinhVien.FirstOrDefault(x => x.MaSV == maSV);
            if (sv == null)
            {
                error.Add("Mã sinh viên không tồn tạo");
                httpActionResult = Ok(error);
            }
            else
            {
                this._db.SinhVien.Remove(sv);
                this._db.SaveChanges();
                httpActionResult = Ok("Đã xóa sinh viên " + sv.MaSV);
            }
            return httpActionResult;
        }

    }
}