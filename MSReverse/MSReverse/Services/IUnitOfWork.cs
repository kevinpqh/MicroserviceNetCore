using MSReverse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSReverse.Services
{
    public interface IUnitOfWork
    {
        ICreditRepository Credit { get; }
    }
}
