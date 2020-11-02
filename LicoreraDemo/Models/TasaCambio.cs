using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicoreraDemo.Models
{
    [Table("TasaCambio")]
    public class TasaCambio
    {
        [Key]
        public int Id { get; set; }
        //public int IdTasa { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public decimal Equivalencia { get; set; }

        public ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
