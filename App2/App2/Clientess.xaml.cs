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
    public partial class Clientess : ContentPage
    {
        public Clientess()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Clientes Clit = new Clientes
                {
                    CC = txtCC.Text,
                    NombreP = txtNombreP.Text,
                    Asunto = txtAsunto.Text,
                    Email = txtEmail.Text,
                    Cell = txtCell.Text,
                    Descripcion = txtDescripcion.Text,

                };
                await App.SQLiteDB.SaveClientesAsync(Clit);

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
            var ClientesList = await App.SQLiteDB.GetClientesAsync();
            if (ClientesList != null)
            {
                lstClientes.ItemsSource = ClientesList;
            }
        }
        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtCC.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNombreP.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtAsunto.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCell.Text))
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
            if (!string.IsNullOrEmpty(txtIdClientes.Text))
            {
                Clientes clientes = new Clientes()
                {
                    IdClientes = Convert.ToInt32(txtIdClientes.Text),
                    CC = txtCC.Text,
                    NombreP = txtNombreP.Text,
                    Asunto = txtAsunto.Text,
                    Email = txtEmail.Text,
                    Cell = txtCell.Text,
                    Descripcion = txtDescripcion.Text, 

                };
                await App.SQLiteDB.SaveClientesAsync(clientes);
                await DisplayAlert("Resgistro", "la actualizacion fue exitosa ", "Ok");


                LimpiarControles();
                txtIdClientes.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnEliminar.IsVisible = false;
                llenarDatos();

            }

        }

        private async void lstClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Clientes)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdClientes.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdClientes.ToString()))
            {
                var clientes = await App.SQLiteDB.GetClientesByIdAsync(obj.IdClientes);
                if (clientes != null)
                {
                    txtIdClientes.Text = clientes.IdClientes.ToString();
                    txtCC.Text = clientes.CC;
                    txtNombreP.Text = clientes.NombreP;
                    txtAsunto.Text = clientes.Asunto;
                    txtEmail.Text = clientes.Email;
                    txtCell.Text = clientes.Cell;
                    txtDescripcion.Text = clientes.Descripcion;
                    txtIdClientes.IsVisible = false;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var clientes = await App.SQLiteDB.GetClientesByIdAsync(Convert.ToInt32(txtIdClientes.Text));
            if (clientes != null)
            {
                await App.SQLiteDB.DeleteClientesAsync(clientes);
                await DisplayAlert("cliente", "Se elimino de manera exitosa", "ok");
                LimpiarControles();
                llenarDatos();
                txtIdClientes.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistrar.IsVisible = true;
            }
        }

        public void LimpiarControles()
        {
            txtIdClientes.Text = "";
            txtCC.Text = "";
            txtNombreP.Text = "";
            txtAsunto.Text = "";
            txtEmail.Text = "";
            txtCell.Text = "";
            txtDescripcion.Text = "";
        }
    }
}