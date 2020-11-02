using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicoreraDemo.Models
{
    [Table("DetalleFactura")]
    public class DetalleFactura
    {
        [Key]
        public int Id { get; set; }
        //public int DetalleId { get; set; }

        [ForeignKey("Factura")]
        public int FacturaId { get; set; }
        
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        [ForeignKey("TasaCambio")]
        public int TasaCambioId { get; set; }

        public int Cantidad { get; set; }

        public Factura Factura { get; set; }

        public Producto Producto { get; set; }
        public TasaCambio TasaCambio { get; set; }
    }
}
