using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using ProyectoFILHAMAUI.Models;
using ProyectoFILHAMAUI.Services;

namespace ProyectoFILHAMAUI.ViewModels
{
    public class CatalogoViewModel : INotifyPropertyChanged
    {
        private readonly CosmeticoApiService _apiService;
        private List<Cosmetico> _todosLosProductos = new();
        private string _categoriaSeleccionada = "Todos";

        private bool _cargando;
        private bool _isRefreshing;
        private bool _tieneError;
        private string _mensajeError = string.Empty;

        public ObservableCollection<Cosmetico> Productos { get; } = new();
        public ObservableCollection<CategoriaChip> Categorias { get; } = new();

        public bool Cargando
        {
            get => _cargando;
            set { _cargando = value; OnPropertyChanged(nameof(Cargando)); OnPropertyChanged(nameof(MostrarVacio)); }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        public bool TieneError
        {
            get => _tieneError;
            set { _tieneError = value; OnPropertyChanged(nameof(TieneError)); OnPropertyChanged(nameof(MostrarVacio)); }
        }

        public string MensajeError
        {
            get => _mensajeError;
            set { _mensajeError = value; OnPropertyChanged(nameof(MensajeError)); }
        }

        // Solo mostramos "vacío" cuando ya terminó de cargar, no hay error, y no hay resultados
        public bool MostrarVacio => !Cargando && !TieneError && Productos.Count == 0;

        public ICommand RefrescarCommand { get; }
        public ICommand ReintentarCommand { get; }

        public CatalogoViewModel(CosmeticoApiService apiService)
        {
            _apiService = apiService;
            RefrescarCommand = new Command(async () => await CargarProductosAsync(esRefresh: true));
            ReintentarCommand = new Command(async () => await CargarProductosAsync());
        }

        public async Task CargarProductosAsync(bool esRefresh = false)
        {
            if (Cargando) return;

            TieneError = false;

            if (esRefresh) IsRefreshing = true;
            else Cargando = true;

            try
            {
                _todosLosProductos = await _apiService.GetCosmeticosAsync();
                ConstruirCategorias();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando productos: {ex.Message}");
                TieneError = true;
                MensajeError = "No pudimos cargar el catálogo. Revisa tu conexión e intenta de nuevo.";
            }
            finally
            {
                Cargando = false;
                IsRefreshing = false;
                OnPropertyChanged(nameof(MostrarVacio));
            }
        }

        private void ConstruirCategorias()
        {
            var nombres = _todosLosProductos
                .Where(p => p.Categoria != null && !string.IsNullOrWhiteSpace(p.Categoria.Nombre))
                .Select(p => p.Categoria!.Nombre!)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            Categorias.Clear();
            Categorias.Add(CrearChip("Todos"));
            foreach (var nombre in nombres)
                Categorias.Add(CrearChip(nombre));
        }

        private CategoriaChip CrearChip(string nombre)
        {
            var chip = new CategoriaChip
            {
                Nombre = nombre,
                IsSelected = nombre == _categoriaSeleccionada
            };
            chip.SeleccionarCommand = new Command(() => SeleccionarCategoria(nombre));
            return chip;
        }

        private void SeleccionarCategoria(string nombre)
        {
            if (_categoriaSeleccionada == nombre) return;

            _categoriaSeleccionada = nombre;
            foreach (var chip in Categorias)
                chip.IsSelected = chip.Nombre == nombre;

            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var filtrados = _categoriaSeleccionada == "Todos"
                ? _todosLosProductos
                : _todosLosProductos.Where(p => p.Categoria?.Nombre == _categoriaSeleccionada).ToList();

            Productos.Clear();
            foreach (var p in filtrados)
                Productos.Add(p);

            OnPropertyChanged(nameof(MostrarVacio));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}