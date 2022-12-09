using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using static AA_ES.Menus;


namespace AA_ES;

class Program
{
    public static List<VideoJuego> catalogo = new List<VideoJuego>();
    public static List<Usuarios> allUsers = new List<Usuarios>();
    private static bool permiso = false;
    private const string pass = "Micontra123";
    static void Main(string[] args)
    {
        try
        {
            //El ÚNICO superadmin
            var usuarioAdmin = new Usuarios("Administrador","1234",DateTime.Now);
            allUsers.Add(usuarioAdmin);

            var VideoJuego1 = new VideoJuego("Apex Legends", 3, 10.99m);
            var VideoJuego2 = new VideoJuego("Payaso Esponja Horror Horripilante Abuelita miedo", 100, 12.00m);
            var VideoJuego3 = new VideoJuego("Crysis 2", 5, 30.00m);
            var VideoJuego4 = new VideoJuego("Victoria 3", 10, 59.99m);

            catalogo.Add(VideoJuego1);
            catalogo.Add(VideoJuego2);
            catalogo.Add(VideoJuego3);
            catalogo.Add(VideoJuego4);

            Menus.menu_IniciarSesion();
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("ArgumentOutOfRangeException: " + e.ToString());
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("InvalidOperationException: " + e.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    public static void crear()
    {

        try
        {
            string name;
            int initial;
            decimal coste;

            Console.Write("Indique el título del videojuego.\n");
            name = Console.ReadLine();

            foreach (var item in catalogo) 
            {
                //string compTitu = item.Titulo;

                if (string.Equals(name, item.Titulo, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("El videojuego " + name + " ya existe en el catalogo");
                    crear();
                }
            } 


            Console.WriteLine("\n\tIndique el número de unidades disponibles.");
            initial = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n\tIndique el precio de venta del videojuego.");
            coste = Convert.ToDecimal(Console.ReadLine());

            //a método crear()
            var videojuego = new VideoJuego(name, initial, coste);
            Console.WriteLine($"Videojuego (#{videojuego.Id}) {videojuego.Titulo} ha sido creado con {videojuego.Unidades} unidades por un precio unitario de {videojuego.PrecioVenta}€.");

            catalogo.Add(videojuego);
            //catalogo.ToArray();
            menu_3();
        }
        catch
        {
            Console.WriteLine("Has introducido datos erroneos");
            crear();
        }

    }
    public static void verCatalogo(List<VideoJuego> v)
    {
        Console.WriteLine($"ID \t TÍTULO \t UNIDADES \t PRECIO VENTA");

        if (permiso == true)
        {
            for (int i = 0; i < v.Count; i++)
            {
                var item = v[i];
                Console.WriteLine($"{item.Id} \t {item.Titulo} \t {item.Unidades} \t\t {item.PrecioVenta}");
            }
        }
        else
        {
            for (int i = 0; i < v.Count; i++)
            {
                var item = v[i];
                if (item.agotado == false)
                {
                    Console.WriteLine($"{item.Id} \t {item.Titulo} \t {item.Unidades} \t\t {item.PrecioVenta}");
                }
            }
        }

        permiso = false;
        menu_IniciarSesion();
    }

     public static void verCatalogoSesion(List<VideoJuego> v)
    { 
        permiso = true;
        Console.WriteLine($"ID \t TÍTULO \t UNIDADES \t PRECIO VENTA");

        if (permiso == true)
        {
            for (int i = 0; i < v.Count; i++)
            {
                var item = v[i];
                Console.WriteLine($"{item.Id} \t {item.Titulo} \t {item.Unidades} \t\t {item.PrecioVenta}");
            }
        }
        else
        {
            for (int i = 0; i < v.Count; i++)
            {
                var item = v[i];
                if (item.agotado == false)
                {
                    Console.WriteLine($"{item.Id} \t {item.Titulo} \t {item.Unidades} \t\t {item.PrecioVenta}");
                }
            }
        }

        permiso = false;
        menu_3();
    }

    public static void añadir(VideoJuego v)
    {

        int añadido;
        string nota_añadir;
        Console.Write("Ha seleccionado añadir videojuego al catálogo.\n" +
                    "\tIndique la cantidad que desea añadir.\n");
        añadido = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\tIndique una pequeña descripción de la operación.");
        nota_añadir = Console.ReadLine();

        v.ComprarJuego(añadido, DateTime.Now, nota_añadir);

        menu_2(v); 
    }

    public static void sacar(VideoJuego v)
    {
        int gasto;
        string nota_gasto;

        Console.Write("Ha seleccionado retirar videojuego del catálogo.\n" +
                    "\tIndique la cantidad que desea retirar.\n");
        gasto = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\tIndique una pequeña descripción de la operación.");
        nota_gasto = Console.ReadLine();

        v.VenderJuego(gasto, DateTime.Now, nota_gasto);

        menu_2(v);
    }


    public static void buscar()
    {
        Console.WriteLine("Introduce nombre de juego (al menos 3 letras): ");
        var tituloV = Console.ReadLine();
        var rx = new Regex(@tituloV, RegexOptions.IgnoreCase); 

        Console.WriteLine("Estos son los resultados de la busqueda '" + tituloV + "': \n"); // a
        foreach (var item in catalogo)
        {
            var word = item.Titulo;
            var unidades = item.Unidades;
            var prec = item.PrecioVenta;
            if (rx.IsMatch(word))
            {
                Console.WriteLine($"TÍTULO \t\t UNIDADES DISPONIBLES \t PRECIO VENTA");
                Console.WriteLine($"{item.Titulo} \t {item.Unidades} \t\t {item.PrecioVenta}");
            }
        }
        Console.WriteLine("\n");
        menu();
    }

    public static void serializar() 
    {
        string mijson = JsonSerializer.Serialize(catalogo);
        File.WriteAllText("Videojuegos", mijson);
    } 
}
