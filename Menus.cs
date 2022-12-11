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
    public static bool permiso = false;

    //Menú Login / Salir
    public static void menu_IniciarSesion()
    {
        Console.Write("¿Qué operación desea hacer?\n" +
                            "\tIniciar sesión (login).\n" +
                            "\tSalir.\n");

        string option = Console.ReadLine();

        switch (option.ToLower())
        {
            case "login":
                iniciarSesion();
                break;
            case "salir":
                Console.WriteLine("Gracias por confiar en nosotros :D");
                Environment.Exit(-1);
                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu_IniciarSesion();
                break;
        }

    }

    // Menú Login / Registro
    public static void iniciarSesion()
    {

        Console.Write("¿Qué quieres hacer?\n" +
                       "\tIniciar sesion. (iniciar)\n" +
                       "\tCrear cuenta. (registrar)\n" +
                       "\tSalir (salir)\n");
        string option = Console.ReadLine();
        switch (option.ToLower())
        {
            case "iniciar":
                iniciar();
                break;

            case "registrar":
                crearCuenta();
                break;

            case "salir":
                Console.WriteLine("Gracias por confiar en nosotros :D");
                Environment.Exit(-1);
                break;
                
                default:
                Console.WriteLine("Operación no válida.");
                iniciarSesion();
                break;
        }
    }

    // Iniciar sesión
    public static void iniciar()
    {
        Console.WriteLine("Introduce nombre usuario");
        var nombreUser = Console.ReadLine();
        Console.WriteLine("Introduce contraseña usuario");
        var contraUser = Console.ReadLine();

        if (nombreUser == "Administrador" && contraUser =="1234"){
                menu_3();
        }

        foreach (var item in allUsers){

            if (nombreUser == item.Nombre && contraUser == item.Contra){
                menu();
            }
        }
        Console.Write($"\nLos datos introducidos son erroneos\n" +
                        "\tReintentar.\n" +
                        "\tSalir (salir).\n");

        var ans = Console.ReadLine();
        if(ans.ToLower() == "salir"){
            iniciarSesion();
        }else{
            iniciar();
        }
    }

    // Crear un nuevo usuario sin permisos de Admin
    public static void crearCuenta(){
        try
        {
            Console.WriteLine("Introduce nombre usuario");
            var nombreUser = Console.ReadLine();
            if (nombreUser== ""||nombreUser== " "){
                Console.WriteLine("Datos introducidos erroneos");
                crearCuenta();
            }
            Console.WriteLine("Introduce contraseña usuario");
            var contraUser = Console.ReadLine();
            var nuevoUsuario = new Usuarios(nombreUser, contraUser, DateTime.Now);
            allUsers.Add(nuevoUsuario);
            serializar();
            menu();
        }

        catch (Exception e)
        {
            Console.WriteLine("Datos introducidos erroneos");
            crearCuenta();
        }
    }

    // Zona pública, se pueden ver los videojuegos NO agotados
    public static void menu()
    {
        Console.Write("¿Qué operación desea hacer?\n" +
                        "\tVer catálogo disponible (ver).\n" +                      
                        "\tBuscar juego (buscar)\n" +      
                        "\tSalir\n");

        string option = Console.ReadLine();

        switch (option.ToLower())
        {         

            case "ver":
                verCatalogo(catalogo);

                break;        

            case "buscar":
                buscar();
                break;
            case "salir":
                serializar();
                iniciarSesion();
                //Environment.Exit(-1);

                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu();
                break;
        }
    }

    // Zona privada, sólo el admin puede entrar
    public static void menu_3()
    {
            Console.Write($"\n¿Qué operación desea hacer?\n" +
                        "\tVer catalogo completo (ver).\n" +
                        "\tNueva entrada (crear).\n" +
                        "\tSeleccionar videojuego (seleccionar)\n" +
                        "\tVer usuarios (usuarios).\n" +
                        "\tBorrar usuario(borrar)\n" +
                        "\tSalir\n");

            string option = Console.ReadLine();

        switch (option.ToLower())
        {

              case "crear":
                Console.Write("Has seleccionado crear juego\n"); 
                crear();

                break;

            case "ver":
                permiso = true;
                verCatalogo(catalogo);
                break;

            case "seleccionar":
                Console.WriteLine("Seleccione el ID del artículo al que quiere acceder");
                menu_2(catalogo[int.Parse(Console.ReadLine()) - 1]); //Intenta seleccionar la cuenta deseada del array
                break;

            case "usuarios":
                verUsuarios();

                break;

            case "borrar":

                borrarUsuario();

                break;
            case "salir":
                Console.WriteLine("\tHa seleccionado salir del programa.");
                permiso = false;
                iniciarSesion();
                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu_3();
                break;
        }
    }

    // Administrar videojuego
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
                menu_3();
                break;

            default:
                Console.WriteLine("Operación no válida.");
                menu_2(v);
                break;
        }
    }

    public static void verUsuarios()
    {
        foreach (var item in allUsers)
        {
            Console.WriteLine($"{item.IdentificadorUser} \t {item.Nombre} \t\t {item.Date}");
        }
        Console.WriteLine("\n");
        menu_3();
    }
    public static void borrarUsuario()
    {
        Console.WriteLine("ID \t NOMBRE \t\t FECHA CREACIÓN");
        foreach (var item in allUsers)
        {           
            Console.WriteLine($"{item.IdentificadorUser} \t {item.Nombre} \t\t {item.Date}");


        }
        Console.WriteLine("\n");
        Console.WriteLine("¿Que usuario quieres borrar? Ingresa la ID");
        var idborrar = Console.ReadLine();
        if (idborrar == "1"){
                Console.WriteLine("No se puede borrar al admnistrador");
                borrarUsuario();
            }
        try
        {
            
            foreach (var item in allUsers)
            {
                if (idborrar == item.IdentificadorUser)
                {
                    allUsers.Remove(item);
                }
            }
        }
        catch (Exception e)
        {
            menu_3();
        }
        Console.WriteLine("El usuario ha sido borrado correctamente :)");
        menu_3();

    }
}