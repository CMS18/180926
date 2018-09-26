using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQJoinGroupAggreagate
{
    public class Account
    {
        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public decimal Saldo { get; private set; }

        public Account(int id, int customerId, decimal saldo)
        {
            Id = id;
            CustomerId = customerId;
            Saldo = saldo;
        }
    }
}
