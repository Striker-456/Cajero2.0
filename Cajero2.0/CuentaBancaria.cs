using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cajero
{
    internal class




        CuentaBancaria
    {
        public string NumeroCuenta { get; private set; }
        private string Clave;
        public double Saldo { get; private set; }

        private List<string> movimientos;
        private string ArchivoMovimientos;
        private string ArchivoClave;
        private string ArchivoSaldo;
        public string ArchivoCuenta;


        public CuentaBancaria(string NumeroCuenta)
        {
            NumeroCuenta = NumeroCuenta;
            ArchivoCuenta = $"C:\\Users\\hecto\\Downloads\\Cajero2.0\\usuario{NumeroCuenta}";// Carpeta para guardar los archivos de cada usuario
            ArchivoSaldo = Path.Combine($"C:\\Users\\hecto\\Downloads\\Cajero2.0\\usuario{NumeroCuenta}\\Saldo.txt");// Ruta completa del archivo de saldo
            ArchivoClave = Path.Combine($"C:\\Users\\hecto\\Downloads\\Cajero2.0\\usuario{NumeroCuenta}\\clave.txt");// Ruta completa del archivo de clave
            ArchivoMovimientos = Path.Combine($"C:\\Users\\hecto\\Downloads\\Cajero2.0\\\\usuario{NumeroCuenta}\\movimientos.txt");

            movimientos = new List<string>();   //Con este comando esta inicializando la lista

            if (!Directory.Exists(ArchivoCuenta))// Si el directorio no existe, lo crea
                Directory.CreateDirectory(ArchivoCuenta);

            if (!File.Exists(ArchivoSaldo))//Si el archivo de saldo no existe,lo crea
                File.WriteAllText(ArchivoSaldo, "0");
            Saldo = double.Parse(File.ReadAllText(ArchivoSaldo)); //Lee el saldo del archivo y lo asigna a la variable Saldo

            if (!File.Exists(ArchivoClave))// Si el archivo de la clave no existe, lo crea (el "1234" es la clave por defecto)
                File.WriteAllText(ArchivoClave, "1234");
            Clave = File.ReadAllText(ArchivoClave); //Lee la clave del archivo y la asigna a la variable Clave

            if (!File.Exists(ArchivoMovimientos))
                File.Create(ArchivoMovimientos).Close();

            Saldo = double.Parse(File.ReadAllText(ArchivoSaldo)); //Lee el saldo del archivo y lo asigna a la variable Saldo

            string[] lineas = File.ReadAllLines(ArchivoMovimientos);
            movimientos.AddRange(lineas);
        }
        public bool VerificarClave(string claveIngresada)
        {
            return Clave == claveIngresada; //Compara la clave ingresada con la clave almacenada

        }
        public void CambiarClave(string ClaveActual, string NuevaClave)
        {
            if (ClaveActual != Clave) //Verifica si la clave actual es correcta
            {
                Console.WriteLine("Clave actual incorrecta");
                return;
            }
            Clave = NuevaClave; //Asigna la nueva clave a la variable Clave
            File.WriteAllText(ArchivoClave, NuevaClave); //Escribe la nueva clave en el archivo
            Console.WriteLine("Clave cambiada con exito");
            RegistrarMovimiento($"Cambio de clave | {DateTime.Now}");
            Console.ReadKey();

        }
        public void Depositar(double Monto)
        {
            Saldo += Monto; //Suma el monto al saldo
            RegistrarMovimiento($"Depósito: {Monto} | {DateTime.Now}");
            Console.WriteLine("Deposito realizado con exito");
            Console.ReadKey();
            Console.WriteLine($"Se ha depositado {Monto}. Nuevo saldo: {Saldo}");
            GuardarSaldo(); //Guarda el saldo en el archivo
            Console.ReadKey();
        }
        public void Retirar(double Monto)//Este metodo retira dinero de la cuenta
        {
            if (Monto <= Saldo)
            {
                Saldo -= Monto; //Resta el monto al saldo
                RegistrarMovimiento($"Retiro: {Monto} | {DateTime.Now}");
            }
            else
            {
                Console.WriteLine("Saldo insuficiente para realizar el retiro.");
            }
            Console.WriteLine("Retiro realizado con exito");
            Console.ReadKey();
            Console.WriteLine($"Se ha retirado {Monto}. Nuevo saldo: {Saldo}");//Muestra el nuevo saldo
            GuardarSaldo(); //Guarda el saldo en el archivo
        }
        public void ConsultarSaldo()
        {
            Console.WriteLine($"Su saldo actual es: {Saldo}");//Muestra el saldo
            RegistrarMovimiento($"Consulta de saldo: {Saldo} | {DateTime.Now}");//Registra el movimiento
            Console.ReadKey();
        }
        public void ConsultarMovimientos()
        {
            Console.WriteLine("=====ULTIMOS MOVIMIENTOS=====");
            foreach (var movimiento in movimientos)
            {
                Console.WriteLine(movimiento);//Muestra cada movimiento
            }
            Console.ReadKey();
        }
        private void RegistrarMovimiento(string movimiento)
        {
            string Registro = $"cuenta {NumeroCuenta}: {movimiento} | {DateTime.Now}";
            movimientos.Add(Registro); //Agrega el movimiento a la lista
            File.AppendAllText(ArchivoMovimientos, Registro + Environment.NewLine); //Escribe el movimiento en el archivo
        }
        private void GuardarSaldo()
        {
            File.WriteAllText(ArchivoSaldo, Saldo.ToString()); //Escribe el saldo en el archivo
        }

    }
}
