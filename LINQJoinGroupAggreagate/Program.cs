using System;
using System.Collections.Generic;
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

            if (args.Length < 1)
            {
                Console.WriteLine("Fel. Använd programmet genom att skriva LINQJoinGroupAggreagate.exe <filnamn>");
                return;
            }

            var filnamn = args[0];

            var bank = ReadBankData(filnamn);

            var countOfCustomers = bank.Customers.Where(c => c.Country == "Sweden").Count();

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
                                              saldo: decimal.Parse(columns[2]));

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
