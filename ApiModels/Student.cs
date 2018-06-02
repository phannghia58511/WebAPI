using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels
{
    public class Student : Entity<int>
    {
        public int MaSV { get; set; }
        public string TenSV { get; set; }
        public DateTime NTNS { get; set; }
        public string DiaChi { get; set; }
        public virtual Class Lop { get; set; }
    }
}
