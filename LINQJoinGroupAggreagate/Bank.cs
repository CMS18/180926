﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQJoinGroupAggreagate
{
    public class Bank
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
