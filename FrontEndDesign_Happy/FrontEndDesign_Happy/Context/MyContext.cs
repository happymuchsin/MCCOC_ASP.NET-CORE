using FrontEndDesign_Happy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
    }
}
