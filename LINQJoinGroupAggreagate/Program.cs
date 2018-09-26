using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQJoinGroupAggreagate
{
    class Program
    {
        static void Main(string[] args)
        {
            //TextDemo();
            //JoinDemo();

            
            if (args.Length < 1)
            {
                Console.WriteLine("Fel. Använd programmet genom att skriva LINQJoinGroupAggreagate.exe <filnamn>");
                return;
            }

            var filnamn = args[0];

            var bank = ReadBankData(filnamn);

            var countOfCustomers = bank.Customers.Where(c => c.Country == "Sweden").Count();

            var accountList = from account in bank.Accounts
                              join customer in bank.Customers on account.CustomerId equals customer.Id
                              select new {
                                            AccountNo = account.Id,
                                            CustomerNo = customer.Id,
                                            CustomerName = customer.Name,
                                            account.Saldo
                                         };
            var custId = 1032;
            var accountsForCustomer = from account in bank.Accounts
                                      where account.CustomerId == custId
                                      select account;

            var accountsForCustomer2 = bank.Accounts.Where(account => account.CustomerId == custId);
            var accountsForCustomer2Result = accountsForCustomer2.ToList();

            var numberOfAccounts = accountsForCustomer2Result.Count();
            Console.WriteLine("numberOfAccounts: " + numberOfAccounts);

            var totalBalance = accountsForCustomer2Result.Sum(a => a.Saldo);
            Console.WriteLine("totalBalance: " + totalBalance);

            Console.WriteLine("List of accounts: ");
            foreach (var item in accountList)
            {
                Console.WriteLine($"Account: #{item.AccountNo} Saldo: {item.Saldo} Customer: #{item.CustomerNo} {item.CustomerName}");
            }

        }

        private static void JoinDemo()
        {
            int[] talen = new[] { 1, 2, 3, 4 };
            char[] bokstäver = new[] { 'A', 'B', 'C' };

            var query = from tal in talen
                         from bokstav in bokstäver
                         select tal + " " + bokstav;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

        }

        private static Bank ReadBankData(string path)
        {
            var bank = new Bank();

            using (var reader = new StreamReader(path))
            {
                int countOfCustomers = int.Parse(reader.ReadLine());

                for (int i = 0; i < countOfCustomers; i++)
                {
                    string line = reader.ReadLine();
                    string[] columns = line.Split(';');

                    var customer = new Customer
                    {
                        Id = int.Parse(columns[0]),
                        Name = columns[2],
                        Country = columns[7]
                    };

                    bank.Customers.Add(customer);
                }

                int countOfAccounts = int.Parse(reader.ReadLine());

                for (int i = 0; i < countOfAccounts; i++)
                {
                    string line = reader.ReadLine();
                    string[] columns = line.Split(';');

                    var account = new Account(id: int.Parse(columns[0]),
                                              customerId: int.Parse(columns[1]),
                                              saldo: decimal.Parse(columns[2], CultureInfo.InvariantCulture));

                    bank.Accounts.Add(account);
                }

            }


            // Vi använder using istället för att garanterat stänga filen.
            // reader.Close(); // Stänger filen - lämnar tillbaka resursen till operativsystemet.


            return bank;
        }

        private static void TextDemo()
        {
            string text = "Hej jag heter Fredrik";

            string baklänges = new string(text.Reverse().ToArray());

            var rövare = String.Join("", text.Select(
                    bokstav => "aoueiäåöy ".Contains(bokstav) ? bokstav.ToString() : bokstav + "o" + bokstav
                ));

            Console.WriteLine(rövare);
        }
    }
}
