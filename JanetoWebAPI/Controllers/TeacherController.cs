using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JanetoWebAPI.ViewModels;
using ApiModels;

namespace JanetoWebAPI.Controllers
{
    public class TeacherController : ApiController
    {
        private ApiDBContext _db;
        public TeacherController()
        {
            this._db = new ApiDBContext();
        }
        
        [HttpPost]
        public IHttpActionResult TaoGV(TaoGV model)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            if (string.IsNullOrEmpty(model.MaGV))
            {
                error.Add("Mã giáo viên không được để trống");
            }

            if (string.IsNullOrEmpty(model.TenGV))
            {
                error.Add("Họ tên giáo viên không được để trống");
            }

            if (error.Errors.Count == 0)
            {
                Teacher teacher = new Teacher();
                teacher.MaGV = model.MaGV;
                teacher.TenGV = model.TenGV;              
                teacher = _db.GiaoVien.Add(teacher);

                _db.SaveChanges();
                TeacherModel vienModel = new TeacherModel(teacher);
                httpActionResult = Ok(vienModel);
            }
            else
            {
                httpActionResult = Ok(error);
            }

            return httpActionResult;
        }
        
        [HttpGet]
        public IHttpActionResult GetAllGV()
        {
            var list = _db.GiaoVien.Select(x => new TeacherModel
            {
                Id = x.Id,
                MaGV = x.MaGV,
                TenGV = x.TenGV,             
            });
            return Ok(list);
        }
        
        [HttpPut]
        public IHttpActionResult CapNhatGV(CapNhatGV model)
        {
            IHttpActionResult httpActionresult;
            ErrorModel error = new ErrorModel();
            Teacher tc = this._db.GiaoVien.FirstOrDefault(x => x.Id == model.Id);
            if (tc == null)
            {
                error.Add("Không tìm thấy Giáo viên");
                httpActionresult = Ok(error);
            }
            else
            {
                tc.TenGV = model.TenGV ?? model.TenGV;
                tc.MaGV = model.MaGV;              
                this._db.Entry(tc).State = System.Data.Entity.EntityState.Modified;
                this._db.SaveChanges();
                httpActionresult = Ok(new TeacherModel(tc));
            }
            return httpActionresult;
        }
        
        [HttpGet]
        public IHttpActionResult TimGVTheoMaGV(string maGV)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            var tc = _db.GiaoVien.FirstOrDefault(x => x.MaGV == maGV);
            if (tc == null)
            {
                error.Add("Không tìm thấy giáo viên");
                httpActionResult = Ok(error);
            }
            else
            {
                httpActionResult = Ok(new TeacherModel(tc));
            }
            return httpActionResult;
        }
        
        [HttpDelete]
        public IHttpActionResult XoaGV(string maGV)
        {
            IHttpActionResult httpActionResult;
            ErrorModel error = new ErrorModel();
            var tc = _db.GiaoVien.FirstOrDefault(x => x.MaGV == maGV);
            if (tc == null)
            {
                error.Add("Mã giáo viên không tồn tại");
                httpActionResult = Ok(error);
            }
            else
            {
                this._db.GiaoVien.Remove(tc);
                this._db.SaveChanges();
                httpActionResult = Ok("Đã xóa giáo viên " + tc.MaGV);
            }
            return httpActionResult;
        }
    }
}
