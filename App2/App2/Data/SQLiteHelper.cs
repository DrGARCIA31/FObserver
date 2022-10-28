using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using App2.Models;
using System.Threading.Tasks;

namespace App2.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper (string dbPath)
        {
            db=new SQLiteAsyncConnection (dbPath);
            db.CreateTableAsync<Transacciones>().Wait();
            db.CreateTableAsync<Clientes>().Wait();
            db.CreateTableAsync<Inventario>().Wait();
        }
        public Task <int> SaveTransaccionesAsync(Transacciones trans)
        {
            if (trans.IdTransacciones!=0)
            {
                return db.UpdateAsync(trans);
            }
            else
            {
                return db.InsertAsync(trans);
            }
        }
        public Task<int> DeleteTransaccionesAsync(Transacciones transacciones)
        {
            return db.DeleteAsync(transacciones);
        }
        /// <summary>
        /// Recuperar todos las transacciones
        /// </summary>
        /// <returns></returns>
        public Task<List<Transacciones>> GetTransaccionesAsync()
        {
            return db.Table<Transacciones>().ToListAsync();
        }
        /// <summary>
        /// Recuperear transacciones por id
        /// </summary>
        /// <param name="idTransacciones"> Id de la transaccion que se requiere</param>
        /// <returns></returns>
        public Task<Transacciones> GetTransaccionesByIdAsync(int idTransacciones)
        {
            return db.Table<Transacciones>().Where(a => a.IdTransacciones == idTransacciones).FirstOrDefaultAsync();
        }

        public Task<int> SaveClientesAsync(Clientes Clit)
        {
            if (Clit.IdClientes != 0)
            {
                return db.UpdateAsync(Clit);
            }
            else
            {
                return db.InsertAsync(Clit);
            }
        }
        public Task<int> DeleteClientesAsync(Clientes clientes)
        {
            return db.DeleteAsync(clientes);
        }
        /// <summary>
        /// Recuperar todos las transacciones
        /// </summary>
        /// <returns></returns>
        public Task<List<Clientes>> GetClientesAsync()
        {
            return db.Table<Clientes>().ToListAsync();
        }
        /// <summary>
        /// Recuperear transacciones por id
        /// </summary>
        /// <param name="idClientes"> Id de la transaccion que se requiere</param>
        /// <returns></returns>
        public Task<Clientes> GetClientesByIdAsync(int idClientes)
        {
            return db.Table<Clientes>().Where(a => a.IdClientes == idClientes).FirstOrDefaultAsync();
        }

        public Task<int> SaveInventarioAsync(Inventario Invent)
        {
            if (Invent.IdInventario != 0)
            {
                return db.UpdateAsync(Invent);
            }
            else
            {
                return db.InsertAsync(Invent);
            }
        }
        public Task<int> DeleteInventarioAsync(Inventario inventario)
        {
            return db.DeleteAsync(inventario);
        }
        /// <summary>
        /// Recuperar todos las transacciones
        /// </summary>
        /// <returns></returns>
        public Task<List<Inventario>> GetInventarioAsync()
        {
            return db.Table<Inventario>().ToListAsync();
        }
        /// <summary>
        /// Recuperear transacciones por id
        /// </summary>
        /// <param name="idInventario"> Id de la transaccion que se requiere</param>
        /// <returns></returns>
        public Task<Inventario> GetInventarioByIdAsync(int idInventario)
        {
            return db.Table<Inventario>().Where(a => a.IdInventario == idInventario).FirstOrDefaultAsync();
        }
    }
}
