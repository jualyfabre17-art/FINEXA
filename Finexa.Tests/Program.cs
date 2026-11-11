/*IAmortization frenchMethod = new FrenchAmortization();
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
Console.WriteLine($"Suma Capital Aleman (60 meses): {tablaExtremaAleman.Sum(f => f.Principal)}");*/
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;
using Finexa.Domain.Enums;
using Finexa.Domain.Exceptions;
using Finexa.Domain.ValueObjects;

/*Console.WriteLine("--- INICIO PRUEBAS ---\n");

Console.WriteLine("1. SIMULANDO PRESTAMO DE 3 CUOTAS (Flujo Feliz)");
var prestamo = new Loan(1, new Money(100, "DOP"), 0.12m, 3);
prestamo.CalculateLoan();
decimal montoCuota = prestamo.Installments.First().Amount.Amount;
Console.WriteLine($"Estado inicial: {prestamo.Status}"); 

prestamo.RegisterPayment(montoCuota, DateTime.Now);
Console.WriteLine($"Pago 1 realizado. Estado: {prestamo.Status}"); 

prestamo.RegisterPayment(montoCuota, DateTime.Now);
Console.WriteLine($"Pago 2 realizado. Estado: {prestamo.Status}"); 

prestamo.RegisterPayment(montoCuota, DateTime.Now);
Console.WriteLine($"Pago 3 realizado. Estado: {prestamo.Status}\n"); 


Console.WriteLine("2. INTENTO DE PAGO INSUFICIENTE");
try
{
    var prestamo2 = new Loan(2, new Money(100, "DOP"), 0.12m, 3);
    prestamo2.CalculateLoan();

    prestamo2.RegisterPayment(10m, DateTime.Now);
}
catch (Exception ex)
{
    Console.WriteLine($"RECHAZADO CORRECTAMENTE: {ex.Message}\n");
}


Console.WriteLine("3. INTENTO DE PAGO EXTRA TRAS SALDAR");
try
{
    prestamo.RegisterPayment(montoCuota, DateTime.Now);
}
catch (Exception ex)
{
    Console.WriteLine($"RECHAZADO CORRECTAMENTE: {ex.Message}\n");
}


Console.WriteLine("4. INTENTO DE ANULACIÓN CON PAGOS");
try
{
    var prestamo4 = new Loan(4, new Money(100, "DOP"), 0.12m, 3);
    prestamo4.CalculateLoan();

    prestamo4.RegisterPayment(montoCuota, DateTime.Now);

    prestamo4.CancelLoan();
}
catch (Exception ex)
{
    Console.WriteLine($"RECHAZADO CORRECTAMENTE: {ex.Message}\n");
}


Console.WriteLine("5. ANULACIÓN SIN PAGOS (Debe funcionar)");
try
{
    var prestamo5 = new Loan(5, new Money(100, "DOP"), 0.12m, 3);
    
    prestamo5.CancelLoan();
    Console.WriteLine($"ANULADO CORRECTAMENTE. Estado final: {prestamo5.Status}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR INESPERADO: {ex.Message}\n");
}

Console.WriteLine("--- FIN PRUEBAS ---");*/
Console.WriteLine("¡El programa se ha iniciado correctamente!");


public class FakeDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public class FakeUnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync()
    {
        return Task.FromResult(1);
    }
}

public class FakePartnerRepository : IPartnerRepository
{
    private readonly List<Partner> _partners = new();

    public Task AddPartnerAsync(Partner partner, CancellationToken ct = default)
    {
        _partners.Add(partner);
        return Task.CompletedTask;
    }

    public Task<Partner?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var partner = _partners.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(partner);
    }

    public Task<IReadOnlyList<Partner>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
    {
        var result = _partners.Where(p => ids.Contains(p.Id)).ToList();

        return Task.FromResult<IReadOnlyList<Partner>>(result);
    }
}

public class FakeLoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans = new();

    public Task AddLoanAsync(Loan loan, CancellationToken ct = default)
    {
        _loans.Add(loan);
        return Task.CompletedTask;
    }

    public Task<int> CountPartnerLoanAsync(int partnerId, CancellationToken ct = default)
    {
        var count = _loans.Count(l => l.PartnerId == partnerId && l.Status == Status.Active);
        return Task.FromResult(count);
    }

    public Task<Loan?> GetLoanByIdAsync(int id, CancellationToken ct = default)
    {
        var loan = _loans.FirstOrDefault(l => l.Id == id);
        return Task.FromResult(loan);
    }

    public Task<IReadOnlyList<Loan>> ListOverdueLoansAsync(DateTime dueDate, CancellationToken ct = default)
    {
        var result = _loans.Where(l =>
            l.Installments.Any(i => !i.IsPaid && i.DueDate < dueDate)
        ).ToList();

        return Task.FromResult<IReadOnlyList<Loan>>(result);
    }
}