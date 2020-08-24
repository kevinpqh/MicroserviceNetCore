using MSGetData.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSGetData.Data
{
    public interface ICreditRepository
    {
        IEnumerable<CreditListBean> ListAll();
    }
}
