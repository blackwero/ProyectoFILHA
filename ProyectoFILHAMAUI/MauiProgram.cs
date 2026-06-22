using Microsoft.Extensions.Logging;
using ProyectoFILHAMAUI.Services;
using ProyectoFILHAMAUI.ViewModels;
using ProyectoFILHAMAUI.Views;

namespace ProyectoFILHAMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(sp => new CosmeticoApiService(
                new HttpClient { BaseAddress = new Uri(ApiConfig.BaseUrl) }));

            builder.Services.AddSingleton(sp => new AuthApiService(
                new HttpClient { BaseAddress = new Uri(ApiConfig.BaseUrl) }));

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddTransient<RegistroViewModel>();
            builder.Services.AddTransient<RegistroPage>();

            builder.Services.AddTransient<CatalogoViewModel>();
            builder.Services.AddTransient<CatalogoPage>();
            builder.Services.AddTransient<DetalleProductoPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}