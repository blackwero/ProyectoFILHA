using ProyectoFILHMAUI.Models;
using ProyectoFILHMAUI.PageModels;

namespace ProyectoFILHMAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}