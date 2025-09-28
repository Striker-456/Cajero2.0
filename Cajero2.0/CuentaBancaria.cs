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
        public string NumeroCuenta { get; private set; }// El numero de cuenta es publico para que pueda ser accedido desde fuera de la clase
        private string Clave;// La clave es privada para que no pueda ser accedida desde fuera de la clase
        public double Saldo { get; private set; }// El saldo es publico para que pueda ser accedido desde fuera de la clase pero solo puede ser modificado desde dentro de la clase

        private List<string> Movimientos;
        private string ArchivoMovimientos;
        private string ArchivoClave;
        private string ArchivoSaldo;
        public string ArchivoCuenta;


        public CuentaBancaria(string NumeroCuenta)
        {
            this.NumeroCuenta = NumeroCuenta;// Asigna el numero de cuenta a la variable de instancia
            ArchivoCuenta = $"C:\\Users\\hecto\\Downloads\\Cajero2.0\\usuario{NumeroCuenta}";//Ruta del directorio de la cuenta
            ArchivoSaldo =$"C:\\Users\\hecto\\Downloads\\Cajero2.0\\usuario{NumeroCuenta}\\Saldo.txt";//Ruta del archivo de saldo
            ArchivoClave =$"C:\\Users\\hecto\\Downloads\\Cajero2.0\\usuario{NumeroCuenta}\\clave.txt";//Ruta del archivo de clave
            ArchivoMovimientos = $"C:\\Users\\hecto\\Downloads\\Cajero2.0\\\\usuario{NumeroCuenta}\\movimientos.txt";//Ruta del archivo de movimientoss

            Movimientos = new List<string>();   //Con este comando esta inicializando la lista

            if (!Directory.Exists(ArchivoCuenta))// Si el directorio no existe, lo crea
                Directory.CreateDirectory(ArchivoCuenta);

            if (!File.Exists(ArchivoSaldo))//Si el archivo de saldo no existe,lo crea
                File.WriteAllText(ArchivoSaldo, "0");
            Saldo = double.Parse(File.ReadAllText(ArchivoSaldo)); //Lee el saldo del archivo y lo asigna a la variable Saldo

            if (!File.Exists(ArchivoClave))// Si el archivo de la clave no existe, lo crea (el "1234" es la clave por defecto)
                File.WriteAllText(ArchivoClave, "1234");
            Clave = File.ReadAllText(ArchivoClave); //Lee la clave del archivo y la asigna a la variable Clave

            if (!File.Exists(ArchivoMovimientos))// Si el archivo de movimientos no existe, lo crea
                File.Create(ArchivoMovimientos).Close();

            Saldo = double.Parse(File.ReadAllText(ArchivoSaldo)); //Lee el saldo del archivo y lo asigna a la variable Saldo

            string[] lineas = File.ReadAllLines(ArchivoMovimientos);//Lee todas las lineas del archivo de movimientos y las asigna a un array
            Movimientos.AddRange(lineas);
        }
        public bool VerificarClave(string claveIngresada)//Este metodo verifica si la clave ingresada es correcta
        {
            return Clave == claveIngresada; //Compara la clave ingresada con la clave almacenada

        }
        public void CambiarClave(string ClaveActual, string NuevaClave)//Este metodo cambia la clave de la cuenta
        {
            if (ClaveActual != Clave) //Verifica si la clave actual es correcta
            {
                Console.WriteLine("Clave actual incorrecta");
                return;
            }
            Clave = NuevaClave; //Asigna la nueva clave a la variable Clave
            File.WriteAllText(ArchivoClave, NuevaClave); //Escribe la nueva clave en el archivo
            Console.WriteLine("Clave cambiada con exito");
            RegistrarMovimiento($"Cambio de clave ");
            Console.ReadKey();
        }
        public void Depositar(double Monto)//Este metodo deposita dinero en la cuenta
        {
            Console.Clear();
            Saldo += Monto; //Suma el monto al saldo
            RegistrarMovimiento($"Depósito: {Monto}");//Registra el movimiento
            Console.WriteLine("Deposito realizado con exito");//Mensaje de exito
            Console.ReadKey();//Espera a que el usuario presione una tecla
            Console.WriteLine($"Se ha depositado {Monto}. Nuevo saldo: {Saldo}");// Muestra el nuevo saldo
            GuardarSaldo(); //Guarda el saldo en el archivo
            Console.ReadKey();//Espera a que el usuario presione una tecla
        }
        public void Retirar(double Monto)//Este metodo retira dinero de la cuenta
        {
            if (Monto <= Saldo)//Verifica si hay saldo suficiente
            {
                Saldo -= Monto; //Resta el monto al saldo
                RegistrarMovimiento($"Retiro: {Monto}");//Registra el movimiento
            }
            else
            {
                Console.WriteLine("Saldo insuficiente para realizar el retiro.");//Mensaje de error
            }
            Console.WriteLine("Retiro realizado con exito");//Mensaje de exito
            Console.ReadKey();//Espera a que el usuario presione una tecla
            Console.WriteLine($"Se ha retirado {Monto}. Nuevo saldo: {Saldo}");//Muestra el nuevo saldo
            GuardarSaldo(); //Guarda el saldo en el archivo
        }
        public void ConsultarSaldo()//Este metodo consulta el saldo de la cuenta
        {
            Console.WriteLine($"Su saldo actual es: {Saldo}");//Muestra el saldo
            RegistrarMovimiento($"Consulta de saldo: {Saldo}");//Registra el movimiento
            Console.ReadKey();//Espera a que el usuario presione una tecla
        }
        public void ConsultarMovimientos()//Este metodo consulta los ultimos movimientos de la cuenta
        {
            Console.Clear();
            Console.WriteLine("\n=====ULTIMOS MOVIMIENTOS=====");//Muestra el titulo
            foreach (var Movimiento in Movimientos)//Recorre la lista de movimientos
            {
                Console.WriteLine(Movimiento);//Muestra cada movimiento
            }
            Console.ReadKey();//Espera a que el usuario presione una tecla
        }
        private void RegistrarMovimiento(string Movimiento)//Este metodo registra un movimiento en la lista y en el archivo
        {
            string Registro = $"cuenta {NumeroCuenta}: {Movimiento} | {DateTime.Now}";//Crea el registro del movimiento
            Movimientos.Add(Registro); //Agrega el movimiento a la lista
            File.AppendAllText(ArchivoMovimientos, Registro + Environment.NewLine); //Escribe el movimiento en el archivo
        }
        private void GuardarSaldo()//Este metodo guarda el saldo en el archivo
        {
            File.WriteAllText(ArchivoSaldo, Saldo.ToString()); //Escribe el saldo en el archivo
        }

    }
}
