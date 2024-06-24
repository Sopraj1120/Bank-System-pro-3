using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_03
{

    public class Bank
    {
        private List<Customer> customers = new List<Customer>();

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public Customer FindCustomerById(int customerId)
        {
            return customers.Find(c => c.CustomerId == customerId);
        }

        public bool Deposit(int accountNumber, decimal amount)
        {
            Account account = FindAccountByNumber(accountNumber);
            if (account != null)
            {
                return account.Deposit(amount);
            }
            return false;
        }

        public bool Withdraw(int accountNumber, decimal amount)
        {
            Account account = FindAccountByNumber(accountNumber);
            if (account != null)
            {
                return account.Withdraw(amount);
            }
            return false;
        }

        public bool Transfer(int sourceAccountNumber, int destinationAccountNumber, decimal amount)
        {
            Account sourceAccount = FindAccountByNumber(sourceAccountNumber);
            Account destinationAccount = FindAccountByNumber(destinationAccountNumber);

            if (sourceAccount != null && destinationAccount != null)
            {
                if (sourceAccount.Withdraw(amount))
                {
                    destinationAccount.Deposit(amount);
                    return true;
                }
            }
            return false;
        }

        private Account FindAccountByNumber(int accountNumber)
        {
            foreach (var customer in customers)
            {
                var account = customer.Accounts.Find(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    return account;
                 }
            }
            return null;
        }
    }
}

