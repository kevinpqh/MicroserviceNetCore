using Dapper;
using MSGetData.Beans;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MSGetData.Data
{
    public class CreditRepository : ICreditRepository
    {

        string _cnString;

        public CreditRepository(string cnString)
        {
            _cnString = cnString;
        }

        public IEnumerable<CreditListBean> ListAll()
        {
            using (var connection = new NpgsqlConnection(_cnString))
            {
                connection.Open();
                connection.BeginTransaction();

                var creditListBean = connection.Query<CreditListBean>("fn_list_credits", commandType: CommandType.StoredProcedure);
                connection.Close();

                return creditListBean;
            }
        }
    }
}
