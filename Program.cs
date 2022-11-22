using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AA_ES;
class Program
{
    private static List<VideoJuego> catalogo = new List<VideoJuego>();
    static void Main(string[] args)
    {
        try{

            var VideoJuego1 = new VideoJuego ("Apex Legends",3,10.99m);
            var VideoJuego2 = new VideoJuego ("Payaso Esponja Horror Horripilante Abuelita miedo",100,12.00m);
            var VideoJuego3 = new VideoJuego ("Crysis 2",5,30.00m);
            var VideoJuego4 = new VideoJuego ("Victoria 3",10,59.99m);

            catalogo.Add(VideoJuego1);
            catalogo.Add(VideoJuego2);
            catalogo.Add(VideoJuego3);
            catalogo.Add(VideoJuego4);

            /*
            foreach (var item in catalogo)
            {
                Console.WriteLine(item.Numero);
                Console.WriteLine(item.Nombre);
                Console.WriteLine(item.Precio);
                Console.WriteLine(item.Unidades);
            }
            */
            menu();
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

    public static void menu(){
            Console.Write("¿Qué operación desea hacer?\n"+
                            "\tVer catálogo (ver).\n"+
                            "\tNueva entrada (crear).\n"+
                            "\tSeleccionar videojuego (seleccionar)\n"+
                            "\tSalir\n");

            string option = Console.ReadLine();

            switch (option.ToLower()){
                case "crear":

                    crear();

                break;

                case "ver":
                    verCatalogo(catalogo);
                    
                break;

                case "seleccionar":
                    Console.WriteLine("Seleccione el ID del artículo al que quiere acceder");
                    menu_2(catalogo[int.Parse(Console.ReadLine())-1]); //Intenta seleccionar la cuenta deseada del array
                break;

                case "salir":

                break;

                default:
                    Console.WriteLine("Operación no válida.");
                    menu();
                break;
            }
        }
        public static void menu_2(VideoJuego v){

            Console.Write($"Has seleccionado el videojuego (#{v.Id}) {v.Titulo} de precio {v.PrecioVenta} y {v.Unidades} unidades restantes."+
                            "\n¿Qué operación desea hacer?\n"+
                            "\tAñadir unidades (añadir).\n"+
                            "\tSacar unidades (sacar).\n"+
                            "\tVer registro de transacciones (transacciones)\n"+
                            "\tSalir\n");

            string option = Console.ReadLine();

            switch(option.ToLower()){

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

        public static void crear(){
            string name;
            int initial;
            decimal coste;


            Console.Write("Ha seleccionado crear un nuevo registro de videojuego.\n"+
                        "\tIndique el título del videojuego.\n");
            name = Console.ReadLine();

            Console.WriteLine("\n\tIndique el número de unidades disponibles.");
            initial = Convert.ToInt32(Console.ReadLine()); //Posible conflicto con el tipo de dato

            Console.WriteLine("\n\tIndique el precio de venta del videojuego.");
            coste = Convert.ToDecimal(Console.ReadLine());

            //a método crear()
            var videojuego = new VideoJuego(name, initial, coste);
            Console.WriteLine($"Videojuego (#{videojuego.Id}) {videojuego.Titulo} ha sido creado con {videojuego.Unidades} unidades por un precio unitario de {videojuego.PrecioVenta}€.");
            
            catalogo.Add(videojuego);
            //catalogo.ToArray();
            menu();
        }

        public static void verCatalogo(List<VideoJuego> v){
            Console.WriteLine($"ID \t TÍTULO \t UNIDADES \t PRECIO VENTA");

            for (int i = 0; i < v.Count; i++){
                var item = v[i];
                if (item.agotado == false){
                Console.WriteLine($"{item.Id} \t {item.Titulo} \t {item.Unidades} \t\t {item.PrecioVenta}");
                }
            }
            
            menu();
        }
        /* Creo que no lo llego a usar, es menu2
        public static string acceder(){
            Console.WriteLine($"Seleccione el ID de la cuenta a la que quiere acceder.\n");
            return Console.ReadLine();
        }*/

        public static void añadir(VideoJuego v){
            int añadido;
            string nota_añadir;
            Console.Write("Ha seleccionado añadir videojuego al catálogo.\n"+
                        "\tIndique la cantidad que desea añadir.\n");
            añadido = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\tIndique una pequeña descripción de la operación.");
            nota_añadir = Console.ReadLine();

            v.ComprarJuego(añadido, DateTime.Now, nota_añadir);

            menu_2(v); //El problema es que está metido en una función aparte
        }

        public static void sacar(VideoJuego v){
            int gasto;
            string nota_gasto;
            Console.Write("Ha seleccionado retirar videojuego del catálogo.\n"+
                        "\tIndique la cantidad que desea retirar.\n");
            gasto = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\tIndique una pequeña descripción de la operación.");
            nota_gasto = Console.ReadLine();

            v.VenderJuego(gasto, DateTime.Now, nota_gasto);

            menu_2(v);
        }
        
}