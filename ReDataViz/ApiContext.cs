using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReDataViz
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) 
            : base(options){}

        public DbSet<Models.DtvizMessage> DtvizMessages { get; set; }
        public DbSet<Models.User> Users { get; set; }

    }
}
