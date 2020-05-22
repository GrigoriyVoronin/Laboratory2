using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExcelToDB
{
    public class GroupContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}
