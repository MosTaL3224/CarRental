using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.DBObjects
{
    [Table("DRIVE_TYPE")]
    public class DriveType
    {
        [Column("ID")]
        public int DriveTypeId { get; set; }

        [Column("DRIVE_TYPE")]
        public string Type { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}
