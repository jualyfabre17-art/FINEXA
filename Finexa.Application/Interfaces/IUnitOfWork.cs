using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Application.Interfaces
{
    public interface IUnitOfWork
    {
       Task<int> SaveChangesAsync(); 
    }
}
