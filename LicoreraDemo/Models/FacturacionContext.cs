using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicoreraDemo.Models
{
    public class FacturacionContext : DbContext
    {
        public FacturacionContext(DbContextOptions<FacturacionContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cliente
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id= 1, Nombre = "Pepe", Apellidos = "Perez", Direccion = "El Tanque Rojo", NoIdentificacion = "0010101010009J", Telefono = "89889988" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id= 2, Nombre = "Mike", Apellidos = "Leon", Direccion = "Tokyi", NoIdentificacion = "2210101010009J", Telefono = "88776688" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id= 3, Nombre = "Elon", Apellidos = "Musk", Direccion = "New York", NoIdentificacion = "0010112010009J", Telefono = "87898786" });
            //Producto
            modelBuilder.Entity<Producto>().HasData(new Producto { Id= 1, Nombre = "Mouse", Descripcion = "Dell inalambrico", PreciosNio = 210, PrecioUS = 10, Stock = 90 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id= 2, Nombre = "Teclado", Descripcion = "Dell Mecanico", PreciosNio = 800, PrecioUS = 25, Stock = 20 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id= 3, Nombre = "Camara", Descripcion = "Cannon Semi Profecional", PreciosNio = 3210, PrecioUS = 100, Stock = 14 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id= 4, Nombre = "Cama", Descripcion = "Ortopedica", PreciosNio = 8110, PrecioUS = 230, Stock = 80 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id= 5, Nombre = "Mesa", Descripcion = "Madera de cedro macho", PreciosNio = 4510, PrecioUS = 130, Stock = 50 });
            // Tasa de cambio
            modelBuilder.Entity<TasaCambio>().HasData(new TasaCambio { Id= 1, Fecha = DateTime.UtcNow, Equivalencia = 35 });
            //Factura
            modelBuilder.Entity<Factura>().HasData(new Factura { Id= 1, NoFactura = "F0001", ClienteId = 1, Fecha = DateTime.UtcNow });
            modelBuilder.Entity<Factura>().HasData(new Factura { Id= 2, NoFactura = "F0002", ClienteId = 2, Fecha = DateTime.UtcNow });
            //DetalleFactura
            modelBuilder.Entity<DetalleFactura>().HasData(new DetalleFactura { Id= 1, FacturaId = 1, ProductoId = 1, TasaCambioId = 1, Cantidad = 1 });
            modelBuilder.Entity<DetalleFactura>().HasData(new DetalleFactura { Id= 2, FacturaId = 1, ProductoId = 3, TasaCambioId = 1, Cantidad = 1 });
            modelBuilder.Entity<DetalleFactura>().HasData(new DetalleFactura { Id= 3, FacturaId = 1, ProductoId = 1, TasaCambioId = 1, Cantidad = 2 });
            modelBuilder.Entity<DetalleFactura>().HasData(new DetalleFactura { Id= 4, FacturaId = 2, ProductoId = 4, TasaCambioId = 1, Cantidad = 2 });
            modelBuilder.Entity<DetalleFactura>().HasData(new DetalleFactura { Id= 5, FacturaId = 2, ProductoId = 2, TasaCambioId = 1, Cantidad = 2 });
            modelBuilder.Entity<DetalleFactura>().HasData(new DetalleFactura { Id= 6, FacturaId = 2, ProductoId = 3, TasaCambioId = 1, Cantidad = 1 });
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<TasaCambio> TasaCambios { get; set; }

    }
}
