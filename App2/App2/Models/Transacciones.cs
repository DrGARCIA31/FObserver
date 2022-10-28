using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2.Models
{
    public class Transacciones
    {
        [PrimaryKey, AutoIncrement]
        public int IdTransacciones { get; set; }
        [MaxLength(50)]
        public string CC { get; set; }
        [MaxLength(50)]
        public DateTime Dia { get; set; }
        [MaxLength(50)]
        public string Transaccion { get; set; }
        [MaxLength(100)]
        public int Valor { get; set; }
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Asunto { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }      
        public string Estado { get; set; }
        [MaxLength(100)]
        public string Cell { get; set; }

    }
}
