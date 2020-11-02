using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicoreraDemo.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellidos { get; set; }

        [StringLength(100)]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string NoIdentificacion { get; set; }

        [StringLength(10)]
        public string Telefono { get; set; }
        [JsonIgnore]
        public ICollection<Factura> Facturas { get; set; }
    }
}
