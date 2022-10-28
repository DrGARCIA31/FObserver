using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2.Models
{
    public class Inventario
    {
        [PrimaryKey, AutoIncrement]
        public int IdInventario { get; set; }
        [MaxLength(50)]
        public string CProducto { get; set; }
        [MaxLength(50)]
        public string NombreP { get; set; }
        [MaxLength(50)]
        public int Cantidad { get; set; }  
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        [MaxLength(100)]
        public string Propietario { get; set; }
    }
}
