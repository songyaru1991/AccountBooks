using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AccountBooks.Models;

namespace AccountBooks.Models
{
    public class DataContext:DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultSchema("SMT");
        //}
        public DataContext()
            : base("DataContext")
        {
        }


        public DbSet<ChargeModels> Charge { get; set; }//此属性可以操作ChargeModels实体类生成的表
    }
}