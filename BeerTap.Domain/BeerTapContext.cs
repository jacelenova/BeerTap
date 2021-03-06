﻿using System.Collections.Generic;
using System.Data.Entity;

namespace BeerTap.Domain
{
    public class BeerTapContext : DbContext
    {
        public BeerTapContext() : base()
        {
            //Database.SetInitializer<BeerTapContext>(new CreateDatabaseIfNotExists<BeerTapContext>());
            Database.SetInitializer<BeerTapContext>(new BeerTapInitializer());
            //Database.Connection.ConnectionString =
            //    "Server=MAN29-W541-W8\\SQL2012;Database=BeerTap;User Id=sa;Password=iQmetrix!;";
        }

        public DbSet<Keg> Kegs { get; set; }
        public DbSet<Office> Offices { get; set; }
    }

    public class BeerTapInitializer : CreateDatabaseIfNotExists<BeerTapContext>
    {
        protected override void Seed(BeerTapContext context)
        {
            context.Offices.Add(new Office("Vancouver")
            {
                Kegs = new List<Keg>()
                {
                    new Keg("Red Horse"),
                    new Keg("San Mig Light")
                }
            });
            context.Offices.Add(new Office("Regina")
            {
                Kegs = new List<Keg>()
                {
                    new Keg("Red Horse"),
                    new Keg("Smirnoff Mule")
                }
            });
            context.Offices.Add(new Office("Winnepeg")
            {
                Kegs = new List<Keg>()
                {
                    new Keg("Red Horse"),
                    new Keg("Colt 45")
                }
            });
            context.Offices.Add(new Office("Davidson")
            {
                Kegs = new List<Keg>()
                {
                    new Keg("Red Horse"),
                    new Keg("San Mig Light"),
                    new Keg("San Mig Apple")
                }
            });

            //context.Kegs.Add(new Keg() { Name = "Red Horse", OfficeId = 1 });
            //context.Kegs.Add(new Keg() { Name = "San Mig Light", OfficeId = 1 });
            //context.Kegs.Add(new Keg() { Name = "Red Horse", OfficeId = 2 });
            //context.Kegs.Add(new Keg() { Name = "Smirnoff Mule", OfficeId = 2 });
            //context.Kegs.Add(new Keg() { Name = "Red Horse", OfficeId = 3 });
            //context.Kegs.Add(new Keg() { Name = "Colt 45", OfficeId = 3 });
            //context.Kegs.Add(new Keg() { Name = "San Mig Apple", OfficeId = 4 });
            //context.Kegs.Add(new Keg() { Name = "San Mig Light", OfficeId = 4 });
            //context.Kegs.Add(new Keg() { Name = "Red Horse", OfficeId = 4 });

            base.Seed(context);
        }
    }
}
