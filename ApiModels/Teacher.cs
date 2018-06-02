using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels
{
    public class Teacher:Entity<int>
    {
        public string MaGV { get; set; }
        public string TenGV { get; set; }             
        public virtual ICollection<Class> ChuNhiem { get; set; }
        public virtual ICollection<Class> Lop { get; set; }
    }
}
