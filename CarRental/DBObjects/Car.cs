using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.DBObjects
{
    [Table("CAR")]
    public class Car
    {
        [Column("ID")]
        public int CarId { get; set; }
        [Column("CAR_NAME")]
        public string CarName { get; set; }
        [Column("CAR_PRICE")]
        public double CarPrice { get; set; }
        [Column("REGISTRATION_NUMBER")]
        public string RegistrationNumber { get; set; }
        [Column("CAR_TYPE")]
        [ForeignKey("CAR_TYPE")]
        public int CarType { get; set; }
        [Column("FUEL_TYPE")]
        [ForeignKey("FUEL_TYPE")]
        public int FuelType { get; set; }
        [Column("DRIVE_TYPE")]
        [ForeignKey("DRIVE_TYPE")]
        public int DriveType { get; set; }

        public override string ToString()
        {
            return CarName + ", Cena: " + CarPrice;
        }
    }
}
