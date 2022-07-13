using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DBObjects;
namespace CarRental.Containers
{
    /// <summary>
    /// Agregate class RentContainer that contains database objects and CarContainer
    /// </summary>
    public class RentContainer
    {
        public Rent Rent { get; set; }
        public CarContainer CarContainer{ get; set; }
        public Client Client { get; set; }
        public Insurance Insurance { get; set; }
        public Localization LocalizationRent { get; set; }
        public Localization LocalizationReturn { get; set; }
        public double Price { get; set; }

    }
}
