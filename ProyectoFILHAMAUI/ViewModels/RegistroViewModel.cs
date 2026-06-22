using System.ComponentModel;
using System.Windows.Input;
using ProyectoFILHAMAUI.Models;
using ProyectoFILHAMAUI.Services;

namespace ProyectoFILHAMAUI.ViewModels
{
    public class RegistroViewModel : INotifyPropertyChanged
    {
        private readonly AuthApiService _authService;

        private string _nombre = string.Empty;
        private string _correo = string.Empty;
        private string _telefono = string.Empty;
        private string _password = string.Empty;
        private string _confirmarPassword = string.Empty;
        private DateTime _fechaNacimiento = DateTime.Now.AddYears(-18);

        private bool _cargando;
        private bool _tieneError;
        private string _mensajeError = string.Empty;

        public string Nombre { get => _nombre; set { _nombre = value; OnPropertyChanged(nameof(Nombre)); } }
        public string Correo { get => _correo; set { _correo = value; OnPropertyChanged(nameof(Correo)); } }
        public string Telefono { get => _telefono; set { _telefono = value; OnPropertyChanged(nameof(Telefono)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string ConfirmarPassword { get => _confirmarPassword; set { _confirmarPassword = value; OnPropertyChanged(nameof(ConfirmarPassword)); } }
        public DateTime FechaNacimiento { get => _fechaNacimiento; set { _fechaNacimiento = value; OnPropertyChanged(nameof(FechaNacimiento)); } }

        public bool Cargando { get => _cargando; set { _cargando = value; OnPropertyChanged(nameof(Cargando)); } }
        public bool TieneError { get => _tieneError; set { _tieneError = value; OnPropertyChanged(nameof(TieneError)); } }
        public string MensajeError { get => _mensajeError; set { _mensajeError = value; OnPropertyChanged(nameof(MensajeError)); } }

        public ICommand RegistrarCommand { get; }

        public RegistroViewModel(AuthApiService authService)
        {
            _authService = authService;
            RegistrarCommand = new Command(async () => await EjecutarRegistroAsync());
        }

        private async Task EjecutarRegistroAsync()
        {
            TieneError = false;

            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Password))
            {
                TieneError = true;
                MensajeError = "Nombre, correo y contraseña son obligatorios.";
                return;
            }

            if (Password != ConfirmarPassword)
            {
                TieneError = true;
                MensajeError = "Las contraseñas no coinciden.";
                return;
            }

            if (Password.Length < 6)
            {
                TieneError = true;
                MensajeError = "La contraseña debe tener al menos 6 caracteres.";
                return;
            }

            Cargando = true;

            var (exito, sesion, error) = await _authService.RegistroAsync(new RegistroRequest
            {
                Nombre = Nombre,
                Correo = Correo,
                Password = Password,
                Telefono = Telefono,
                FechaNacimiento = FechaNacimiento
            });

            Cargando = false;

            if (!exito || sesion == null)
            {
                TieneError = true;
                MensajeError = error ?? "No se pudo crear la cuenta.";
                return;
            }

            await SessionService.GuardarSesionAsync(sesion);

            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}