using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DBObjects;
using CarRental.DBTools;
using CarRental.Utils;
using CarRental.Containers;
using System.Diagnostics;
namespace CarRental.DBTools
{
    /// <summary>
    /// Database util class
    /// </summary>
    public class DbUtils
    {
        CarRentalDbContext dbContext;
        /// <summary>
        /// Constructor of DbUtils class
        /// </summary>
        public DbUtils(CarRentalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Returns list of Rent Containers
        /// </summary>
        public List<RentContainer> GetRentContainers()
        {
            List<RentContainer> containers = new List<RentContainer>();
            FillContainers(containers);
            return containers;
        }
        /// <summary>
        /// Returns list of Car Containers
        /// </summary>
        public List<CarContainer> GetCarContainers()
        {
            List<CarContainer> carContainers = new List<CarContainer>();
            List<Car> cars = dbContext.Cars.ToList();
            foreach (var c in cars)
            {
                carContainers.Add(GetCarContainer(c.CarId));
            }

            return carContainers;
        }


        private void FillContainers(List<RentContainer> containers)
        {
            List<Rent> rents = dbContext.Rents.ToList();
            foreach(var rent in rents)
            {
                RentContainer rentContainer = new RentContainer();
                rentContainer.Rent = rent;
                rentContainer.Client = dbContext.Clients.Where(e => e.ClientId == rent.ClientId).Single();
                rentContainer.Insurance = dbContext.Insurances.Where(e => e.InsuranceId == rent.InsuranceId).Single();
                rentContainer.LocalizationRent = dbContext.Localizations.Where(e => e.LocalizationId == rent.RentLocalizationId).Single();
                rentContainer.LocalizationReturn = dbContext.Localizations.Where(e => e.LocalizationId == rent.ReturnLocalizationId).Single();
                rentContainer.CarContainer = GetCarContainer(rent.CarId);
                rentContainer.Price = CarRentalUtils.GetRentPrice(rentContainer.CarContainer.Car.CarPrice,
                    rentContainer.Insurance.InsurancePrice,
                    rentContainer.Rent.RentDays);
                containers.Add(rentContainer);
            }
        }

        private CarContainer GetCarContainer(int carId)
        {
            CarContainer carContainer = new CarContainer();
            carContainer.Car = dbContext.Cars.Where(e => e.CarId == carId).Single();
            carContainer.CarType = dbContext.CarTypes.Where(e => e.CarTypeId == carContainer.Car.CarType).Single();
            carContainer.DriveType = dbContext.DriveTypes.Where(e => e.DriveTypeId == carContainer.Car.DriveType).Single();
            carContainer.FuelType = dbContext.FuelTypes.Where(e => e.FuelId == carContainer.Car.FuelType).Single();
            return carContainer; 
        }


    }
}
