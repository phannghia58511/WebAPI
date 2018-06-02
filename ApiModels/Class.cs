using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels
{
    public class Class:Entity<int>
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public DateTime LichDay { get; set; }
        
        //1 lớp có 1 GV chủ nhiệm.
        [InverseProperty("Homeroom")]
        public virtual Teacher ChuNhiem { get; set; }

        //1 lớp dạy bởi nhiều GV
        public ICollection<Teacher> GiaoVien { get; set; }
        //1 lớp có nhiều SV
        public ICollection<Student> SinhVien { get; set; }
    }
}
