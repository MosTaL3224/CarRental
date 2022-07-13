using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.DBObjects
{
    [Table("FUEL_TYPE")]
    public class FuelType
    {
        [Column("ID")]
        public int FuelId { get; set; }

        [Column("FUEL")]
        public string FuelName { get; set; }

        public override string ToString()
        {
            return FuelName;
        }
    }
}
