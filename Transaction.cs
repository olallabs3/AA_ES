using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AA_ES;

class Transaction
{
    public decimal Unidades { get; }
    public DateTime Date { get; }
    public Transaction(int unidades, DateTime date)
    {
        Unidades = unidades;
        Date = date;
    }
}