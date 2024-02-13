using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static int tamano = 15;
    static int[] numeroPago = new int[tamano];
    static DateTime[] fecha = new DateTime[15];
    static TimeOnly[] hora = new TimeOnly[15];
    static string[] Placa = new string[15];
    static int[] numeroCaseta = new int[15];
    static int[] tipoVehiculo = new int[15];
    static int[] numeroFactura = new int[15];
    static double[] montoPagar = new double[15];
    static double[] montopago = new double[15];
    static double[] montoDeducido = new double[15];
    static double[] montoPagaCliente = new double[15];
    static double[] vuelto = new double[15];
    static int indice = 0;


    static void Main()
    {
        MenuPrincipal();
    }
    static void MenuPrincipal()
    {
        try
        {            
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*****Pago de peajes Autopistas del Sol S.A*****");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("1. Inicializar Vectores");
                Console.WriteLine("2. Ingresar Paso Vehicular");
                Console.WriteLine("3. Consultar Pagos X Numero de Pago");
                Console.WriteLine("4. Modificar Pagos X Numero de Pago");
                Console.WriteLine("5. Ver todos los pagos");
                Console.WriteLine("6. Salir");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Ingrese una opción: ");
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            InicializarVectores();
                            break;
                        case 2:
                            IngresarVehiculo();
                            break;
                        case 3:
                            ConsultarPagos();
                            break;
                        case 4:
                            ModificarPagos();
                            break;
                        case 5:
                            VerTodosLosPagos();
                            break;
                        case 6:
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Intente de nuevo.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    Console.ReadKey();
                }
            } while (opcion != 7);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error en el Menú: " + e.Message);
            Console.ReadKey();
            MenuPrincipal();
        }
    }

    static void InicializarVectores()
    {
        try
        {
            Console.Clear();

            numeroPago = Enumerable.Repeat(0, tamano).ToArray<int>(); 
            fecha = Enumerable.Repeat(DateTime.Parse("01/01/0001"), tamano).ToArray<DateTime>(); 
            hora = Enumerable.Repeat(TimeOnly.Parse("00:00"), tamano).ToArray<TimeOnly>();
            Placa = Enumerable.Repeat("", tamano).ToArray<string>();
            numeroCaseta = Enumerable.Repeat(0, tamano).ToArray<int>();
            tipoVehiculo = Enumerable.Repeat(0, tamano).ToArray<int>();
            numeroFactura = Enumerable.Repeat(0, tamano).ToArray<int>();
            montoPagar = Enumerable.Repeat(0.0, tamano).ToArray<double>();
            montoPagaCliente = Enumerable.Repeat(0.0, tamano).ToArray<double>();
            vuelto = Enumerable.Repeat(0.0, tamano).ToArray<double>();
            Console.WriteLine("Vectores inicializados.");

            Console.ReadKey();
            MenuPrincipal();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al inicializar vectores: " + e.Message);
            Console.ReadKey();
            MenuPrincipal();
        }
    }

    static void IngresarVehiculo()
    {
        int posicion = 0;

        try
        {
            string continuar = "S";            

            do
            { 
                Console.Clear();

                posicion = Consecutivo();

                if (posicion == 15)
                {
                    Console.WriteLine("Vectores Llenos");
                    return;
                }
                else
                {
                    numeroPago[posicion] = posicion + 1;

                    Console.WriteLine("Numero de pago:  " + (posicion + 1).ToString());

                    Console.WriteLine("Ingrese la fecha (ej: '02/02/2024'):  ");
                    fecha[posicion] = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese la hora (ej: '14:00'):  "); 
                    hora[posicion] = TimeOnly.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese la placa del vehiculo (ej: '123456'):  "); 
                    Placa[posicion] = Console.ReadLine();

                    Console.WriteLine("Tipo de vehiculo ([1] Moto, [2] Vehiculo liviano, [3] Vehiculo pesado, [4] Autobus):  "); 
                    int tVehiculo = int.Parse(Console.ReadLine());

                    if ((tVehiculo < 0) || (tVehiculo > 4))
                    {
                        Console.WriteLine("El Tipo de vehiculo debe ser un número válido de 1 a 4");
                        Console.ReadKey();
                        return;
                    }
                    tipoVehiculo[posicion] = tVehiculo;

                    Console.WriteLine("Numero de Caseta ([1] Caseta1, [2] Caseta2, [3] Caseta3): ");
                    int nCaja = int.Parse(Console.ReadLine());
                    if ((nCaja < 0) || (nCaja > 3))
                    {
                        Console.WriteLine("El numero de Caseta debe ser un número válido del 1 a 3");
                        Console.ReadKey();
                        return;
                    }
                    numeroCaseta[posicion] = nCaja;

                    Console.WriteLine("Ingrese el numero de factura:  "); 
                    numeroFactura[posicion] = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el monto a pagar (500 moto, liviano 700, Pesado 2700, Autobus 3700):  ");
                    double mPagar = 0;
                    mPagar = double.Parse(Console.ReadLine());
                    montoPagar[posicion] = mPagar;

                    double pago = 0;
                    switch (tVehiculo)
                    {
                        case 1:
                            pago = mPagar + 500; // Motocicleta
                            break;
                        case 2:
                            pago = mPagar + 700; // Vehiculo liviano
                            break;
                        case 3:
                            pago = mPagar + 2700; // Vehiculo Pesado
                            break;
                        case 4:
                            pago = mPagar + 3700; // Autobus
                            break;
                    }
                    Console.WriteLine("Monto a pagar:  " + mPagar);
                    montopago[posicion] = pago;

                    Console.WriteLine("Ingrese el monto de pago cliente:  "); 
                    double PagoCliente = double.Parse(Console.ReadLine());                 
                    if (PagoCliente < mPagar)
                    {
                        Console.WriteLine("El Pago del Cliente es menor que el monto adeudado");
                        Console.ReadKey();
                        return;
                    }
                    montoPagaCliente[posicion] = PagoCliente;
                                        
                    double vueltoCliente = PagoCliente - mPagar;
                    vuelto[posicion] = vueltoCliente;
                    Console.WriteLine("El vuelto es de:  " + vueltoCliente);

                    Console.WriteLine("Pago registrado correctamente.");
                    Console.ReadKey();
                }

                do 
                {
                    Console.Clear();
                    Console.WriteLine("Desea continuar (S/N)");
                    string input = Console.ReadLine();
                    continuar = input.ToUpper();
                } while ((continuar != "S") && (continuar != "N"));

            } while (continuar != "N");

        }
        catch (Exception e)
        {
            Console.WriteLine("Error al guardar la información: " + e.Message);
            limpiarVector(posicion);
            Console.ReadKey();
            MenuPrincipal();
        }
    }

    static void limpiarVector(int pos)
    {
        numeroPago[pos] = 0;
        fecha[pos] = DateTime.Parse("01/01/0001");
        hora[pos] = TimeOnly.Parse("00:00");
        Placa[pos] = "";
        numeroCaseta[pos] = 0;
        tipoVehiculo[pos] = 0;
        numeroFactura[pos] = 0;
        montoPagar[pos] = 0.0;
        montopago[pos] = 0.0;
        montoDeducido[pos] = 0.0;
        montoPagaCliente[pos] = 0.0;
        vuelto[pos] = 0.0;
    }

    public static int Consecutivo()
    {
        try
        { 
            for (int i = 0; i < 10; i++)
            {
                if (numeroPago[i] == 0)
                {
                    return i;
                }
            }

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            Console.ReadKey();
            return 10;         
        }
    }
    static void ConsultarPagos()
    {
        try
        {
            Console.Clear();

            Console.WriteLine("Digite el Número de Pago");
            int Nume = int.Parse(Console.ReadLine());

            if (Nume > 15)
            {
                Console.WriteLine("El número de Pago no puede ser mayor a 15");
                Console.ReadKey();
                return;
            }

            int posVector = Nume - 1;

            if (numeroPago[posVector] == 0)
            {
                Console.WriteLine("Pago no se encuentra Registrado");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Dato encontrado en la posición: " + posVector);
                Console.WriteLine("Presione cualquier tecla para ver Registo");
                Console.WriteLine("");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Número de Pago     :  " + numeroPago[posVector]);
                Console.WriteLine("Fecha              :  " + fecha[posVector].ToShortDateString());
                Console.WriteLine("Hora               :  " + hora[posVector]);
                Console.WriteLine("Placa             :  " + Placa[posVector]);
                Console.WriteLine("Número de Caja     :  " + numeroCaseta[posVector]);
                Console.WriteLine("Tipo de Vehiculo   :  " + tipoVehiculo[posVector]);
                Console.WriteLine("Número de Factura  :  " + numeroFactura[posVector]);
                Console.WriteLine("Monto a Pagar      :  " + montoPagar[posVector]);
                Console.WriteLine("Monto Pago Cliente :  " + montoPagaCliente[posVector]);
                Console.WriteLine("Vuelto             :  " + vuelto[posVector]);

                Console.ReadKey();
                MenuPrincipal();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error cargando los datos: " + e.Message);
            Console.ReadKey();      
        }
    }
    static void ModificarPagos()
    {
            Console.Clear();

            Console.WriteLine("Digite el Número de Pago");
            int Nume = int.Parse(Console.ReadLine());

            if (Nume > 15)
            {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("El número de Pago no puede ser mayor a 15");
            Console.ReadKey();
                return;
            }

            int posVector = Nume - 1;

            if (numeroPago[posVector] == 0)
            {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Pago no se encuentra Registrado");
            Console.ReadKey();
                return;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Dato encontrado en la posición: " + posVector);
                Console.WriteLine("");
                Console.WriteLine("Número de Pago     :  " + numeroPago[posVector]);
                Console.WriteLine("Fecha              :  " + fecha[posVector].ToShortDateString());
                Console.WriteLine("Hora               :  " + hora[posVector]);
                Console.WriteLine("Placa             :  " + Placa[posVector]);
                Console.WriteLine("Número de Caja     :  " + numeroCaseta[posVector]);
                Console.WriteLine("Tipo de Vehiculo   :  " + tipoVehiculo[posVector]);
                Console.WriteLine("Número de Factura  :  " + numeroFactura[posVector]);
                Console.WriteLine("Monto a Pagar      :  " + montoPagar[posVector]);
                Console.WriteLine("Monto Pago Cliente :  " + montoPagaCliente[posVector]);
                Console.WriteLine("Vuelto             :  " + vuelto[posVector]);
                Console.WriteLine("Presione cualquier tecla para modificar Registo");
                Console.ReadKey();
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Numero de pago a Modificar:  " + Nume);

                Console.WriteLine("Ingrese la fecha (ej: '02/02/2024'):  ");
                fecha[posVector] = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Ingrese la hora (ej: '14:00'):  ");
                hora[posVector] = TimeOnly.Parse(Console.ReadLine());

                Console.WriteLine("Ingrese la Placa del vehiculo (ej: '123456'):  ");
                Placa[posVector] = Console.ReadLine();

            int posicion = 0;
            posicion = Consecutivo();
                    Console.WriteLine("Tipo de vehiculo ([1] Moto, [2] Vehiculo liviano, [3] Vehiculo pesado, [4] Autobus):  ");
                int tVehiculo = int.Parse(Console.ReadLine());

                if ((tVehiculo < 0) || (tVehiculo > 4))
                {
                    Console.WriteLine("El Tipo de vehiculo debe ser un número válido de 1 a 4");
                    Console.ReadKey();
                    return;
                }
                tipoVehiculo[posicion] = tVehiculo;

                Console.WriteLine("Numero de Caseta ([1] Caseta1, [2] Caseta2, [3] Caseta3): ");
                int nCaja = int.Parse(Console.ReadLine());
                if ((nCaja < 0) || (nCaja > 3))
                {
                    Console.WriteLine("El numero de Caseta debe ser un número válido del 1 a 3");
                    Console.ReadKey();
                    return;
                }
                numeroCaseta[posicion] = nCaja;

                Console.WriteLine("Ingrese el numero de factura:  ");
                numeroFactura[posicion] = int.Parse(Console.ReadLine());

                Console.WriteLine("Ingrese el monto a pagar (500 moto, liviano 700, Pesado 2700, Autobus 3700):  ");
                double mPagar = 0;
                mPagar = double.Parse(Console.ReadLine());
                montoPagar[posicion] = mPagar;

                double pago = 0;
                switch (tVehiculo)
                {
                    case 1:
                        pago = mPagar + 500; // Motocicleta
                        break;
                    case 2:
                        pago = mPagar + 700; // Vehiculo liviano
                        break;
                    case 3:
                        pago = mPagar + 2700; // Vehiculo Pesado
                        break;
                    case 4:
                        pago = mPagar + 3700; // Autobus
                        break;
                }
                Console.WriteLine("Monto a pagar:  " + mPagar);
                montopago[posicion] = pago;

                Console.WriteLine("Ingrese el monto de pago cliente:  ");
                double PagoCliente = double.Parse(Console.ReadLine());
                if (PagoCliente < mPagar)
                {
                    Console.WriteLine("El Pago del Cliente es menor que el monto adeudado");
                    Console.ReadKey();
                    return;
                }
                montoPagaCliente[posicion] = PagoCliente;

                double vueltoCliente = PagoCliente - mPagar;
                vuelto[posicion] = vueltoCliente;
                Console.WriteLine("El vuelto es de:  " + vueltoCliente);

                Console.WriteLine("Pago registrado correctamente.");
                Console.ReadKey();
            }
        }

    static void VerTodosLosPagos()
    {
        try
        {
            Console.Clear();
            double mTotal = 0;
            int contador = 0;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Reporte de todos los pagos");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("# Pago  Fecha      Hora       Placa      Monto a Pagar      Cliente Pago      Vuelto                 ");
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 10; i++)
            {
                if (numeroPago[i] == 0)
                {
                    i = 10;
                }
                else
                {
                    Console.WriteLine(numeroPago[i].ToString().PadRight(8) + fecha[i].ToShortDateString().PadRight(11) + hora[i].ToString().PadRight(10) + Placa[i].ToString().PadRight(16) + montoPagar[i].ToString().PadRight(19) + montoPagaCliente[i].ToString().PadRight(16) + vuelto[i].ToString().PadRight(16));
            mTotal = mTotal + montoPagar[i];
                    contador = contador + 1;
                }
            }

            
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Total de registros: " + contador + "                                                                     Monto Total: " + mTotal);
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error cargando los datos: " + e.Message);
            Console.ReadKey();      
        }
    }

    static void VerPagosPortipoVehiculo()
    {
        try
        {

            Console.Clear();

            Console.WriteLine("Digite el Tipo de Servicio ([1] recibo de luz, [2] servicio telefonico, [3] recibo de agua): ");
            int tVehiculo = int.Parse(Console.ReadLine());

            if (tVehiculo > 3)
            {
                Console.WriteLine("El Tipo de Servicio no puede ser mayor a 3");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            double mTotal = 0;
            int contador = 0;

            Console.WriteLine("Reporte de todos los pagos por Tipo de Servicio");
            Console.WriteLine("");
            Console.WriteLine("===============================================================================================================================");
            Console.WriteLine("# Pago  Fecha      Hora       Cédula      Nombre          Apellido 1          ApelLido 2          Monto Recibo          ");
            Console.WriteLine("===============================================================================================================================");

            for (int i = 0; i < 10; i++)
            {
                if (numeroPago[i] == 0)
                {
                    i = 10;
                }
                else
                {
                    if (tipoVehiculo[i] == tVehiculo)
                    {
                        Console.WriteLine(numeroPago[i].ToString().PadRight(8) + fecha[i].ToShortDateString().PadRight(11) + hora[i].ToString().PadRight(7) + Placa[i].ToString().PadRight(9) + montoPagar[i].ToString().PadRight(16));
                        mTotal = mTotal + montoPagar[i];
                        contador = contador + 1;
                    }
                }
            }

            Console.WriteLine("");
            Console.WriteLine("===============================================================================================================================");
            Console.WriteLine("Total de registros: " + contador + "                                                                     Monto Total: " + mTotal);
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error cargando los datos: " + e.Message);
            Console.ReadKey();
        }
    }

    static void VerPagosPorCodigoCaja()
    {
        try
        {

            Console.Clear();

            Console.WriteLine("Digite el Número de Caja (rango entre 1 y 3): ");
            int nCaja = int.Parse(Console.ReadLine());

            if (nCaja > 3)
            {
                Console.WriteLine("El Número de Caja no puede ser mayor a 3");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            double mTotal = 0;
            int contador = 0;

            Console.WriteLine("Reporte de todos los pagos por Código de Cajero");
            Console.WriteLine("");
            Console.WriteLine("===============================================================================================================================");
            Console.WriteLine("# Pago  Fecha      Hora       Cédula      Nombre          Apellido 1          ApelLido 2          Monto Recibo          ");
            Console.WriteLine("===============================================================================================================================");

            for (int i = 0; i < 10; i++)
            {
                if (numeroPago[i] == 0)
                {
                    i = 10;
                }
                else
                {
                    if (numeroCaseta[i] == nCaja)
                    {
                        Console.WriteLine(numeroPago[i].ToString().PadRight(8) + fecha[i].ToShortDateString().PadRight(11) + hora[i].ToString().PadRight(7) + Placa[i].ToString().PadRight(9) + montoPagar[i].ToString().PadRight(16));
                        mTotal = mTotal + montoPagar[i];
                        contador = contador + 1;
                    }
                }
            }

            Console.WriteLine("");
            Console.WriteLine("===============================================================================================================================");
            Console.WriteLine("Total de registros: " + contador + "                                                                     Monto Total: " + mTotal);
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error cargando los datos: " + e.Message);
            Console.ReadKey();
        }

    }

    static void VerDineropagoPorVehiculos()
    {
        try
        {
            Console.Clear();
            int cantMoto = 0;
            int cantLiviano = 0;
            int cantPesado = 0;
            int cantAutobus = 0;
            double pagoMoto = 0;
            double pagoLiviano = 0;
            double pagoPesado = 0;
            double pagoAutobus = 0;
            int contador = 0;

            Console.WriteLine("Reporte de dinero pago - Desglose por Tipo de Vehiculo");
            Console.WriteLine("");
            Console.WriteLine("===============================================================================");
            Console.WriteLine("ITEM              Cantidad de Transacciones         Total pago                 ");
            Console.WriteLine("===============================================================================");

            for (int i = 0; i < 10; i++)
            {
                if (numeroPago[i] == 0)
                {
                    i = 10;
                }
                else
                {
                    switch (tipoVehiculo[i])
                    {
                        case 1:
                            cantMoto = cantMoto + 1;
                            pagoMoto = pagoMoto + cantMoto;
                            break;
                        case 2:
                            cantLiviano = cantLiviano + 1;
                            pagoLiviano = cantLiviano + cantLiviano;
                            break;
                        case 3:
                            cantPesado = cantPesado + 1;
                            pagoPesado = cantPesado + cantPesado;
                            break;
                        case 4:
                            cantAutobus = cantAutobus + 1;
                            pagoAutobus = cantAutobus + cantAutobus;
                            break;
                    }

                    contador = contador + 1;
                }
            }

            Console.WriteLine("1-Moto   " + cantMoto.ToString().PadRight(34) + pagoMoto.ToString());
            Console.WriteLine("2-Vehiculo Liviano       " + cantLiviano.ToString().PadRight(34) + pagoLiviano.ToString());
            Console.WriteLine("3-Vehiculo Pesado        " + cantPesado.ToString().PadRight(34) + pagoPesado.ToString());
            Console.WriteLine("3-Vehiculo Autobus       " + cantPesado.ToString().PadRight(34) + pagoPesado.ToString());
            Console.WriteLine("===============================================================================");
            Console.WriteLine("");
            Console.WriteLine("Total:           " + contador.ToString().PadRight(34) + (cantLiviano + cantPesado + cantPesado).ToString());
            Console.WriteLine("");
            Console.WriteLine("===============================================================================");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error cargando los datos: " + e.Message);
            Console.ReadKey();
        }
    }
}