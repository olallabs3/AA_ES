using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using static AA_ES.Program;

namespace AA_ES;

class Menus 
{
    private const string pass = "Micontra123";
    private static bool permiso = false;

   
    public static void menu_IniciarSesion(){
    Console.Write("¿Qué operación desea hacer?\n" +
                        "\tVer catálogo disponible (ver).\n" +
                        "\tIniciar sesión (login).\n" +
                        "\tSalir\n");

                        string option = Console.ReadLine();

        switch (option.ToLower())
        {
          
            case "ver":
                verCatalogo(catalogo);
                break;

            case "login":
                iniciarSesion();
                break;
            case "salir":
                Console.WriteLine("Gracias por confiar en nosotros :D");
                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu_IniciarSesion();
                break;
        }

}
    public static void menu()
    {
        Console.Write("¿Qué operación desea hacer?\n" +
                        "\tVer catálogo disponible (ver).\n" +
                        "\tNueva entrada (crear).\n" +
                        "\tSeleccionar videojuego (seleccionar)\n" +
                        "\tBuscar juego (buscar)\n" +
                        "\tAdministración (administrar)\n" +
                        "\tSalir\n");

        string option = Console.ReadLine();

        switch (option.ToLower())
        {
            case "crear":
                Console.Write("Has seleccionado crear juego\n"); // Olalla
                crear();

                break;

            case "ver":
                verCatalogoSesion(catalogo);

                break;

            case "seleccionar":
                Console.WriteLine("Seleccione el ID del artículo al que quiere acceder");
                menu_2(catalogo[int.Parse(Console.ReadLine()) - 1]); //Intenta seleccionar la cuenta deseada del array
                break;

            case "administrar":
                menu_3();

                break;

            case "buscar":
                buscar();
                break;
            case "salir":
                Console.WriteLine("Gracias por confiar en nosotros :D");
                serializar();
                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu();
                break;
        }
    }
    public static void menu_2(VideoJuego v)
    {

        Console.Write($"Has seleccionado el videojuego (#{v.Id}) {v.Titulo} de precio {v.PrecioVenta} y {v.Unidades} unidades restantes." +
                        "\n¿Qué operación desea hacer?\n" +
                        "\tAñadir unidades (añadir).\n" +
                        "\tSacar unidades (sacar).\n" +
                        "\tVer registro de transacciones (transacciones)\n" +
                        "\tSalir\n");

        string option = Console.ReadLine();

        switch (option.ToLower())
        {

            case "añadir":

                añadir(v);

                break;

            case "sacar":

                sacar(v);

                break;

            case "transacciones" or "registro": //Ojo que igual no va
                Console.WriteLine(v.GetHistory());
                menu_2(v);
                break;

            case "salir":
                Console.WriteLine("\tHa seleccionado salir del programa.");
                menu();
                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu_2(v);
                break;
        }
    }
    public static void menu_3()
    {
        Console.WriteLine("Introduce contraseña: ");
        var contra = Console.ReadLine();
        if (pass == contra)
        {
            permiso = true;
            verCatalogo(catalogo);
        }

        if (contra == "salir")
        {  // Olalla
            menu();
        }
        else if (pass != contra)
        {
            Console.WriteLine("Contraseña erronea, vuelve a intentarlo o escribe 'salir'");
            menu_3();
        } // olalla
    }
    public static void iniciarSesion(){

        Console.Write("¿Qué quieres hacer?\n" +
                       "\tIniciar sesion.\n" +
                       "\tCrear cuenta.\n" +
                       "\tSalir");
        string option = Console.ReadLine();
        switch (option.ToLower())
        {
            case "iniciar":
                 iniciar();


                break;

            case "crear":
                crearCuenta();

                break;


            case "salir":
                Console.WriteLine("Gracias por confiar en nosotros :D");

                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu_IniciarSesion();
                break;
        }

    }

    public static void iniciar(){
        Console.WriteLine("Introduce nombre usuario");
        var nombreUser = Console.ReadLine();
        Console.WriteLine("Introduce contraseña usuario");
        var contraUser = Console.ReadLine();
        foreach (var item in allUsers)
        {
            if (nombreUser == item.Nombre && contraUser==item.Contra){
                menu();
            }
            Console.WriteLine("Los datos introducidos son erroneos");
            iniciar();
        }
        
    }

    public static void crearCuenta(){
        
    }
}