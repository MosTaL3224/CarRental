using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CarRental.DBObjects;
namespace CarRental.DBTools
{
    public class CarRentalDbContext:DbContext
    {
        /// <summary>
        /// Return DbSet of table CAR from database.
        /// </summary>
        public DbSet<Car> Cars { get; set; }
        /// <summary>
        /// Return DbSet of table CAR_TYPE from database.
        /// </summary>
        public DbSet<CarType> CarTypes { get; set; }
        /// <summary>
        /// Return DbSet of table CLIENT from database.
        /// </summary>
        public DbSet<Client> Clients { get; set; }
        /// <summary>
        /// Return DbSet of table DRIVE_TYPE from database.
        /// </summary>
        public DbSet<DriveType> DriveTypes { get; set; }
        /// <summary>
        /// Return DbSet of table FUEL_TYPE from database.
        /// </summary>
        public DbSet<FuelType> FuelTypes { get; set; }
        /// <summary>
        /// Return DbSet of table INSURANCE from database.
        /// </summary>
        public DbSet<Insurance> Insurances { get; set; }
        /// <summary>
        /// Return DbSet of table LOCALIZATION from database.
        /// </summary>
        public DbSet<Localization> Localizations { get; set; }
        /// <summary>
        /// Return DbSet of table RENTS from database.
        /// </summary>
        public DbSet<Rent> Rents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=rent_car_db.db", option => {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("CAR").HasKey(e => e.CarId);
            modelBuilder.Entity<CarType>().ToTable("CAR_TYPE").HasKey(e => e.CarTypeId);
            modelBuilder.Entity<Client>().ToTable("CLIENT").HasKey(e => e.ClientId);
            modelBuilder.Entity<DriveType>().ToTable("DRIVE_TYPE").HasKey(e => e.DriveTypeId);
            modelBuilder.Entity<FuelType>().ToTable("FUEL_TYPE").HasKey(e => e.FuelId);
            modelBuilder.Entity<Insurance>().ToTable("INSURANCE").HasKey(e => e.InsuranceId);
            modelBuilder.Entity<Localization>().ToTable("LOCALIZATION").HasKey(e => e.LocalizationId);
            modelBuilder.Entity<Rent>().ToTable("RENTS").HasKey(e => e.RentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
