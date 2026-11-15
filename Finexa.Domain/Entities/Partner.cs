using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Entities
{
    public class Partner
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int IdDocument { get; private set; }
        public Money AccumulatedSavings { get; private set; }


        private Partner() { }

        public Partner( string name, int idDocument, Money initialSavings)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nombre de socio invalido", nameof(name));
            if (idDocument <=0)
                throw new ArgumentOutOfRangeException(nameof(idDocument), "Cedula de identidad invalida");
            if (initialSavings == null)
                throw new ArgumentNullException(nameof(initialSavings), "Monto de ahorros acumulados invalido");
            if (initialSavings.Amount < 0)
                throw new ArgumentOutOfRangeException(nameof(initialSavings), "Monto de ahorros acumulados invalido");

            Name = name;
            IdDocument = idDocument;
            AccumulatedSavings = initialSavings;
        }
        public void UpdateDetails(string name, int idDocument)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacio.", nameof(name));
            if (idDocument <= 0)
                throw new ArgumentOutOfRangeException(nameof(idDocument), "Documento invalido.");

            Name = name;
            IdDocument = idDocument;
        }

        public void Deposit(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount), "El monto no puede ser nulo.");
            if (amount.Amount <= 0)
                throw new ArgumentException("El monto a depositar debe ser mayor a 0.");

            AccumulatedSavings = new Money(AccumulatedSavings.Amount + amount.Amount, "DOP");
        }
    }
}
