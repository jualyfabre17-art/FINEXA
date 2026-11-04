using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Domain.Entities
{
    public class Guarantor
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int PartnerId { get; private set; }

        private Guarantor() { }

        public Guarantor(string name, int partnerId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nombre del garante invalido", nameof(name));
            if (partnerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(partnerId), "Id del socio invalido");

            Name = name;
            PartnerId = partnerId;
        }
        
    }
}
