using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.DBObjects
{
    [Table("LOCALIZATION")]
    public class Localization
    {
        [Column("ID")]
        public int LocalizationId { get; set; }

        [Column("CITY")]
        public string City { get; set; }
        [Column("PROVINCE")]
        public string Province { get; set; }
        [Column("ADDRESS")]
        public string Address { get; set; }

        public override string ToString()
        {
            return City + " woj. " + Province + ", " + Address;
        }
    }
}
