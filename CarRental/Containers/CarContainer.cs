using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DBObjects;
namespace CarRental.Containers
{
    /// <summary>
    /// Agregate class CarContainer that contains database objects : Car, CarType, DriverType and FuelType
    /// </summary>
    public class CarContainer
    {
        public Car Car { get; set; }
        public CarType CarType { get; set; }
        public DriveType DriveType { get; set; }
        public FuelType FuelType { get; set; }
    }
}
