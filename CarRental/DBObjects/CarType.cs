using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.DBObjects
{
    [Table("CAR_TYPE")]
    public class CarType
    {
        [Column("ID")]
        public int CarTypeId { get; set; }

        [Column("CAR_TYPE")]
        public string Type { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}
