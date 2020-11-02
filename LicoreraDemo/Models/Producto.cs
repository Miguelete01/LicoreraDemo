
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicoreraDemo.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public decimal PreciosNio { get; set; }

        public decimal PrecioUS { get; set; }

        public bool Activo { get; set; }

        public ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}