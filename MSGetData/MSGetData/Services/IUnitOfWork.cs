using MSGetData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSGetData.Services
{
    public interface IUnitOfWork
    {
        ICreditRepository Credit { get; }
    }
}
