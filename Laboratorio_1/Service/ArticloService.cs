using Laboratorio_1.Models;
using System.Collections.Generic;

namespace Laboratorio_1.Service
{
    /// <summary>
    /// Representa la lista de articulos disponibles
    /// </summary>
    public static class ArticloService
    {
        public static List<Articulos> listaArticulos = new List<Articulos>
        {
            new Articulos { NombreArticulo = "Camisa", Precio = 15.00 },
            new Articulos { NombreArticulo = "Cinturón", Precio = 8.00 },
            new Articulos { NombreArticulo = "Zapatos", Precio = 40.00 },
            new Articulos { NombreArticulo = "Pantalón", Precio = 25.00 },
            new Articulos { NombreArticulo = "Calcetines", Precio = 2.50 },
            new Articulos { NombreArticulo = "Faldas", Precio = 18.00 },
            new Articulos { NombreArticulo = "Gorras", Precio = 9.00 },
            new Articulos { NombreArticulo = "Suéter", Precio = 19.00 },
            new Articulos { NombreArticulo = "Corbatas", Precio = 12.00 },
            new Articulos { NombreArticulo = "Chaquetas", Precio = 35.00 },
        };

        
    }
}
