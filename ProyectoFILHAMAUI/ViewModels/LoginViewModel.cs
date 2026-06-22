using System.ComponentModel;
using System.Windows.Input;
using ProyectoFILHAMAUI.Models;
using ProyectoFILHAMAUI.Services;

namespace ProyectoFILHAMAUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthApiService _authService;

        private string _correo = string.Empty;
        private string _password = string.Empty;
        private bool _cargando;
        private string _mensajeError = string.Empty;
        private bool _tieneError;

        public string Correo
        {
            get => _correo;
            set { _correo = value; OnPropertyChanged(nameof(Correo)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public bool Cargando
        {
            get => _cargando;
            set { _cargando = value; OnPropertyChanged(nameof(Cargando)); }
        }

        public bool TieneError
        {
            get => _tieneError;
            set { _tieneError = value; OnPropertyChanged(nameof(TieneError)); }
        }

        public string MensajeError
        {
            get => _mensajeError;
            set { _mensajeError = value; OnPropertyChanged(nameof(MensajeError)); }
        }

        public ICommand LoginCommand { get; }
        public ICommand IrARegistroCommand { get; }

        public LoginViewModel(AuthApiService authService)
        {
            _authService = authService;

            LoginCommand = new Command(async () => await EjecutarLoginAsync());
            IrARegistroCommand = new Command(async () => await Shell.Current.GoToAsync("RegistroPage"));
        }

        private async Task EjecutarLoginAsync()
        {
            TieneError = false;

            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Password))
            {
                TieneError = true;
                MensajeError = "Ingresa tu correo y contraseña.";
                return;
            }

            Cargando = true;

            var (exito, sesion, error) = await _authService.LoginAsync(new LoginRequest
            {
                Correo = Correo,
                Password = Password
            });

            Cargando = false;

            if (!exito || sesion == null)
            {
                TieneError = true;
                MensajeError = error ?? "No se pudo iniciar sesión.";
                return;
            }

            await SessionService.GuardarSesionAsync(sesion);

            // Volvemos al catálogo (o a la página anterior) ya logueado
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}