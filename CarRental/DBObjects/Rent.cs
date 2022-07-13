using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.DBObjects
{
    [Table("RENTS")]
    public class Rent
    {
        [Column("ID")]
        public int RentId { get; set; }
        [Column("CLIENT")]
        [ForeignKey("CLIENT")]
        public int ClientId { get; set; }
        [Column("CAR")]
        [ForeignKey("CAR")]
        public int CarId { get; set; }
        [Column("INSURANCE")]
        [ForeignKey("INSURANCE")]
        public int InsuranceId { get; set; }
        [Column("RENT_LOCALIZATION")]
        [ForeignKey("LOCALIZATION")]
        public int RentLocalizationId { get; set; }
        [Column("RETURN_LOCALIZATION")]
        [ForeignKey("LOCALIZATION")]
        public int ReturnLocalizationId { get; set; }
        [Column("RENT_DATE")]
        public DateTime RentDate { get; set; }
        [Column("RENT_DAYS")]
        public int RentDays { get; set; }
    }
}
