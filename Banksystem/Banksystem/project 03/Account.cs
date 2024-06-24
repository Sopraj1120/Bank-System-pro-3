using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_03
{
    public abstract class Account
    {
        public int AccountNumber { get; set; }
        public int CustomerID { get; set; }
        public decimal Balance { get; protected set; }

        public Account(int accountNumber, int customerId, decimal initialDeposit)
        {
            AccountNumber = accountNumber;
            CustomerID = customerId;
            Balance = initialDeposit;
        }
        public abstract bool Deposit(decimal amount);
        public abstract bool Withdraw(decimal amount);   }
}
