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
    public partial class Transaccioness : ContentPage
    {
        public Transaccioness()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Transacciones trans = new Transacciones
                {
                    CC=txtCC.Text,
                    Dia=txtDia.Date,
                    Transaccion=txtTransaccion.Text,
                    Nombre=txtNombre.Text,
                    Email=txtEmail.Text,
                    Descripcion=txtDescripcion.Text,
                    Valor= int.Parse(txtValor.Text),
                };
                await App.SQLiteDB.SaveTransaccionesAsync(trans);
                
                await DisplayAlert("Registro", "Se guardo de manera exitosa la transaccion", "Ok");
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
            var TransaccionesList = await App.SQLiteDB.GetTransaccionesAsync();
            if (TransaccionesList != null)
            {
                lstTransacciones.ItemsSource = TransaccionesList;
            }
        }
        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtCC.Text))
            {
                respuesta = false;
            }
            
            else if (string.IsNullOrEmpty(txtTransaccion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtValor.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
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
            if (!string.IsNullOrEmpty(txtIdTransacciones.Text))
            {
                Transacciones transacciones = new Transacciones()
                {
                    IdTransacciones=Convert.ToInt32(txtIdTransacciones.Text),
                    CC=txtCC.Text,
                    Dia=txtDia.Date,
                    Transaccion=txtTransaccion.Text,
                    Nombre = txtNombre.Text,
                    Email = txtEmail.Text,
                    Descripcion = txtDescripcion.Text,
                    Valor =Convert.ToInt32(txtValor.Text),

                };
                await App.SQLiteDB.SaveTransaccionesAsync(transacciones);
                await DisplayAlert("Resgistro", "la actualizacion fue exitosa ", "Ok");


                LimpiarControles();
                txtIdTransacciones.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnEliminar.IsVisible = false;
                llenarDatos();

            }

        }

        private async void lstTransacciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj=(Transacciones)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdTransacciones.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdTransacciones.ToString()))
            {
                var transacciones = await App.SQLiteDB.GetTransaccionesByIdAsync(obj.IdTransacciones);
                if (transacciones != null)
                {
                    txtIdTransacciones.Text = transacciones.IdTransacciones.ToString();
                    txtCC.Text = transacciones.CC;
                    txtDia.Date = transacciones.Dia;
                    txtTransaccion.Text = transacciones.Transaccion;
                    txtNombre.Text = transacciones.Nombre;
                    txtEmail.Text = transacciones.Email;
                    txtDescripcion.Text = transacciones.Descripcion;
                    txtValor.Text = transacciones.Valor.ToString();
                    txtIdTransacciones.IsVisible = false;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var transaccion = await App.SQLiteDB.GetTransaccionesByIdAsync(Convert.ToInt32(txtIdTransacciones.Text));
            if (transaccion != null)
            {
                await App.SQLiteDB.DeleteTransaccionesAsync(transaccion);
                await DisplayAlert("transaccion", "Se elimino de manera exitosa", "ok");
                LimpiarControles();
                llenarDatos();
                txtIdTransacciones.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistrar.IsVisible = true;
            }
        }

        public void LimpiarControles()
        {
            txtIdTransacciones.Text = "";
            txtCC.Text = "";
            txtDia.Date = DateTime.Now;
            txtTransaccion.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtEmail.Text = "";
            txtValor.Text = "";
        }
    }
}