using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using JanetoWebAPI.ViewModels;
using ApiModels;

namespace JanetoWebAPI.Controllers
{
    public class ClassController : ApiController
    {
        private ApiDBContext _db;
        public ClassController()
        {
            this._db = new ApiDBContext();
        }
        
        [HttpPost]
        public IHttpActionResult TaoLop(TaoLop lop)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            if (string.IsNullOrEmpty(lop.MaLop))
            {
                error.Add("Mã lớp bắt buộc");
            }
            if (string.IsNullOrEmpty(lop.TenLop))
            {
                error.Add("Tên lớp bắt buộc");
            }
            if (_db.GiaoVien.FirstOrDefault(m => m.Id == lop.ChuNhiem) == null)
            {
                error.Add("Giáo viên không tồn tại");
            }
            if (error.Errors.Count == 0)
            {
                Class oneClass = new Class();
                oneClass.TenLop = lop.TenLop;
                oneClass.MaLop = lop.MaLop;
                oneClass.LichDay = lop.LichDay;
                oneClass.ChuNhiem = _db.GiaoVien.FirstOrDefault(m => m.Id == lop.ChuNhiem);
                oneClass = this._db.Lop.Add(oneClass);
                this._db.SaveChanges();
                ClassModel viewModel = new ClassModel(oneClass);
                httpActionResult = Ok(viewModel);
            }
            else
            {
                httpActionResult = Ok(error);
            }
            return httpActionResult;
        }
        
        [HttpPut]
        public IHttpActionResult CapNhatLop(CapNhatLop update)
        {
            IHttpActionResult httpActionresult;
            ErrorModel error = new ErrorModel();
            Class lop = this._db.Lop.FirstOrDefault(x => x.Id == update.Id);
            if (lop == null)
            {
                error.Add("Không tìm thấy lớp");
                httpActionresult = Ok(error);
            }
            else
            {
                lop.MaLop = update.MaLop ?? update.MaLop;
                lop.TenLop = update.TenLop ?? update.TenLop;
                lop.LichDay = update.LichDay;
                lop.ChuNhiem = _db.GiaoVien.FirstOrDefault(x => x.Id == update.ChuNhiem);
                this._db.Entry(lop).State = System.Data.Entity.EntityState.Modified;
                this._db.SaveChanges();
                httpActionresult = Ok(new ClassModel(lop));
            }
            return httpActionresult;
        }
        
        [HttpGet]
        public IHttpActionResult DanhSachLop()
        {
            var lstClass = this._db.Lop.Select(x => new ClassModel()
            {
                MaLop = x.MaLop,
                TenLop = x.TenLop,
                LichDay=x.LichDay,
                ChuNhiem=x.ChuNhiem.Id,
                Id = x.Id
            });
            return Ok(lstClass);
        }
       
        [HttpGet]
        public IHttpActionResult LayLopTheoMaLop(string maLop)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            var lop = _db.Lop.FirstOrDefault(x => x.MaLop == maLop);
            if (lop == null)
            {
                error.Add("Không tìm thấy lớp");
                httpActionResult = Ok(error);
            }
            else
            {
                httpActionResult = Ok(new ClassModel(lop));
            }
            return httpActionResult;
        }
        
        [HttpDelete]
        public IHttpActionResult XoaLop(int id)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            var lop = _db.Lop.FirstOrDefault(x => x.Id == id);
            if (lop == null)
            {
                error.Add("Mã lớp không tồn tạo");
                httpActionResult = Ok(error);
            }
            else
            {
                this._db.Lop.Remove(lop);
                this._db.SaveChanges();
                httpActionResult = Ok("Đã xóa lớp " + lop.MaLop);
            }
            return httpActionResult;
        }
    }
}