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
}