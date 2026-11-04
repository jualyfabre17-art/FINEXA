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
    }
}
