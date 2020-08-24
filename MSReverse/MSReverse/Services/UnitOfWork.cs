using MSReverse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSReverse.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string cnString)
        {
            Credit = new CreditRepository(cnString);
        }

        public ICreditRepository Credit { get; private set; }
    }
}
