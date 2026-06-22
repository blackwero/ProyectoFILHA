using ProyectoFILHAMAUI.ViewModels;

namespace ProyectoFILHAMAUI.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}