using Cajero;


class program
{
    static void Main(string[] args)

    {

        Console.WriteLine("Ingrese su numero de cuenta:");
        string NumeroCuenta = Console.ReadLine();

        CuentaBancaria Cuenta = new CuentaBancaria("123456789"); // Crea una instancia de CuentaBancaria
        CuentaBancaria cuenta2 = new CuentaBancaria("987654321"); // Crea una segunda instancia de CuentaBancaria
        //Comenzaremos con el proceso de autenticacion

        int intentos = 0;//Contador de intentos
        bool acceso = false;//Variable que indica si el usuario tiene acceso o no
        while (intentos < 3 && !acceso)//El usuario tiene 3 intentos para ingresar la clave
        {
            Console.WriteLine("Ingrese su clave:");
            string claveIngresada = Console.ReadLine();//Lee la clave ingresada por el usuario
            if (Cuenta.VerificarClave(claveIngresada))//Verifica si la clave es correcta
            {
                acceso = true;//Si la clave es correcta, el usuario tiene acceso
                Console.WriteLine("Acceso concedido. Bienvenido a su cuenta.");
                Console.ReadKey();//Espera a que el usuario presione una tecla
            }
            else//Si la clave es incorrecta
            {
                intentos++;//Incrementa el contador de intentos
                Console.WriteLine("Clave incorrecta. Intentos restantes: " + (3 - intentos));//Muestra los intentos restantes
                Console.ReadKey();//Espera a que el usuario presione una tecla
            }
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
                    if(double.TryParse(Console.ReadLine(), out double MontoDeposito)) 
                    {
                    Cuenta.Depositar(MontoDeposito);//Llama al metodo Depositar
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
                        Cuenta.Retirar(MontoRetiro);//Llama al metodo Retirar
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
                    Cuenta.ConsultarSaldo();//Llama al metodo ConsultarSaldo
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    Cuenta.ConsultarMovimientos();//Llama al metodo ConsultarMovimientos
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Ingrese su clave actual:");
                    string ClaveActual = Console.ReadLine();//Lee la clave actual
                    Console.WriteLine("Ingrese su nueva clave:");
                    string NuevaClave = Console.ReadLine();//Lee la nueva clave
                    Cuenta.CambiarClave(ClaveActual, NuevaClave);//Llama al metodo CambiarClave
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