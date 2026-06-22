using ProyectoFILHAMAUI.ViewModels;

namespace ProyectoFILHAMAUI.Views
{
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage(RegistroViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}