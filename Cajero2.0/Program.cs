using Cajero;


class program
{
    static void Main(string[] args)

    {
        List<CuentaBancaria> cuentas = new List<CuentaBancaria>()//Lista de cuentas predefinidas
        {
            new CuentaBancaria("123456789", "Hector"),
            new CuentaBancaria("987654321", "Maria"),
        };
        CuentaBancaria CuentaSeleccionada = null;//Cuenta seleccionada por el usuario

        while (true)//Bucle para seleccionar la cuenta
        {
            Console.WriteLine("Ingrese su numero de cuenta:");
            string numeroCuenta = Console.ReadLine();


            if (numeroCuenta?.ToLower() == "Salir")//Si el usuario escribe "Salir", sale del sistema
            {
                Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
                return;
            }
            CuentaSeleccionada = cuentas.Find(c => c.NumeroCuenta == numeroCuenta);//Busca la cuenta en la lista de cuentas

            if (CuentaSeleccionada == null)//Si la cuenta no existe, muestra un mensaje de error
            {
                Console.WriteLine("Numero de cuenta no encontrado. Intente de nuevo o escriba 'Salir' para salir.");
                Console.ReadKey();
                Console.Clear();
            }
            else//Si la cuenta existe, sale del bucle
            {
                break;
            }
        }
        Console.Clear();



        int intentos = 0;//Contador de intentos
        bool acceso = false;//Variable que indica si el usuario tiene acceso o no
        while (intentos < 3 && !acceso)
        {
            Console.WriteLine("____________________________________");
            Console.WriteLine($"        Bienvenido {CuentaSeleccionada.Titular}");
            Console.WriteLine("____________________________________");
            Console.WriteLine("Ingrese su clave:");
            string claveIngresada = Console.ReadLine();
            Console.Clear();
            if (CuentaSeleccionada.VerificarClave(claveIngresada))
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

        if (!acceso)
        {
            Console.WriteLine("Demasiados intentos fallidos. Saliendo del sistema...");
            return;
        }
        Console.Clear();
        int opcion = 0;//Variable que almacena la opcion del menu
        do//Menu principal
        {
            if (!acceso)//Si el usuario no tiene acceso, sale del sistema
            {
                Console.WriteLine("Demasiados intentos fallidos. Saliendo del sistema.");
                break;
            }//Si el usuario tiene acceso, muestra el menu
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
            Console.WriteLine("|      Seleccione una opcion       |");
            Console.WriteLine("|__________________________________|");
            String entrada = Console.ReadLine();//Lee la opcion ingresada por el usuario


            if (!int.TryParse(entrada, out opcion))//Verifica si la entrada es un numero
            {
                Console.Clear();
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número del 1 al 6.");
                Console.ReadKey();
                Console.Clear();
                continue; // Vuelve al inicio del bucle
            }
            if (opcion < 1 || opcion > 6)
            {
                Console.Clear();
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                Console.ReadKey();
                Console.Clear();
                continue; // Vuelve al inicio del bucle
            }
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Ingrese el monto a depositar:");//Pide el monto a depositar
                    if (double.TryParse(Console.ReadLine(), out double MontoDeposito))
                    {
                        CuentaSeleccionada.Depositar(MontoDeposito);//Llama al metodo Depositar
                        Console.Clear();
                    }
                    else
                    {

                        Console.WriteLine("Monto inválido. Por favor, ingrese un número válido.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Ingrese el monto a retirar:");
                    if (double.TryParse(Console.ReadLine(), out double MontoRetiro))
                    {
                        CuentaSeleccionada.Retirar(MontoRetiro);//Llama al metodo Retirar
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Monto inválido. Por favor, ingrese un número válido.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case 3:
                    Console.Clear();
                    CuentaSeleccionada.ConsultarSaldo();//Llama al metodo ConsultarSaldo
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    CuentaSeleccionada.ConsultarMovimientos();//Llama al metodo ConsultarMovimientos
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Ingrese su clave actual:");
                    string ClaveActual = Console.ReadLine();//Lee la clave actual
                    Console.WriteLine("Ingrese su nueva clave:");
                    string NuevaClave = Console.ReadLine();//Lee la nueva clave
                    CuentaSeleccionada.CambiarClave(ClaveActual, NuevaClave);//Llama al metodo CambiarClave
                    Console.Clear();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Gracias por usar el cajero. ¡Hasta luego!");

                    break;
                default://Si la opcion no es valida
                    Console.Clear();
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        } while (opcion != 6 && acceso);//El menu se repite hasta que el usuario elija salir o no tenga acceso
    }
}