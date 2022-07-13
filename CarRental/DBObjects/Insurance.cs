using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.DBObjects
{
    [Table("INSURANCE")]
    public class Insurance
    {
        [Column("ID")]
        public int InsuranceId { get; set; }

        [Column("INSURANCE_NAME")]
        public string InsuranceName { get; set; }
        [Column("INSURANCE_PRICE")]
        public double InsurancePrice { get; set; }

        public override string ToString()
        {
            return InsuranceName + ", Cena: " + InsurancePrice;
        }
    }
}
