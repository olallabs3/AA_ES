using System;
using System.Collections.Generic;
using System.Text;

namespace AA_ES;

    class VideoJuego
    {
        public string Id { get; }
        public string Titulo { get; set; }
        public int Unidades {get; set;}
        public decimal PrecioVenta { get; set; }
        public bool agotado {get; set;}

        //public decimal PrecioCompra { get; set; }
        //private List<Categoria> categorias = new List<Categoria>();

        private static int accountNumberSeed = 1;

        private List<Transaction> allTransactions = new List<Transaction>();

        public VideoJuego(string nombre, int unidades, decimal precio)
        {
        
            this.Id = accountNumberSeed.ToString();
            accountNumberSeed++;
            this.Titulo = nombre;
            this.PrecioVenta = precio;
            this.Unidades = 0;
            ComprarJuego(unidades, DateTime.Now, " cantidad");
            this.agotado = false;

    }

    public void ComprarJuego(int unidades, DateTime date, string note)
        {
            //try catch
            //Comprobar valor negativo
            if (unidades <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unidades), "No puedes no comprar ningun juego");
            }
            var compra = new Transaction(unidades, date);
            allTransactions.Add(compra);
            this.Unidades += unidades;
        }

        public void VenderJuego(int unidades, DateTime date, string note)
        {
              //try catch
            //Comprobar valor negativo
            if (unidades <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unidades), "No puedes no comprar ningun juego");
            }
            if (this.Unidades - unidades < 0)
            {
                throw new InvalidOperationException("No hay suficientes unidades :(");
                
            }
            var venta = new Transaction(-unidades, date);
            allTransactions.Add(venta);
            this.Unidades -= unidades;
            if (Unidades == 0){
               this.agotado = true;
            }
        }

        public string GetHistory()
        {
            var report = new StringBuilder();

            decimal balance = 0;
            report.AppendLine("Unidades\tTítulo\tPrecio unidad");
            foreach (var item in allTransactions)
            {
                balance += item.Unidades;
                report.AppendLine($"{item.Unidades}\t{this.Titulo}\t{this.PrecioVenta}");
            }

            return report.ToString();
        }
    }
