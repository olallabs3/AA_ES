using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AA_ES;

class Usuarios
{
    public int IdentificadorUser {get;}
    public string Nombre { get; set;}
     public string Contra { get; set;}
    public DateTime Date { get; }

    private static int incrementarid = 1;


    public Usuarios(string nombre, string contra, DateTime date)
    {
        IdentificadorUser = incrementarid;
        incrementarid++;
        Nombre = nombre;
        Contra = contra;
        Date = date;
    }

}