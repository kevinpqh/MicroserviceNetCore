using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSReverse.Data
{
    public interface ICreditRepository
    {
        IEnumerable<string> Reverse(int creditId, decimal amount);
    }
}
