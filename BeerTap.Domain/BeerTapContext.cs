using System.Data.Entity;

namespace BeerTap.Domain
{
    public class BeerTapContext : DbContext
    {
        public BeerTapContext() : base()
        {
            //Database.SetInitializer<BeerTapContext>(new CreateDatabaseIfNotExists<BeerTapContext>());
            Database.SetInitializer<BeerTapContext>(new BeerTapInitializer());
            Database.Connection.ConnectionString =
                "Server=MAN29-W541-W8\\SQL2012;Database=BeerTap;User Id=sa;Password=iQmetrix!;";
        }

        public DbSet<Keg> Kegs { get; set; }
        public DbSet<Office> Offices { get; set; }
    }

    public class BeerTapInitializer : CreateDatabaseIfNotExists<BeerTapContext>
    {
        protected override void Seed(BeerTapContext context)
        {
            context.Offices.Add(new Office("Vancouver"));
            context.Offices.Add(new Office("Regina"));
            context.Offices.Add(new Office("Winnepeg"));
            context.Offices.Add(new Office("Davidson"));

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
