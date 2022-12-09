using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AA_ES;

class Usuarios
{
    public string IdentificadorUser {get;}
    public string Nombre { get; set;}
    public string Contra { get; set;}
    public DateTime Date { get; }
    public bool admin { get; }

    private static int incrementarid = 1;


    public Usuarios(string nombre, string contra, DateTime date)
    {
        IdentificadorUser = incrementarid.ToString();
        incrementarid++;
        Nombre = nombre;
        Contra = contra;
        Date = date;

        //Sólo hay un superadmin, el primero que se creo al principio de la ejecución del Program.cs
        if(this.IdentificadorUser == "1"){
            admin = true;
        }else{
            admin = false;
        }
    }

}