using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Extensions
{

    // extension methods for DataContext
    public static class DataContextExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            // AllMigrationsApplied is custom extension method - look into DbContextExtensions
            if (context.AllMigrationsApplied())
            {
                // if table is empty, add some initial records
            //    if (!context.Persons.Any())
            //    {
            //        context.Persons.Add(entity: new Person() { FirstName = "Andres", LastName = "Käver" });
            //        context.Persons.Add(entity: new Person() { FirstName = "Mait", LastName = "Poska" });
            //        context.SaveChanges();
            //    }
            }
        }
    }



}
