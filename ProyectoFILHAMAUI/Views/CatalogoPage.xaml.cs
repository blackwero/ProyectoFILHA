using ProyectoFILHAMAUI.Models;
using ProyectoFILHAMAUI.ViewModels;

namespace ProyectoFILHAMAUI.Views;

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
}