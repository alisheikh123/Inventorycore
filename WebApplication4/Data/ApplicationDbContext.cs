using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_Systems.Models;

namespace WebApplication4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Inventory_Management_Systems.Models.tblItemcategory> tblItemcategory { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblItem> tblItem { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblAccount> tblAccount { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblAccountHead> tblAccountHead { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblInvoice> tblInvoice { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblInvoiceDetail> tblInvoiceDetail { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblItemUnit> tblItemUnit { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblVoucher> tblVoucher { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblVoucherDetail> tblVoucherDetail { get; set; }
        public DbSet<Inventory_Management_Systems.Models.User> User { get; set; }
        public DbSet<Inventory_Management_Systems.Models.Customer> Customer { get; set; }
        public DbSet<Inventory_Management_Systems.Models.tblCompany> tblCompany { get; set; }
    }
}
