using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Labo8.Models;

namespace Labo8.Data
{
    public class Labo8Context : DbContext
    {
        public Labo8Context (DbContextOptions<Labo8Context> options)
            : base(options)
        {
        }

        public DbSet<Labo8.Models.Item> Item { get; set; } = default!;
    }
}
