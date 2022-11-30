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

            //Control de que el campo Título no esté vacío
            try{
                if(nombre == "" || nombre == " "){
                    throw new ArgumentNullException(nameof(nombre), "El campo Título no puede estar vacío.");
                }else{
                    this.Titulo = nombre;
                }
            }catch (ArgumentNullException e){
                Console.WriteLine("ArgumentNullException: " + e.ToString());
            }

            //Control de que el campo Unidades sea negativo 
            try{
                if(unidades > 0){
                    throw new ArgumentOutOfRangeException(nameof(unidades), "El campo de unidades no puede ser menor que 0.");
                }
            }catch (ArgumentOutOfRangeException e){
                Console.WriteLine("ArgumentOutOfRangeException: " + e.ToString());
            }

            //Control de que el campo Precio sea negativo 
            try{
               if (precio <= 0){
                    throw new ArgumentOutOfRangeException(nameof(unidades), "El campo de unidades no puede ser menor que 0.");
               }else{
                    this.PrecioVenta = precio;
               }
            }
            catch (ArgumentOutOfRangeException e){
                Console.WriteLine("ArgumentOutOfRangeException: " + e.ToString());
            }

            //Crear registro sin unidades y sin transacciones
            if (unidades == 0){
                this.Unidades = 0;
                this.agotado = true;
            }else{
                ComprarJuego(unidades, DateTime.Now, " cantidad");
                this.agotado = false;
            }
        }

        public void ComprarJuego(int unidades, DateTime date, string note)
        {
            try{
                if (unidades <= 0){
                    throw new ArgumentOutOfRangeException(nameof(unidades), "No puedes no comprar ningun juego");
                }
                var compra = new Transaction(unidades, date);
                allTransactions.Add(compra);
                this.Unidades += unidades;
            }catch (ArgumentOutOfRangeException e){
                Console.WriteLine("ArgumentOutOfRangeException: " + e.ToString());
            }  
        }

        public void VenderJuego(int unidades, DateTime date, string note){
            try{
            
                if (unidades <= 0){
                    throw new ArgumentOutOfRangeException(nameof(unidades), "No puedes no comprar ningun juego");
                }
                if (this.Unidades - unidades < 0){
                    throw new InvalidOperationException("No hay suficientes unidades :(");
                    
                }
            }catch (ArgumentOutOfRangeException e){
                Console.WriteLine("ArgumentOutOfRangeException: " + e.ToString());
            }catch(InvalidOperationException e){
                Console.WriteLine("ArgumentOutOfRangeException: " + e.ToString());
            }

            //¿Dentro del try-catch?
            var venta = new Transaction(-unidades, date);
            allTransactions.Add(venta);
            this.Unidades -= unidades;
            if (unidades == 0){
               this.agotado = true;
            }
        }

        public string GetHistory()
        {
            var report = new StringBuilder();

            decimal balance = 0;
            report.AppendLine("Unidades\tTítulo\tPrecio");
            foreach (var item in allTransactions)
            {
                balance += item.Unidades;
                report.AppendLine($"{item.Unidades}\t{this.Titulo}\t{this.PrecioVenta}");
            }

            return report.ToString();
        }
    }
