using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels
{
    public class ApiDBContext:DbContext
    {
        public ApiDBContext() : base("ApiConnection")
        {

        }
        static ApiDBContext()
        {
            Database.SetInitializer<ApiDBContext>(new IdentityDBInit());
        }
        public static ApiDBContext Create()
        {
            return new ApiDBContext();
        }
        public DbSet<Class> Lop { get; set; }
        public DbSet<Student> SinhVien { get; set; }
        public DbSet<Teacher> GiaoVien { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }

    internal class IdentityDBInit : DropCreateDatabaseIfModelChanges<ApiDBContext>
    {
        public void Seed(ApiDBContext context)
        {
            PerformInit();
            base.Seed(context);
        }

        private void PerformInit()
        {
        }
    }
}
