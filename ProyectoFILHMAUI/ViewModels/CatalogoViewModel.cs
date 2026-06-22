using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ProyectoFILHMAUI.Models;
using ProyectoFILHMAUI.Services;

namespace ProyectoFILHMAUI.ViewModels
{
    public class CatalogoViewModel : INotifyPropertyChanged
    {
        private readonly CosmeticoApiService _apiService;
        private bool _cargando;

        public ObservableCollection<Cosmetico> Productos { get; } = new();

        public bool Cargando
        {
            get => _cargando;
            set { _cargando = value; OnPropertyChanged(nameof(Cargando)); }
        }

        public CatalogoViewModel(CosmeticoApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task CargarProductosAsync()
        {
            if (Cargando) return;
            Cargando = true;

            try
            {
                var productos = await _apiService.GetCosmeticosAsync();
                Productos.Clear();
                foreach (var p in productos)
                    Productos.Add(p);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando productos: {ex.Message}");
                // TODO: mostrar alerta al usuario más adelante
            }
            finally
            {
                Cargando = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}