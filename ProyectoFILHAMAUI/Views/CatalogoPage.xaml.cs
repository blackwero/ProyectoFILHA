using ProyectoFILHAMAUI.Models;
using ProyectoFILHAMAUI.Services;
using ProyectoFILHAMAUI.ViewModels;

namespace ProyectoFILHAMAUI.Views
{
    public partial class CatalogoPage : ContentPage
    {
        private readonly CatalogoViewModel _viewModel;

        public CatalogoPage(CatalogoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarProductosAsync();
        }

        private void OnProductoTapped(object sender, TappedEventArgs e)
        {
            if (sender is Border border && border.BindingContext is Cosmetico producto)
            {
                _viewModel.VerDetalleCommand.Execute(producto);
            }
        }

        private async void OnCuentaTapped(object sender, TappedEventArgs e)
        {
            var sesion = await SessionService.ObtenerSesionAsync();

            if (sesion == null)
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }

            bool cerrarSesion = await DisplayAlert(
                "Mi cuenta",
                $"Sesión iniciada como {sesion.NombreCliente ?? sesion.Correo}",
                "Cerrar sesión",
                "Cancelar");

            if (cerrarSesion)
            {
                SessionService.CerrarSesion();
                await DisplayAlert("Listo", "Sesión cerrada.", "OK");
            }
        }
    }
}