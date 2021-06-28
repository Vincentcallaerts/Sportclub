using Microsoft.EntityFrameworkCore;
using Sportclub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportclub.Database
{
    public class SporterDbContext : DbContext
    {
        public SporterDbContext(DbContextOptions<SporterDbContext> options) : base(options)
        {

        }
        public DbSet<Sporter> Sporters { get; set; }
    }
}
