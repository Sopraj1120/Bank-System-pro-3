using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_03
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(int accountNumber, int customerId, decimal initialDeposit)
            : base(accountNumber, customerId, initialDeposit)
        {
        }

        public override bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public override bool Withdraw(decimal amount)
        {
            if (Balance >= amount && amount > 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }


}
