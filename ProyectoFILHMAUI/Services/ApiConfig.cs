using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFILHMAUI.Services
{
   
        public static class ApiConfig
        {
            public static string BaseUrl =>
#if ANDROID
                "http://10.0.2.2:5286/"; // reemplaza 5286 por el puerto HTTP real de tu API (ver launchSettings.json)
#else
            "https://localhost:7159/";
#endif
        }
    
}
