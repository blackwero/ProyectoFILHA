using ProyectoFILHAMAUI.Views;

namespace ProyectoFILHAMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetalleProductoPage), typeof(DetalleProductoPage));
        }
    }
}