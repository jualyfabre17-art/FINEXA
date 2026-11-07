using Finexa.Domain.Calculations; 
using Finexa.Domain.Interfaces;

IAmortization frenchMethod = new FrenchAmortization();
IAmortization germanMethod = new GermanAmortization();

decimal monto = 1000m;
decimal tasaAnual = 0.12m; 
int plazo = 12;
DateTime fechaInicio = new DateTime(2024, 1, 1);

var tablaFrancesa = frenchMethod.CalculateAmortization(monto, tasaAnual, plazo, fechaInicio);

decimal sumaCapitalFrances = tablaFrancesa.Sum(fila => fila.Principal);

Console.WriteLine("--- PRUEBA METODO FRANCES ---");
Console.WriteLine($"Monto Original: {monto}");
Console.WriteLine($"Suma de Capitales en la tabla: {sumaCapitalFrances}");
Console.WriteLine($"Cantidad de cuotas: {tablaFrancesa.Count}");
Console.WriteLine();

var tablaAlemana = germanMethod.CalculateAmortization(monto, tasaAnual, plazo, fechaInicio);
decimal sumaCapitalAleman = tablaAlemana.Sum(fila => fila.Principal);

Console.WriteLine("--- PRUEBA METODO ALEMAN ---");
Console.WriteLine($"Monto Original: {monto}");
Console.WriteLine($"Suma de Capitales en la tabla: {sumaCapitalAleman}");
Console.WriteLine($"Cantidad de cuotas: {tablaAlemana.Count}");

Console.WriteLine("--- VERIFICACION A MANO (METODO FRANCES) ---");
var primeraCuota = tablaFrancesa.First();
Console.WriteLine($"Cuota #1: Capital = {primeraCuota.Principal}, Interes = {primeraCuota.Interest}");

Console.WriteLine("\n--- PRUEBA 2: TASA 0% ---");
var tablaCero = frenchMethod.CalculateAmortization(1000m, 0m, 10, new DateTime(2024, 1, 1));
Console.WriteLine($"Suma Capital Frances (0%): {tablaCero.Sum(f => f.Principal)}"); 

var tablaCeroAleman = germanMethod.CalculateAmortization(1000m, 0m, 10, new DateTime(2024, 1, 1));
Console.WriteLine($"Suma Capital Aleman (0%): {tablaCeroAleman.Sum(f => f.Principal)}"); 

Console.WriteLine("\n--- PRUEBA 3: MONTO Y PLAZO EXTREMOS ---");
var tablaExtrema = frenchMethod.CalculateAmortization(50000m, 0.60m, 60, new DateTime(2024, 1, 1));
Console.WriteLine($"Suma Capital Frances (60 meses): {tablaExtrema.Sum(f => f.Principal)}"); 

var tablaExtremaAleman = germanMethod.CalculateAmortization(50000m, 0.60m, 60, new DateTime(2024, 1, 1));
Console.WriteLine($"Suma Capital Aleman (60 meses): {tablaExtremaAleman.Sum(f => f.Principal)}");