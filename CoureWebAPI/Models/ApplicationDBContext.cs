using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoureWebAPI.Models
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<CountryDetail> CountryDetails { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}
