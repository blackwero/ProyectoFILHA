using ProyectoFILHAMAUI.Models;

namespace ProyectoFILHAMAUI.Views
{
    [QueryProperty(nameof(Producto), "Producto")]
    public partial class DetalleProductoPage : ContentPage
    {
        public Cosmetico Producto
        {
            set => BindingContext = value;
        }

        public DetalleProductoPage()
        {
            InitializeComponent();
        }

        private async void OnAgregarAlCarritoClicked(object sender, EventArgs e)
        {
            // Sin funcionalidad todavía — solo feedback visual
            await DisplayAlert("Carrito", "Esta función estará disponible próximamente.", "Entendido");
        }
    }
}