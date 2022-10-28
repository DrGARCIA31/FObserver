using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2.Models
{
    public class Clientes
    {
        [PrimaryKey, AutoIncrement]
        public int IdClientes { get; set; }
        [MaxLength(50)]
        public string CC { get; set; }
        [MaxLength(50)]
        public string Asunto { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string NombreP { get; set; }
        [MaxLength(100)]
        public string Cell { get; set; }  
        public string Descripcion { get; set; }

    }
}
