using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Enums;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Entities
{
    public class SavingTransaction
    {
        public int Id { get; private set; }
        public int PartnerId { get; private set; }
        public Money Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public TransactionType Type { get; private set; } 

        private SavingTransaction() { }

        public SavingTransaction(int partnerId, Money amount, DateTime transactionDate, TransactionType type)
        {
            if (partnerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(partnerId), "Id socio invalido, asegurese sea mayor a 0");
            if (amount == null)
                throw new ArgumentNullException(nameof(amount), "El monto a guardar es invalido");
            if (amount.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Cantidad a guardar invalida");
            

            PartnerId = partnerId;
            Amount = amount;
            TransactionDate = transactionDate;
            Type = type;
        }

    }
}

