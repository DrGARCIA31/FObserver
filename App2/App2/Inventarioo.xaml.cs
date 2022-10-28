using App2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inventarioo : ContentPage
    {
        public Inventarioo()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Inventario Invent = new Inventario
                {
                    CProducto = txtCProducto.Text,
                    NombreP = txtNombreP.Text,
                    Cantidad = int.Parse(txtCantidad.Text),
                    Estado = txtEstado.Text,
                    Propietario = txtPropietario.Text,
                    Descripcion = txtDescripcion.Text,
                    
                };
                await App.SQLiteDB.SaveInventarioAsync(Invent);

                await DisplayAlert("Registro", "Se guardo de manera exitosa ", "Ok");
                LimpiarControles();
                llenarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "Ok");
            }
        }
        public async void llenarDatos()
        {
            var InventarioList = await App.SQLiteDB.GetInventarioAsync();
            if (InventarioList != null)
            {
                lstInventario.ItemsSource = InventarioList;
            }
        }
        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtCProducto.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNombreP.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEstado.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtPropietario.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdInventario.Text))
            {
                Inventario inventario = new Inventario()
                {
                    IdInventario = Convert.ToInt32(txtIdInventario.Text),
                    CProducto = txtCProducto.Text,
                    NombreP = txtNombreP.Text,
                    Estado = txtEstado.Text,
                    Descripcion = txtDescripcion.Text,
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                    Propietario = txtPropietario.Text,

                };
                await App.SQLiteDB.SaveInventarioAsync(inventario);
                await DisplayAlert("Resgistro", "la actualizacion fue exitosa ", "Ok");


                LimpiarControles();
                txtIdInventario.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnEliminar.IsVisible = false;
                llenarDatos();

            }

        }

        private async void lstInventario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Inventario)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdInventario.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdInventario.ToString()))
            {
                var inventario = await App.SQLiteDB.GetInventarioByIdAsync(obj.IdInventario);
                if (inventario != null)
                {
                    txtIdInventario.Text = inventario.IdInventario.ToString();
                    txtCProducto.Text = inventario.CProducto;
                    txtNombreP.Text = inventario.NombreP;
                    txtEstado.Text = inventario.Estado;
                    txtDescripcion.Text = inventario.Descripcion;
                    txtCantidad.Text = inventario.Cantidad.ToString();
                    txtPropietario.Text = inventario.Propietario;
                    txtIdInventario.IsVisible = false;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var inventario = await App.SQLiteDB.GetInventarioByIdAsync(Convert.ToInt32(txtIdInventario.Text));
            if (inventario != null)
            {
                await App.SQLiteDB.DeleteInventarioAsync(inventario);
                await DisplayAlert("Producto", "Se elimino de manera exitosa", "ok");
                LimpiarControles();
                llenarDatos();
                txtIdInventario.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistrar.IsVisible = true;
            }
        }

        public void LimpiarControles()
        {
            txtIdInventario.Text = "";
            txtCProducto.Text = "";
            txtNombreP.Text = "";
            txtDescripcion.Text = "";
            txtEstado.Text = "";
            txtCantidad.Text = "";
            txtPropietario.Text = ""; 
        }
    }
}