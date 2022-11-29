using System;
using System.Collections.Generic;
using System.Text;

namespace AA_ES;

    class Categorias
{
    public string Id { get; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

     private static int idCategoria = 1;

    public static List<Categorias> listaCategorias = new List<Categorias>();
    public Categorias(string nombre, string descripcion)
    {
        this.Id = idCategoria.ToString();
        idCategoria++;
        this.Nombre = nombre;
        this.Descripcion = descripcion;

    }


}