using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MSReverse.Data
{
    public class CreditRepository : ICreditRepository
    {
        string _cnString;

        public CreditRepository(string cnString)
        {
            _cnString = cnString;
        }

        public IEnumerable<string> Reverse(int creditId, decimal amount)
        {
            using (var connection = new NpgsqlConnection(_cnString))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("in_credit", creditId);
                parameters.Add("in_amount", amount);

                var value = connection.Query<string>("fn_reverse_credits", parameters, commandType: CommandType.StoredProcedure);

                connection.BeginTransaction();
                connection.Close();

                return value;
            }
        }
    }
}
