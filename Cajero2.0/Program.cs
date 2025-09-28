using Cajero;


class program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Ingrese su numero de cuenta:");
        string numeroCuenta = Console.ReadLine();

        CuentaBancaria Cuenta = new CuentaBancaria(numeroCuenta); // Crea una instancia de CuentaBancaria

        //Comenzaremos con el proceso de autenticacion

        int intentos = 0;
        bool acceso = false;
        while (intentos < 3 && !acceso)
        {
            Console.WriteLine("Ingrese su clave:");
            string claveIngresada = Console.ReadLine();
            if (Cuenta.VerificarClave(claveIngresada))
            {
                acceso = true;
                Console.WriteLine("Acceso concedido. Bienvenido a su cuenta.");
                Console.ReadKey();
            }
            else
            {
                intentos++;
                Console.WriteLine("Clave incorrecta. Intentos restantes: " + (3 - intentos));
                Console.ReadKey();
            }
        }

        int opcion = 0;
        do
        {
            if (!acceso)
            {
                Console.WriteLine("Demasiados intentos fallidos. Saliendo del sistema.");
                break;
            }
            Console.WriteLine("____________________________________");
            Console.WriteLine("|       == MENÚ PRINCIPAL ==       |");
            Console.WriteLine("|----------------------------------|");
            Console.WriteLine("|1.         Depositar              |");
            Console.WriteLine("|__________________________________|");
            Console.WriteLine("|2.          Retirar               |");
            Console.WriteLine("|__________________________________|");
            Console.WriteLine("|3.       Consultar Saldo          |");
            Console.WriteLine("|__________________________________|");
            Console.WriteLine("|4.  Consultar ultimos movimientos |");
            Console.WriteLine("|__________________________________|");
            Console.WriteLine("|5.        Cambiar Clave           |");
            Console.WriteLine("|__________________________________|");
            Console.WriteLine("|6.            Salir               |");
            Console.WriteLine("|__________________________________|");

            opcion = int.Parse(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Ingrese el monto a depositar:");
                    double MontoDeposito = double.Parse(Console.ReadLine());
                    Cuenta.Depositar(MontoDeposito);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Ingrese el monto a retirar:");
                    double MontoRetiro = double.Parse(Console.ReadLine());
                    Cuenta.Retirar(MontoRetiro);
                    break;
                case 3:
                    Console.Clear();
                    Cuenta.ConsultarSaldo();
                    break;
                case 4:
                    Console.Clear();
                    Cuenta.ConsultarMovimientos();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Ingrese su clave actual:");
                    string ClaveActual = Console.ReadLine();
                    Console.WriteLine("Ingrese su nueva clave:");
                    string NuevaClave = Console.ReadLine();
                    Cuenta.CambiarClave(ClaveActual, NuevaClave);
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Gracias por usar el cajero. ¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        } while (opcion != 6 && acceso);
    }
}